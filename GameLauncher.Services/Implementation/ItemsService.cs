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
public class ItemsService : BaseService, IItemsService
{
    private readonly IAssetDownloader assetService;
    private readonly IGenreService genreService;
    public ItemsService(GameLauncherContext dbContext, IAssetDownloader assetService, IGenreService genreService) : base(dbContext)
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
        var items = _dbContext.Items.OrderBy(x=>x.LUPlatformesId).ThenBy(x=>x.Name).AsAsyncEnumerable();
        return items;
    }
    public Item Insert(Item item)
    {
        _dbContext.Items.Add(item);
        _dbContext.SaveChanges();
        return item;
    }
    public void DeleteItem(Guid id)
    {
        var collecitem = _dbContext.CollectiondItems.Where(x => x.ID == id);
        if (collecitem != null)
        {
            _dbContext.CollectiondItems.RemoveRange(collecitem);
            SendNotification(MsgCategory.Delete, "Collection Item supprimé", "Item retiré de la collection");
        }
        var deleteitem = _dbContext.Items.FirstOrDefault(x => x.ID == id);
        if (deleteitem != null)
        {
            _dbContext.Items.Remove(deleteitem);
            SendNotification(MsgCategory.Delete, " Item supprimé", "Item retiré de la bibliothèque");
        }
        try
        {
            Directory.Delete(Path.Combine("GameLauncher", "Assets", "Item", id.ToString()), true);
        }
        catch (Exception ex)
        {
            //throw;
        }
        _dbContext.SaveChanges();
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
            item.IsFavorite = updateditem.IsFavorite;
            assetService.RapatrierAsset(item);
            _dbContext.Items.Update(item);
            _dbContext.SaveChanges();
        }
    }   
    public void ToggleItemFavorite(Guid updateditemID)
    {
        var item = _dbContext.Items.FirstOrDefault(x=> x.ID == updateditemID);
        if (item != null)
        {
            item.IsFavorite = !item.IsFavorite;
            _dbContext.Items.Update(item);
            _dbContext.SaveChanges();
        }
    }
}
