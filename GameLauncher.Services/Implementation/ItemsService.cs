using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.Services.Implementation;
public class ItemsService : BaseService, IItemsService
{
    private readonly IAssetDownloader assetService;
    private readonly IGenreService genreService;
    public ItemsService(GameLauncherContext dbContext, IHubContext<SignalRNotificationHub, INotificationService> notifService, IAssetDownloader assetService, IGenreService genreService) : base(dbContext, notifService)
    {
        this.assetService = assetService;
        this.genreService = genreService;
    }
    public IEnumerable<Item> GetAll()
    {
        return _dbContext.Items.OrderBy(x => x.LUPlatformesId).ThenBy(x => x.Name);
    }
    public IAsyncEnumerable<Item> GetAllAsync()
    {
        return _dbContext.Items.OrderBy(x=>x.LUPlatformesId).ThenBy(x=>x.Name).AsAsyncEnumerable();
    }
    public void UpdateItem(Item updateditem)
    {
        var item = _dbContext.Items.FirstOrDefault(x=> x.ID == updateditem.ID);
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
            assetService.RapatrierAsset(item);
            _dbContext.Items.Update(item);
            _dbContext.SaveChanges();
        }
    }
}
