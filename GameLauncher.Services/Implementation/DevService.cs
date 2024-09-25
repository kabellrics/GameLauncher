using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.Services.Implementation;
public class DevService : BaseService, IDevService
{
    //public DevService(GameLauncherContext dbContext, IHubContext<SignalRNotificationHub, INotificationService> notifService) : base(dbContext, notifService)
    public DevService(GameLauncherContext dbContext) : base(dbContext)
    {
    }
    public IEnumerable<Develloppeur> GetAll()
    {
        return _dbContext.Develloppeurs;//.Include(x=>x.Items);
    }
    public IEnumerable<Develloppeur> GetAllForItem(Guid id)
    {
        return _dbContext.Develloppeurs.Where(x => x.Items.Any(item => item.ItemID == id));
    }
    public ItemDev AddDevToItem(string editeurname, Item item)
    {
        var dbgenre = _dbContext.Develloppeurs.FirstOrDefault(x => x.Name == editeurname);
        if (dbgenre == null)
        {
            dbgenre = new Develloppeur();
            dbgenre.Name = editeurname;
            dbgenre.Items = new List<ItemDev>();
            _dbContext.Develloppeurs.Add(dbgenre);
        }
        var itemgenre = new ItemDev()
        {
            ID = Guid.NewGuid(),
            DevelloppeurID = dbgenre.ID,
            ItemID = item.ID
        };
        _dbContext.DevdItems.Add(itemgenre);
        if (!dbgenre.Items.Contains(itemgenre))
        {
            dbgenre.Items.Add(itemgenre);
        }
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.Create, "Ajout d'un studio pour un jeu", $"Ajout du studio {editeurname} pour {item.Name}");
        return itemgenre;
    }
    public ItemDev AddDevToItem(string editeurname, Item item, DbContext dbcontext)
    {
        var dbgenre = _dbContext.Develloppeurs.FirstOrDefault(x => x.Name == editeurname);
        if (dbgenre == null)
        {
            dbgenre = new Develloppeur();
            dbgenre.Name = editeurname;
            dbgenre.Items = new List<ItemDev>();
            _dbContext.Develloppeurs.Add(dbgenre);
        }
        var itemgenre = new ItemDev()
        {
            ID = Guid.NewGuid(),
            DevelloppeurID = dbgenre.ID,
            ItemID = item.ID
        };
        _dbContext.DevdItems.Add(itemgenre);
        if (!dbgenre.Items.Contains(itemgenre))
        {
            dbgenre.Items.Add(itemgenre);
        }
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.Create, "Ajout d'un studio pour un jeu", $"Ajout du studio {editeurname} pour {item.Name}");
        return itemgenre;
    }
    public void UpdateDevInItem(Item Item, List<Develloppeur> newDevs)
    {
        var existingPostTags = _dbContext.DevdItems.Where(pt => pt.ItemID == Item.ID);
        _dbContext.DevdItems.RemoveRange(existingPostTags);
        if(Item.Develloppeurs != null)
            Item.Develloppeurs.Clear();
        foreach (var dev in newDevs)
        {
            AddDevToItem(dev.Name, Item);
        }
    }
    public void Fusionnage(Guid idToDelete, Guid idToKeep)
    {
        _dbContext.DevdItems.Where(x => x.DevelloppeurID == idToDelete).ForEachAsync(x => x.DevelloppeurID = idToKeep);
        var deleteItem = _dbContext.Develloppeurs.First(x => x.ID == idToDelete);
        _dbContext.Develloppeurs.Remove(deleteItem);
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.Update, "Fusion effectué", $"Fusion entre Dev effectué");
    }
    public void Update(Develloppeur updateditem)
    {
        var item = _dbContext.Develloppeurs.FirstOrDefault(x => x.ID == updateditem.ID);
        if (item != null)
        {
            item.Name = updateditem.Name;
            _dbContext.Develloppeurs.Update(item);
            _dbContext.SaveChanges();
            SendNotification(MsgCategory.Update, "MAJ du Studio", $"Mise à jour de {updateditem.Name}");
        }
    }
}
