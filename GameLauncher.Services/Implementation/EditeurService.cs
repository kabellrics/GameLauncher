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
public class EditeurService : BaseService, IEditeurService
{
    //public EditeurService(GameLauncherContext dbContext, IHubContext<SignalRNotificationHub, INotificationService> notifService) : base(dbContext, notifService)
    public EditeurService(GameLauncherContext dbContext) : base(dbContext)
    {
    }
    public IEnumerable<Editeur> GetAll()
    {
        return _dbContext.Editeurs;//.Include(x => x.Items);
    }
    public IEnumerable<Editeur> GetAllForItem(Guid id)
    {
        return _dbContext.Editeurs.Where(x => x.Items.Any(item => item.ItemID == id));
    }
    public ItemEditeur AddEditeurToItem(string editeurname, Item item)
    {
        var dbgenre = _dbContext.Editeurs.FirstOrDefault(x => x.Name == editeurname);
        if (dbgenre == null)
        {
            dbgenre = new Editeur();
            dbgenre.Name = editeurname;
            dbgenre.Items = new List<ItemEditeur>();
            _dbContext.Editeurs.Add(dbgenre);
        }
        var itemgenre = new ItemEditeur()
        {
            ID = Guid.NewGuid(),
            EditeurID = dbgenre.ID,
            ItemID = item.ID
        };
        _dbContext.EditeurdItems.Add(itemgenre);
        if (!dbgenre.Items.Contains(itemgenre))
        {
            dbgenre.Items.Add(itemgenre);
        }
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.Create, "Ajout d'un editeur pour un jeu", $"Ajout de l'éditeur {editeurname} pour {item.Name}");
        return itemgenre;
    }
    public ItemEditeur AddEditeurToItem(string editeurname, Item item, DbContext dbcontext)
    {
        var dbgenre = _dbContext.Editeurs.FirstOrDefault(x => x.Name == editeurname);
        if (dbgenre == null)
        {
            dbgenre = new Editeur();
            dbgenre.Name = editeurname;
            dbgenre.Items = new List<ItemEditeur>();
            _dbContext.Editeurs.Add(dbgenre);
        }
        var itemgenre = new ItemEditeur()
        {
            ID = Guid.NewGuid(),
            EditeurID = dbgenre.ID,
            ItemID = item.ID
        };
        _dbContext.EditeurdItems.Add(itemgenre);
        if (!dbgenre.Items.Contains(itemgenre))
        {
            dbgenre.Items.Add(itemgenre);
        }
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.Create, "Ajout d'un editeur pour un jeu", $"Ajout de l'éditeur {editeurname} pour {item.Name}");
        return itemgenre;
    }
    public void UpdateEditeurInItem(Item Item, List<Editeur> newEditeurs)
    {
        var existingPostTags = _dbContext.EditeurdItems.Where(pt => pt.ItemID == Item.ID);
        _dbContext.EditeurdItems.RemoveRange(existingPostTags);
        if (Item.Editeurs != null)
            Item.Editeurs.Clear();
        foreach (var editeur in newEditeurs)
        {
            AddEditeurToItem(editeur.Name, Item);
        }
    }
    public void Fusionnage(Guid idToDelete, Guid idToKeep)
    {
        _dbContext.EditeurdItems.Where(x => x.EditeurID == idToDelete).ForEachAsync(x => x.EditeurID = idToKeep);
        var deleteItem = _dbContext.Editeurs.First(x => x.ID == idToDelete);
        _dbContext.Editeurs.Remove(deleteItem);
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.Update, "Fusion effectué", $"Fusion entre Editeurs effectué");
    }
    public void Update(Editeur updateditem)
    {
        var item = _dbContext.Editeurs.FirstOrDefault(x => x.ID == updateditem.ID);
        if (item != null)
        {
            item.Name = updateditem.Name;
            _dbContext.Editeurs.Update(item);
            _dbContext.SaveChanges();
            SendNotification(MsgCategory.Update, "MAJ de l'Editeur", $"Mise à jour de {updateditem.Name}");
        }
    }
}
