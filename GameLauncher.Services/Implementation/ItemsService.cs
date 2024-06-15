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
    private readonly IAssetDownloader assetService;
    public ItemsService(GameLauncherContext dbContext, IAssetDownloader assetService)
    {
        this.dbContext = dbContext;
        this.assetService = assetService;
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
            //foreach (var genre in updateditem.Genres) 
            //{
            //    if(!dbContext.Genres.Any(x=>x.ID == genre.ID))
            //    dbContext.Genres.Add(genre); 
            //}
            //item.Editeurs = updateditem.Editeurs;
            //foreach (var editeur in updateditem.Editeurs)
            //{
            //    if (!dbContext.Editeurs.Any(x => x.ID == editeur.ID))
            //        dbContext.Editeurs.Add(editeur); 
            //}
            //item.Develloppeurs = updateditem.Develloppeurs;
            //foreach (var devs in updateditem.Develloppeurs)
            //{
            //    if (!dbContext.Develloppeurs.Any(x => x.ID == devs.ID))
            //        dbContext.Develloppeurs.Add(devs);
            //}
            assetService.RapatrierAsset(item);
            dbContext.Items.Update(item);
            dbContext.SaveChanges();
        }
    }
}
