using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Models.Steam;
using GameLauncher.Services.Interface;

namespace GameLauncher.Services.Implementation;
public class PlateformeService : IPlateformeService
{
    private readonly GameLauncherContext dbContext;
    public PlateformeService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<LUPlatformes> GetAll()
    {
        return dbContext.Platformes;
    }
}
