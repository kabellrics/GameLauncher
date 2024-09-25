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
public class GenreService : BaseService, IGenreService
{
    public GenreService(GameLauncherContext dbContext) : base(dbContext)
    {
    }
    public IEnumerable<Genre> GetAll()
    {
        return _dbContext.Genres;//.Include(x => x.Items);
    }
    public IEnumerable<Genre> GetAllForItem(Guid id)
    {
        return _dbContext.Genres.Where(x=>x.Items.Any(item=>item.ItemID == id));
    }
    public ItemGenre AddGenreToItem(string genrename, Item item)
    {
        var dbgenre = _dbContext.Genres.FirstOrDefault(x => x.Name == genrename);
        if (dbgenre == null)
        {
            dbgenre = new Genre();
            dbgenre.Name = genrename;
            dbgenre.Items = new List<ItemGenre>();
            _dbContext.Genres.Add(dbgenre);
        }
        var itemgenre = new ItemGenre()
        {
            ID = Guid.NewGuid(),
            GenreID = dbgenre.ID,
            ItemID = item.ID
        };
        _dbContext.GenredItems.Add(itemgenre);
        if (!dbgenre.Items.Contains(itemgenre))
        {
            dbgenre.Items.Add(itemgenre);
        }
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.Create, "Ajout d'un genre pour un jeu", $"Ajout du genre {genrename} pour {item.Name}");
        return itemgenre;
    }
    public ItemGenre AddGenreToItem(string genrename, Item item,DbContext dbcontext)
    {
        var dbgenre = _dbContext.Genres.FirstOrDefault(x => x.Name == genrename);
        if (dbgenre == null)
        {
            dbgenre = new Genre();
            dbgenre.Name = genrename;
            dbgenre.Items = new List<ItemGenre>();
            _dbContext.Genres.Add(dbgenre);
        }
        var itemgenre = new ItemGenre()
        {
            ID = Guid.NewGuid(),
            GenreID = dbgenre.ID,
            ItemID = item.ID
        };
        _dbContext.GenredItems.Add(itemgenre);
        if (!dbgenre.Items.Contains(itemgenre))
        {
            dbgenre.Items.Add(itemgenre);
        }
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.Create, "Ajout d'un genre pour un jeu", $"Ajout du genre {genrename} pour {item.Name}");
        return itemgenre;
    }
    public void UpdateGenreInItem(Item Item, List<Genre> newgenres)
    {
        var existingPostTags = _dbContext.GenredItems.Where(pt => pt.ItemID == Item.ID);
        _dbContext.GenredItems.RemoveRange(existingPostTags);
        if (Item.Genres != null)
            Item.Genres.Clear();
        foreach (var genre in newgenres)
        {
            AddGenreToItem(genre.Name, Item);
        }
    }
    public void Fusionnage(Guid idToDelete, Guid idToKeep)
    {
        _dbContext.GenredItems.Where(x => x.GenreID == idToDelete).ForEachAsync(x => x.GenreID = idToKeep);
        var deleteItem = _dbContext.Genres.First(x => x.ID == idToDelete);
        _dbContext.Genres.Remove(deleteItem);
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.Update, "Fusion effectué", $"Fusion entre Genre effectué");
    }
    public void Update(Genre updateditem)
    {
        var item = _dbContext.Genres.FirstOrDefault(x => x.ID == updateditem.ID);
        if (item != null)
        {
            item.Name = updateditem.Name;
            _dbContext.Genres.Update(item);
            _dbContext.SaveChanges();
            SendNotification(MsgCategory.Update, "MAJ du Genre", $"Mise à jour de {updateditem.Name}");
        }
    }
}
