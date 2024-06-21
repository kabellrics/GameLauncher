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
public class GenreService : IGenreService
{
    private readonly GameLauncherContext dbContext;
    public GenreService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<Genre> GetAll()
    {
        return dbContext.Genres;//.Include(x => x.Items);
    }
    public IEnumerable<Genre> GetAllForItem(Guid id)
    {
        return dbContext.Genres.Where(x=>x.Items.Any(item=>item.ItemID == id));
    }
    public ItemGenre AddGenreToItem(string genrename, Item item)
    {
        var dbgenre = dbContext.Genres.FirstOrDefault(x => x.Name == genrename);
        if (dbgenre == null)
        {
            dbgenre = new Genre();
            dbgenre.Name = genrename;
            dbgenre.Items = new List<ItemGenre>();
            dbContext.Genres.Add(dbgenre);
        }
        var itemgenre = new ItemGenre()
        {
            ID = Guid.NewGuid(),
            GenreID = dbgenre.ID,
            ItemID = item.ID
        };
        dbContext.GenredItems.Add(itemgenre);
        if (!dbgenre.Items.Contains(itemgenre))
        {
            dbgenre.Items.Add(itemgenre);
        }
        dbContext.SaveChanges();
        return itemgenre;
    }
    public void UpdateGenreInItem(Item Item, List<Genre> newgenres)
    {
        var existingPostTags = dbContext.GenredItems.Where(pt => pt.ItemID == Item.ID);
        dbContext.GenredItems.RemoveRange(existingPostTags);
        if (Item.Genres != null)
            Item.Genres.Clear();
        foreach (var genre in newgenres)
        {
            AddGenreToItem(genre.Name, Item);
        }
    }
    public void Fusionnage(Guid idToDelete, Guid idToKeep)
    {
        dbContext.GenredItems.Where(x => x.GenreID == idToDelete).ForEachAsync(x => x.GenreID = idToKeep);
        var deleteItem = dbContext.Genres.First(x => x.ID == idToDelete);
        dbContext.Genres.Remove(deleteItem);
        dbContext.SaveChanges();
    }
    public void Update(Genre updateditem)
    {
        var item = dbContext.Genres.FirstOrDefault(x => x.ID == updateditem.ID);
        if (item != null)
        {
            item.Name = updateditem.Name;
            dbContext.Genres.Update(item);
            dbContext.SaveChanges();
        }
    }
}
