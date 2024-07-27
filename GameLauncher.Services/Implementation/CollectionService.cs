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
    public CollectionService(GameLauncherContext dbContext, IHubContext<SignalRNotificationHub, INotificationService> notifService, IAssetDownloader assetService) : base(dbContext, notifService)
    {
        this.assetService = assetService;
    }
    public IEnumerable<Collection> GetAll()
    {
        return _dbContext.Collections;
    }
    public async IAsyncEnumerable<ItemInCollection> GetAllItemInside(Guid id)
    {
        var collecItems = _dbContext.CollectiondItems.Where(x => x.CollectionID == id);
        foreach (var collecittem in collecItems.OrderBy(x => x.Order))
        {
            var item = _dbContext.Items.FirstOrDefault(x => x.ID == collecittem.ItemID);
            if (item != null)
                yield return new ItemInCollection { Item = item, CollectionItem = collecittem };
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
        collec.Fanart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "background", $"{collec.CodeName}.jpg");
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
        SendNotification(MsgCategory.Update,"Collection Item MAJ", "Collection Item mis à jour");
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

    public void CreateCollectionFromPlateforme()
    {
        var distinctplateforme = _dbContext.Items.GroupBy(x=>x.LUPlatformesId).Select(grp => grp.First().LUPlatformesId).ToList();
        var plateformes = _dbContext.Platformes.Where(x => distinctplateforme.Contains(x.Codename));
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
}
