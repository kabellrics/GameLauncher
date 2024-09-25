using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using GameLauncher.DAL;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;

namespace GameLauncher.Services.Implementation;
public class BaseService
{
    protected readonly GameLauncherContext _dbContext;
    //protected readonly IHubContext<SignalRNotificationHub, INotificationService> _notifService;
    public BaseService(GameLauncherContext dbContext)//, IHubContext<SignalRNotificationHub, INotificationService> notifService)
    {
        this._dbContext = dbContext;
        //this._notifService = notifService;
    }

    protected void SendNotification(MsgCategory category, string title, string message)
    {
        var notif = new NotificationMessage { Type = category, MessageTitle = title, MessageCorps = message };
        WeakReferenceMessenger.Default.Send<NotificationMessage>(notif);
        //_notifService.Clients.All.SendMessage(notif);
    }
}
