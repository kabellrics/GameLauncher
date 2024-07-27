using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace GameLauncher.Services.Implementation;

public class SignalRNotificationHub : Hub<INotificationService>
{
    public async Task SendMessage(NotificationMessage message)
    {
        await Clients.All.SendJsonMessage(JsonConvert.SerializeObject(message));
    }
    public async Task SendJsonMessage(String message)
    {
        await Clients.All.SendJsonMessage(message);
    }
}
