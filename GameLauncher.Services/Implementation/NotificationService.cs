using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;

namespace GameLauncher.Services.Implementation;
public class NotificationService : Hub, INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }
    public async Task SendMessage(NotificationMessage message)
    {
        await _hubContext.Clients.All.SendAsync("Notif", message);
    }
    //public async Task SendMessage(NotificationMessage message)
    //    => await Clients.All.SendAsync("Notification", message);
}
