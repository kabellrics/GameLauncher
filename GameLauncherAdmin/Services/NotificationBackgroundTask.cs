using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using GameLauncher.AdminProvider;
using GameLauncher.Models.APIObject;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Windows.ApplicationModel.Background;

namespace GameLauncherAdmin.Services;
public class NotificationBackgroundTask: IHostedService
{
    //private NotificationProvider _notificationProvider;
    private HubConnection _hubConnection;
    public NotificationBackgroundTask()
    {
        //_notificationProvider = new NotificationProvider();
        _hubConnection = new HubConnectionBuilder()
           .WithUrl("https://localhost:7197/notif")
           .WithAutomaticReconnect()
           .Build();
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        //await _notificationProvider.StartAsync();
        try
        {
            _hubConnection.On<string>("SendNotif", (message) =>
            {
                Console.WriteLine($"Message received : {message}");
                WeakReferenceMessenger.Default.Send<NotificationMessage>(JsonConvert.DeserializeObject<NotificationMessage>(message));
            });
            await _hubConnection.StartAsync();
            Console.WriteLine("Connected to the hub.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while connecting: {ex.Message}");
        }
    }
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _hubConnection.StopAsync();
            Console.WriteLine("Disconnected from the hub.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while disconnecting: {ex.Message}");
        }
        //if (_notificationProvider != null)
        //{
        //    await _notificationProvider.StopAsync();
        //}
    }
}
