using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Services.Interface;

namespace GameLauncher.Services.Implementation;
public class StartingService : IStartingService
{
    private readonly GameLauncherContext dbContext;

    public StartingService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task StartITem(Guid itemid)
    {
        var item = dbContext.Items.FirstOrDefault(x => x.ID == itemid);
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
