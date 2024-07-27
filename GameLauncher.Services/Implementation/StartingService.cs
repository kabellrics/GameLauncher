using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;

namespace GameLauncher.Services.Implementation;
public class StartingService : BaseService, IStartingService
{

    public StartingService(GameLauncherContext dbContext, IHubContext<SignalRNotificationHub, INotificationService> notifService) : base(dbContext, notifService)
    {
    }
    public async Task StartITem(Guid itemid)
    {
        var item = _dbContext.Items.FirstOrDefault(x => x.ID == itemid);
        if (item != null)
        {
            if (item.LUProfileId == null)
            {
                var ps = new ProcessStartInfo(item.Path)
                {
                    UseShellExecute = true,
                    Verb = "open"
                };
                Process.Start(ps);
            }
        }
    }
}
