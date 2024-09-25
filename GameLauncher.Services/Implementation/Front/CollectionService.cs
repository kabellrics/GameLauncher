using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Models.Steam;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using Newtonsoft.Json;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using GameLauncher.Models.APIObject;
using Microsoft.AspNetCore.SignalR;
using GameLauncher.Services.Interface.Front;

namespace GameLauncher.Services.Implementation.Front;
public class CollectionService : ICollectionService
{
    protected readonly GameLauncherContext _dbContext;
    public CollectionService(GameLauncherContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<Collection> GetAll()
    {
        return _dbContext.Collections;
    }
    public async Task<IEnumerable<FullCollectionTrueItem>> GetAllFull()
    {
        List<FullCollectionTrueItem> response = new();
        if (!_dbContext.Collections.Any())
        {
            var fullcollec = new FullCollectionTrueItem();
            var collec = new Collection();
            collec.Name = "Tous les Jeux";
            collec.CodeName = "auto-allgames";
            collec.Fanart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "background", $"{collec.CodeName}.jpg");
            collec.Logo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "Collection V2", $"{collec.CodeName}.png");
            fullcollec.Collection = collec;
            fullcollec.Items = new();
            foreach (var item in _dbContext.Items.OrderBy(x => x.Name))
            {
                var trueitem = InitTrueItem(item);
                fullcollec.Items.Add(new TrueItemInCollection() { CollectionItem = null, Item = trueitem });
            }
            response.Add(fullcollec);
            return response;
        }

        var collections = _dbContext.Collections;
        foreach (var collection in collections)
        {
            var fullcollec = new FullCollectionTrueItem();
            fullcollec.Collection = collection;
            fullcollec.Items = new();
            await foreach (var item in GetAllTrueItemInside(collection.ID))
            {
                fullcollec.Items.Add(item);
            }
            if (collection.CodeName == "auto-favorites")
            {
                foreach (var item in _dbContext.Items.Where(x => x.IsFavorite).OrderByDescending(x => x.NbStart))
                {
                    var trueitem = InitTrueItem(item);
                    fullcollec.Items.Add(new TrueItemInCollection() { CollectionItem = null, Item = trueitem });
                }
            }
            if (collection.CodeName == "auto-allgames")
            {
                foreach (var item in _dbContext.Items.Where(x => x.LUPlatformesId != "emulator").OrderBy(x => x.Name))
                {
                    var trueitem = InitTrueItem(item);
                    fullcollec.Items.Add(new TrueItemInCollection() { CollectionItem = null, Item = trueitem });
                }
            }
            if (collection.CodeName == "emulator")
            {
                foreach (var item in _dbContext.Items.Where(x => x.LUPlatformesId == "emulator").OrderBy(x => x.Name))
                {
                    var trueitem = InitTrueItem(item);
                    fullcollec.Items.Add(new TrueItemInCollection() { CollectionItem = null, Item = trueitem });
                }
            }
            if (collection.CodeName == "auto-neverplayed")
            {
                foreach (var item in _dbContext.Items.Where(x => x.LUPlatformesId != "emulator").Where(x => x.NbStart == 0).Take(10).OrderBy(x => x.Name))
                {
                    var trueitem = InitTrueItem(item);
                    fullcollec.Items.Add(new TrueItemInCollection() { CollectionItem = null, Item = trueitem });
                }
            }
            if (collection.CodeName == "auto-lastplayed")
            {
                foreach (var item in _dbContext.Items.Where(x => x.LUPlatformesId != "emulator").OrderByDescending(x => x.LastStartDate).Take(10))
                {
                    var trueitem = InitTrueItem(item);
                    fullcollec.Items.Add(new TrueItemInCollection() { CollectionItem = null, Item = trueitem });
                }
            }
            response.Add(fullcollec);
        }
        response = response.OrderBy(x => x.Collection.Order).ToList();
        return response;
    }
    public async IAsyncEnumerable<TrueItemInCollection> GetAllTrueItemInside(Guid id)
    {
        var collecItems = _dbContext.CollectiondItems.Where(x => x.CollectionID == id);
        foreach (var collecittem in collecItems.OrderBy(x => x.Order))
        {
            var item = _dbContext.Items.FirstOrDefault(x => x.ID == collecittem.ItemID);
            if (item != null)
            {
                var trueitem = InitTrueItem(item);
                yield return new TrueItemInCollection { Item = trueitem, CollectionItem = collecittem };
            }
        }
    }

    private ItemCompleteInfo InitTrueItem(Item? item)
    {
        var trueitem = new ItemCompleteInfo(item);
        //trueitem.GenreValue = string.Join(",", genreService.GetAllForItem(trueitem.ID).Select(x => x.Name));
        //trueitem.DevelloppeurValue = string.Join(",", devService.GetAllForItem(trueitem.ID).Select(x => x.Name));
        //trueitem.EditeurValue = string.Join(",", editService.GetAllForItem(trueitem.ID).Select(x => x.Name));
        //trueitem.PlateformeValue = plateformeService.Get(item.LUPlatformesId).Name;

        trueitem.GenreValue = string.Join(",", _dbContext.Genres.Where(x => x.Items.Any(item => item.ItemID == trueitem.ID)).Select(x => x.Name));
        trueitem.DevelloppeurValue = string.Join(",", _dbContext.Develloppeurs.Where(x => x.Items.Any(item => item.ItemID == trueitem.ID)).Select(x => x.Name));
        trueitem.EditeurValue = string.Join(",", _dbContext.Editeurs.Where(x => x.Items.Any(item => item.ItemID == trueitem.ID)).Select(x => x.Name));
        trueitem.PlateformeValue = _dbContext.Platformes.FirstOrDefault(x => x.Codename == item.LUPlatformesId).Name;
        return trueitem;
    }

    public async IAsyncEnumerable<ItemInCollection> GetAllItemInside(Guid id)
    {
        var collecItems = _dbContext.CollectiondItems.Where(x => x.CollectionID == id);
        foreach (var collecittem in collecItems.OrderBy(x => x.Order))
        {
            var item = _dbContext.Items.FirstOrDefault(x => x.ID == collecittem.ItemID);
            if (item != null)
            {
                yield return new ItemInCollection { Item = item, CollectionItem = collecittem };
            }
        }
    }

}
