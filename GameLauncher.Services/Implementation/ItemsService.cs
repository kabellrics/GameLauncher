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
}
