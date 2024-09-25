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
using Microsoft.AspNetCore.SignalR;

namespace GameLauncher.Services.Implementation;
public class CollectionService : BaseService, ICollectionService
{
    private readonly IAssetDownloader assetService;
    private readonly IGenreService genreService;
    private readonly IDevService devService;
    private readonly IEditeurService editService;
    private readonly IPlateformeService plateformeService;
    //public CollectionService(GameLauncherContext dbContext, IHubContext<SignalRNotificationHub, INotificationService> notifService, IAssetDownloader assetService
    //    , IGenreService genreService, IDevService devService, IEditeurService editService, IPlateformeService plateformeService)
    //    : base(dbContext, notifService)
        public CollectionService(GameLauncherContext dbContext, IAssetDownloader assetService
        , IGenreService genreService, IDevService devService, IEditeurService editService, IPlateformeService plateformeService)
        : base(dbContext)
    {
        this.assetService = assetService;
        this.genreService = genreService;
        this.devService = devService;
        this.editService = editService;
        this.plateformeService = plateformeService;
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
        trueitem.GenreValue = string.Join(",", genreService.GetAllForItem(trueitem.ID).Select(x => x.Name));
        trueitem.DevelloppeurValue = string.Join(",", devService.GetAllForItem(trueitem.ID).Select(x => x.Name));
        trueitem.EditeurValue = string.Join(",", editService.GetAllForItem(trueitem.ID).Select(x => x.Name));
        trueitem.PlateformeValue = plateformeService.Get(item.LUPlatformesId).Name;
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
    public void AddToCollectionEnd(Guid id, Guid gameid)
    {
        var collecitems = _dbContext.CollectiondItems.Where(x => x.CollectionID == id);
        var lastitemorder = collecitems.Max(x => x.Order);
        var newCollecItem = new CollectionItem { CollectionID = id, ItemID = gameid, Order = lastitemorder + 1 };
        _dbContext.CollectiondItems.Add(newCollecItem);
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.Create, "Item ajouté à la Collection", $"Ajout de l'item id {gameid} dans la collection ID {id}");
    }
    public bool DelteCollectionItem(Guid id)
    {
        var collecitem = _dbContext.CollectiondItems.FirstOrDefault(x => x.ID == id);
        if (collecitem != null)
        {
            _dbContext.CollectiondItems.Remove(collecitem);
            _dbContext.SaveChanges();
            SendNotification(MsgCategory.Delete, "Collection Item supprimé", "Item retiré de la collection");
            return true;
        }
        return false;
    }
    public bool DelteCollection(Guid id)
    {
        var collec = _dbContext.Collections.FirstOrDefault(x => x.ID == id);
        if (collec != null)
        {
            _dbContext.Collections.Remove(collec);
            _dbContext.SaveChanges();
            SendNotification(MsgCategory.Delete, "Collection supprimé", $"Suppression de {collec.Name}");
            return true;
        }
        return false;
    }
    public void CreateCollection(Collection collec)
    {
        if (string.IsNullOrEmpty(collec.Fanart))
            collec.Fanart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "background", $"{collec.CodeName}.jpg");
        if (string.IsNullOrEmpty(collec.Logo))
            collec.Logo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "Collection V2", $"{collec.CodeName}.png");
        _dbContext.Collections.Add(collec);
        _dbContext.SaveChanges(true);
        SendNotification(MsgCategory.Create, "Collection ajouté", $"Ajout de {collec.Name}");
    }
    public void UpsertCollectionItem(Guid collectionId, Guid gameId, int order)
    {
        var collecitem = _dbContext.CollectiondItems.FirstOrDefault(x => x.CollectionID == collectionId && x.ItemID == gameId);

        if (collecitem != null)
        {
            // Update the existing item
            collecitem.Order = order;
        }
        else
        {
            // Insert a new item
            collecitem = new CollectionItem
            {
                ID = Guid.NewGuid(),
                CollectionID = collectionId,
                ItemID = gameId,
                Order = order
            };
            _dbContext.CollectiondItems.Add(collecitem);
        }

        _dbContext.SaveChanges();
        SendNotification(MsgCategory.Update, "Collection Item MAJ", "Collection Item mis à jour");
    }
    public void Update(Collection updatedcollection)
    {
        var item = _dbContext.Collections.FirstOrDefault(x => x.ID == updatedcollection.ID);
        if (item != null)
        {
            item.Name = updatedcollection.Name;
            item.Order = updatedcollection.Order;
            item.CodeName = updatedcollection.CodeName;
            item.Fanart = updatedcollection.Fanart;
            item.Logo = updatedcollection.Logo;
            assetService.RapatrierAsset(item);
            _dbContext.Collections.Update(item);
            _dbContext.SaveChanges();
            SendNotification(MsgCategory.Update, $"Collection {item.Name} MAJ", $"Collection {item.Name} mis à jour");
            //notifService.SendMessage(new NotificationMessage { Type = MsgType.NeedUpdate, Message = "Collection mis à jour" });
        }
    }
    public DefaultCollectionMessage GetDefaultCollectionStatus()
    {
        DefaultCollectionMessage reponse = new DefaultCollectionMessage();
        reponse.CollecEmulator = _dbContext.Collections.Any(x => x.CodeName == "emulator");
        reponse.CollecAllGames = _dbContext.Collections.Any(x => x.CodeName == "auto-allgames");
        reponse.CollecFavorite = _dbContext.Collections.Any(x => x.CodeName == "auto-favorites");
        reponse.CollecNeverPlayed = _dbContext.Collections.Any(x => x.CodeName == "auto-neverplayed");
        reponse.CollecLastPlayed = _dbContext.Collections.Any(x => x.CodeName == "auto-lastplayed");
        return reponse;
    }
    public DefaultCollectionMessage CreateDefaultColection(DefaultCollectionMessage collectionMessage)
    {
        if (collectionMessage != null)
        {
            try
            {
                var plateformeOrder = 0;
                try
                {
                    plateformeOrder = _dbContext.Collections.Max(x => x.Order);
                }
                catch (Exception ex)
                {
                    //throw;
                }
                var collectionsData = new Dictionary<string, (bool IsPresent, string CodeName)>
        {
            { "Emulateurs",(collectionMessage.CollecEmulator, "emulator") },
            { "Tous les Jeux",(collectionMessage.CollecAllGames, "auto-allgames") },
            { "Favoris",(collectionMessage.CollecFavorite, "auto-favorites") },
            { "Jamais Joués",(collectionMessage.CollecNeverPlayed, "auto-neverplayed") },
            { "Joués Recemment",(collectionMessage.CollecLastPlayed, "auto-lastplayed") },
        };

                foreach (var entry in collectionsData)
                {
                    if (entry.Value.IsPresent)
                    {
                        var collec = new Collection
                        {
                            Name = entry.Key,
                            CodeName = entry.Value.CodeName,
                            Fanart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "background", $"{entry.Value.CodeName}.jpg"),
                            Logo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "Collection V2", $"{entry.Value.CodeName}.png"),
                            Order = plateformeOrder++
                        };
                        _dbContext.Collections.Add(collec);
                        SendNotification(MsgCategory.Create, $"Collection {collec.Name}", $"Collection {collec.Name} ajouté");
                    }
                    else
                    {
                        var todeletecollec = _dbContext.Collections.FirstOrDefault(x => x.CodeName == entry.Value.CodeName);
                        if (todeletecollec != null)
                        {
                            _dbContext.CollectiondItems.RemoveRange(_dbContext.CollectiondItems.Where(x => x.CollectionID == todeletecollec.ID));
                            _dbContext.Collections.Remove(todeletecollec);
                            SendNotification(MsgCategory.Delete, "Collection supprimé", $"Suppression de {todeletecollec.Name}");
                        }
                    }
                }
                _dbContext.SaveChanges();
                SendNotification(MsgCategory.EndTask, $"Fin de Tache", $"Fin de la Creation automatique des Collections par default");
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        return GetDefaultCollectionStatus();
    }
    public void CreateCollectionFromPlateforme()
    {
        var distinctplateforme = _dbContext.Items.GroupBy(x => x.LUPlatformesId).Select(grp => grp.First().LUPlatformesId).ToList();
        var plateformes = _dbContext.Platformes.Where(x => distinctplateforme.Contains(x.Codename) && x.Codename != "emulator");
        int plateformeOrder = 1;
        foreach (var itemplateform in plateformes.OrderBy(x => x.Name))
        {
            var collec = _dbContext.Collections.FirstOrDefault(x => x.Name == itemplateform.Name);
            if (collec == null)
            {
                collec = new Collection();
                collec.Name = itemplateform.Name;
                var codename = _dbContext.Platformes.FirstOrDefault(x => x.Name == collec.Name)?.Codename;
                collec.CodeName = string.IsNullOrEmpty(codename) ? string.Empty : codename;
                collec.Fanart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "background", $"{collec.CodeName}.jpg");
                collec.Logo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "Collection V2", $"{collec.CodeName}.png");
                collec.Order = plateformeOrder++;
                _dbContext.Collections.Add(collec);
                SendNotification(MsgCategory.Create, $"Collection {collec.Name}", $"Collection {collec.Name} ajouté");
            }
            int itemorder = 1;
            var itemsplateforms = _dbContext.Items.Where(x => x.LUPlatformesId == itemplateform.Codename).OrderBy(x => x.Name);
            if (_dbContext.CollectiondItems.Any(x => x.CollectionID == collec.ID))
            {
                var maxorder = _dbContext.CollectiondItems.Where(x => x.CollectionID == collec.ID).Max(x => x.Order);
                itemorder = maxorder + 1;
            }
            foreach (var item in itemsplateforms)
            {
                if (!_dbContext.CollectiondItems.Any(x => x.ItemID == item.ID && x.CollectionID == collec.ID))
                {
                    var collecitem = new CollectionItem();
                    collecitem.ID = Guid.NewGuid();
                    collecitem.ItemID = item.ID;
                    collecitem.CollectionID = collec.ID;
                    collecitem.Order = itemorder++;
                    _dbContext.CollectiondItems.Add(collecitem);
                    SendNotification(MsgCategory.Create, $"Ajout Dans {collec.Name} de {item.Name}", $"Ajout Dans {collec.Name} de {item.Name} au rang {collecitem.Order}");
                }
            }

        }
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.EndTask, $"Fin de Tache", $"Fin de la Creation automatique de Collection à partir des plateformes");
        //_notifService.Clients.All.SendMessage(new NotificationMessage { Type = MsgCategory.Create, MessageTitle = "Collection ajouté" });
    }
    public List<String> GetPredefineCollection()
    {
        var playlistAssetPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "Playlists", "Clear Logo");
        var files = Directory.GetFiles(playlistAssetPath, "*.png");
        return files.Select(x => Path.GetFileNameWithoutExtension(x)).ToList();
    }
}
