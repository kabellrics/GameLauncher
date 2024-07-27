using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Models.Steam;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;

namespace GameLauncher.Services.Implementation;
public class PlateformeService : BaseService, IPlateformeService
{
    public PlateformeService(GameLauncherContext dbContext, IHubContext<SignalRNotificationHub, INotificationService> notifService) : base(dbContext, notifService)
    {
    }
    public IEnumerable<LUPlatformes> GetAll()
    {
        return _dbContext.Platformes;
    }
    public LUPlatformes Get(string id)
    {
        return _dbContext.Platformes.FirstOrDefault(x=>x.Codename == id);
    }
}
