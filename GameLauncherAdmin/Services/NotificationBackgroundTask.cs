using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.AdminProvider;
using Microsoft.Extensions.Hosting;
using Windows.ApplicationModel.Background;

namespace GameLauncherAdmin.Services;
public class NotificationBackgroundTask: IHostedService
{
    private NotificationProvider _notificationProvider;
    public NotificationBackgroundTask()
    {
        _notificationProvider = new NotificationProvider();
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _notificationProvider.StartAsync();
    }
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_notificationProvider != null)
        {
            await _notificationProvider.StopAsync();
        }
    }
}
