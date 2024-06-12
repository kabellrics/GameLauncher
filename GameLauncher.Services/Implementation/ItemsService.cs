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
public class ItemsService : IItemsService
{
    private readonly GameLauncherContext dbContext;
    public ItemsService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<Item> GetAll()
    {
        return dbContext.Items
            .Include(item=>item.Platformes);
    }
    public void UpdateItem(Item updateditem)
    {
        var item = dbContext.Items.FirstOrDefault(x=> x.ID == updateditem.ID);
        if (item != null)
        {
            item.Name = updateditem.Name;
            item.SearchName = updateditem.SearchName;
            item.Description = updateditem.Description;
            item.ReleaseDate = updateditem.ReleaseDate;
            item.LastStartDate = updateditem.LastStartDate;
            item.NbStart = updateditem.NbStart;
            item.Artwork = updateditem.Artwork;
            item.Banner = updateditem.Banner;
            item.Cover = updateditem.Cover;
            item.Logo = updateditem.Logo;
            item.Video = updateditem.Video;
            item.Genres = updateditem.Genres;
            item.Editeurs = updateditem.Editeurs;
            item.Develloppeurs = updateditem.Develloppeurs;
        }
    }
}
