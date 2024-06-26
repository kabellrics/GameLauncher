using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Models.Steam;
using GameLauncher.Services.Interface;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using Newtonsoft.Json;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using GameLauncher.Models.APIObject;

namespace GameLauncher.Services.Implementation;
public class CollectionService : ICollectionService
{
    private readonly GameLauncherContext dbContext;
    public CollectionService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<Collection> GetAll()
    {
        return dbContext.Collections;
    }
    public async IAsyncEnumerable<ItemInCollection> GetAllItemInside(Guid id)
    {
        var collecItems = dbContext.CollectiondItems.Where(x => x.CollectionID == id);
        foreach (var collecittem in collecItems.OrderBy(x => x.Order))
        {
            var item = dbContext.Items.FirstOrDefault(x => x.ID == collecittem.ItemID);
            if (item != null)
                yield return new ItemInCollection { Item = item, CollectionItem = collecittem };
        }
    }
    public void AddToCollectionEnd(Guid id, Guid gameid)
    {
        var collecitems = dbContext.CollectiondItems.Where(x => x.CollectionID == id);
        var lastitemorder = collecitems.Max(x => x.Order);
        var newCollecItem = new CollectionItem { CollectionID = id, ItemID = gameid, Order = lastitemorder + 1 };
        dbContext.CollectiondItems.Add(newCollecItem);
        dbContext.SaveChanges();
    }
    public void UpdateCollectionItemOrder(Guid id, Guid gameid, int newOrder)
    {
        var collecitem = dbContext.CollectiondItems.FirstOrDefault(x => x.CollectionID == id && x.ItemID == gameid);
        if (collecitem != null)
        {
            collecitem.Order = newOrder;
            var collecitemstoupdate = dbContext.CollectiondItems.Where(x => x.CollectionID == id && x.Order >= newOrder);
            foreach (var toupdatecollecitem in collecitemstoupdate)
                toupdatecollecitem.Order += 1;
            dbContext.SaveChanges();
        }

    }
    public void Update(Collection updatedcollection)
    {
        var item = dbContext.Collections.FirstOrDefault(x => x.ID == updatedcollection.ID);
        if (item != null)
        {
            item.Name = updatedcollection.Name;
            item.Order = updatedcollection.Order;
            item.CodeName = updatedcollection.CodeName;
            item.Fanart = updatedcollection.Fanart;
            item.Logo = updatedcollection.Logo;
            dbContext.Collections.Update(item);
            dbContext.SaveChanges();
        }
    }

    public void CreateCollectionFromPlateforme()
    {
        var distinctplateforme = dbContext.Items.GroupBy(x=>x.LUPlatformesId).Select(grp => grp.First()).ToList();
        int plateformeOrder = 1;
        foreach (var itemplateform in distinctplateforme.OrderBy(x => x.Name))
        {
            var collec = dbContext.Collections.FirstOrDefault(x => x.Name == itemplateform.Name);
            if (collec == null)
            {
                collec = new Collection();
                collec.Name = itemplateform.Name;
                var codename = dbContext.Platformes.FirstOrDefault(x => x.Name == collec.Name)?.Codename;
                collec.CodeName = string.IsNullOrEmpty(codename) ? string.Empty : codename;
                collec.Fanart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "background", $"{collec.CodeName}.jpg");
                collec.Logo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "Collection V2", $"{collec.CodeName}.png");
                collec.Order = plateformeOrder++;
                dbContext.Collections.Add(collec);
            }
            int itemorder = 1;
            var itemsplateforms = dbContext.Items.Where(x => x.LUPlatformesId == itemplateform.LUPlatformesId).OrderBy(x => x.Name);
            if (dbContext.CollectiondItems.Any(x => x.CollectionID == collec.ID))
            {
                var maxorder = dbContext.CollectiondItems.Where(x => x.CollectionID == collec.ID).Max(x => x.Order);
                itemorder = maxorder + 1;
            }
            foreach (var item in itemsplateforms)
            {
                if (!dbContext.CollectiondItems.Any(x => x.ItemID == item.ID && x.CollectionID == collec.ID))
                {
                    var collecitem = new CollectionItem();
                    collecitem.ID = Guid.NewGuid();
                    collecitem.ItemID = item.ID;
                    collecitem.CollectionID = collec.ID;
                    collecitem.Order = itemorder++;
                    dbContext.CollectiondItems.Add(collecitem);
                }
            }

        }
        dbContext.SaveChanges();
    }
}
