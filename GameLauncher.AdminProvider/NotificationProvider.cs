using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Connector;
using GameLauncher.Models.APIObject;
using Microsoft.AspNetCore.SignalR.Client;
using RestSharp;

namespace GameLauncher.AdminProvider;
public class NotificationProvider
{
    private HubConnection _hubConnection;
    public NotificationProvider()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7197/Notif")
            .WithAutomaticReconnect()
            .Build();
    }

    public async Task StartAsync()
    {
        try
        {
            _hubConnection.On<NotificationMessage>("SendNotif", (message) =>
            {
                WeakReferenceMessenger.Default.Send<NotificationMessage>(message);
                Console.WriteLine($"Message received : {message}");
            });
            await _hubConnection.StartAsync();
            Console.WriteLine("Connected to the hub.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while connecting: {ex.Message}");
        }
    }

    public async Task StopAsync()
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
    }
}
