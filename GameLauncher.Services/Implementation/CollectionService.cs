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
public class CollectionService : ICollectionService
{
    private readonly GameLauncherContext dbContext;
    private readonly IAssetDownloader assetService;
    private readonly IHubContext<SignalRNotificationHub, INotificationService> notifService;
    public CollectionService(GameLauncherContext dbContext, IHubContext<SignalRNotificationHub, INotificationService> notifService, IAssetDownloader assetService)
    {
        this.dbContext = dbContext;
        this.notifService = notifService;
        this.assetService = assetService;
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
    public bool DelteCollectionItem(Guid id)
    {
        var collecitem = dbContext.CollectiondItems.FirstOrDefault(x => x.ID == id);
        if (collecitem != null)
        {
            dbContext.CollectiondItems.Remove(collecitem);
            dbContext.SaveChanges();
            return true;
        }
        return false;
    }
    public bool DelteCollection(Guid id)
    {
        var collec = dbContext.Collections.FirstOrDefault(x => x.ID == id);
        if (collec != null)
        {
            dbContext.Collections.Remove(collec);
            dbContext.SaveChanges();
            notifService.Clients.All.SendMessage(new NotificationMessage { Type = MsgCategory.Delete, MessageTitle = "Collection supprimé" });
            return true;
        }
        return false;
    }
    public void CreateCollection(Collection collec)
    {
        collec.Fanart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "background", $"{collec.CodeName}.jpg");
        collec.Logo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "Collection V2", $"{collec.CodeName}.png");
        dbContext.Collections.Add(collec);
        dbContext.SaveChanges(true);
        notifService.Clients.All.SendMessage(new NotificationMessage {Type= MsgCategory.Create,MessageTitle="Collection ajouté" });
    }
    public void UpsertCollectionItem(Guid collectionId, Guid gameId, int order)
    {
        var collecitem = dbContext.CollectiondItems.FirstOrDefault(x => x.CollectionID == collectionId && x.ItemID == gameId);

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
            dbContext.CollectiondItems.Add(collecitem);
        }

        dbContext.SaveChanges();
        notifService.Clients.All.SendMessage(new NotificationMessage { Type = MsgCategory.Update, MessageTitle = "Collection Item mis à jour" });
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
            assetService.RapatrierAsset(item);
            dbContext.Collections.Update(item);
            dbContext.SaveChanges();
            //notifService.SendMessage(new NotificationMessage { Type = MsgType.NeedUpdate, Message = "Collection mis à jour" });
        }
    }

    public void CreateCollectionFromPlateforme()
    {
        var distinctplateforme = dbContext.Items.GroupBy(x=>x.LUPlatformesId).Select(grp => grp.First().LUPlatformesId).ToList();
        var plateformes = dbContext.Platformes.Where(x => distinctplateforme.Contains(x.Codename));
        int plateformeOrder = 1;
        foreach (var itemplateform in plateformes.OrderBy(x => x.Name))
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
            var itemsplateforms = dbContext.Items.Where(x => x.LUPlatformesId == itemplateform.Codename).OrderBy(x => x.Name);
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
        notifService.Clients.All.SendMessage(new NotificationMessage { Type = MsgCategory.Create, MessageTitle = "Collection ajouté" });
    }
}
