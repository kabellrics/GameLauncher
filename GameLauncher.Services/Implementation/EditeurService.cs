using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.Services.Implementation;
public class EditeurService : IEditeurService
{
    private readonly GameLauncherContext dbContext;
    public EditeurService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<Editeur> GetAll()
    {
        return dbContext.Editeurs;//.Include(x => x.Items);
    }
    public IEnumerable<Editeur> GetAllForItem(Guid id)
    {
        return dbContext.Editeurs.Where(x => x.Items.Any(item => item.ItemID == id));
    }
    public ItemEditeur AddEditeurToItem(string editeurname, Item item)
    {
        var dbgenre = dbContext.Editeurs.FirstOrDefault(x => x.Name == editeurname);
        if (dbgenre == null)
        {
            dbgenre = new Editeur();
            dbgenre.Name = editeurname;
            dbgenre.Items = new List<ItemEditeur>();
            dbContext.Editeurs.Add(dbgenre);
        }
        var itemgenre = new ItemEditeur()
        {
            ID = Guid.NewGuid(),
            EditeurID = dbgenre.ID,
            ItemID = item.ID
        };
        dbContext.EditeurdItems.Add(itemgenre);
        if (!dbgenre.Items.Contains(itemgenre))
        {
            dbgenre.Items.Add(itemgenre);
        }
        dbContext.SaveChanges();
        return itemgenre;
    }
    public void UpdateEditeurInItem(Item Item, List<Editeur> newEditeurs)
    {
        var existingPostTags = dbContext.EditeurdItems.Where(pt => pt.ItemID == Item.ID);
        dbContext.EditeurdItems.RemoveRange(existingPostTags);
        if (Item.Editeurs != null)
            Item.Editeurs.Clear();
        foreach (var editeur in newEditeurs)
        {
            AddEditeurToItem(editeur.Name, Item);
        }
    }
    public void Fusionnage(Guid idToDelete, Guid idToKeep)
    {
        dbContext.EditeurdItems.Where(x => x.EditeurID == idToDelete).ForEachAsync(x => x.EditeurID = idToKeep);
        var deleteItem = dbContext.Editeurs.First(x => x.ID == idToDelete);
        dbContext.Editeurs.Remove(deleteItem);
        dbContext.SaveChanges();
    }
    public void Update(Editeur updateditem)
    {
        var item = dbContext.Editeurs.FirstOrDefault(x => x.ID == updateditem.ID);
        if (item != null)
        {
            item.Name = updateditem.Name;
            dbContext.Editeurs.Update(item);
            dbContext.SaveChanges();
        }
    }
}
