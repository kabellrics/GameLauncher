using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface.Front;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.Services.Implementation.Front;
public class ItemsService : IItemsService
{
    protected readonly GameLauncherContext _dbContext;
    public ItemsService(GameLauncherContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<Item> GetAll()
    {
        return _dbContext.Items.OrderBy(x => x.LUPlatformesId).ThenBy(x => x.Name);
    }
    public IAsyncEnumerable<Item> GetAllAsync()
    {
        var items = _dbContext.Items.OrderBy(x => x.LUPlatformesId).ThenBy(x => x.Name).AsAsyncEnumerable();
        return items;
    }
    public void ToggleItemFavorite(Guid updateditemID)
    {
        var item = _dbContext.Items.FirstOrDefault(x => x.ID == updateditemID);
        if (item != null)
        {
            item.IsFavorite = !item.IsFavorite;
            _dbContext.Items.Update(item);
            _dbContext.SaveChanges();
        }
    }
}
