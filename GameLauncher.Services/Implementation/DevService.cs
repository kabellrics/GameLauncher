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
public class DevService : IDevService
{
    private readonly GameLauncherContext dbContext;
    public DevService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<Develloppeur> GetAll()
    {
        return dbContext.Develloppeurs.Include(x=>x.Items);
    }
    public IEnumerable<Develloppeur> GetAllForItem(Guid id)
    {
        return dbContext.Develloppeurs.Where(x => x.Items.Any(item => item.ID == id));
    }
}
