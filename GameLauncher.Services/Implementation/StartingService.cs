using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using SharpDX.XInput;

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
public class XInputWatcher
{
    Controller controller;
    public Gamepad gamepad;
    public bool connected = false;
    public int deadband = 2500;
    public Point leftThumb, rightThumb = new Point(0, 0);
    public float leftTrigger, rightTrigger;

    public XInputWatcher()
    {
        controller = new Controller(UserIndex.One);
        connected = controller.IsConnected;
    }

    // Call this method to update all class values
    public void Update()
    {
        //if (!connected)
        if (!controller.IsConnected)
            return;

        gamepad = controller.GetState().Gamepad;
        leftTrigger = gamepad.LeftTrigger;
        rightTrigger = gamepad.RightTrigger;
    }
}
