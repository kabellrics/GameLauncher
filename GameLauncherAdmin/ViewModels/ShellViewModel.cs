using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GameLauncher.Models.APIObject;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Navigation;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GameLauncherAdmin.ViewModels;

public partial class ShellViewModel : ObservableRecipient
{
    private DispatcherQueue dispatcherQueue;
    [ObservableProperty]
    private bool isBackEnabled;

    [ObservableProperty]
    private object? selected;

    public ObservableCollection<NotificationMessage> Notif = new();
    public INavigationService NavigationService
    {
        get;
    }

    public INavigationViewService NavigationViewService
    {
        get;
    }

    private ICommand _EmptyNotifCommand;
    public ICommand EmptyNotifCommand
    {
        get
        {
            return _EmptyNotifCommand ?? (_EmptyNotifCommand = new RelayCommand(EmptyNotif));
        }
    }

    private void EmptyNotif() => Notif.Clear();

    [ObservableProperty]
    private HubConnection _hubConnection;

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService)
    {
        dispatcherQueue = DispatcherQueue.GetForCurrentThread();
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        InitSignalRClient();
    }

    private async void InitSignalRClient()
    {
        HubConnection = new HubConnectionBuilder()
           .WithUrl("https://localhost:7197/notif")
           .WithAutomaticReconnect()
           .Build();
        HubConnection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await HubConnection.StartAsync();
        };
        try
        {
            HubConnection.On<NotificationMessage>("SendMessage", (message) =>
            {
                dispatcherQueue.TryEnqueue(() => {
                    Notif.Insert(0, message);
                });
                //WeakReferenceMessenger.Default.Send<NotificationMessage>(JsonConvert.DeserializeObject<NotificationMessage>(message));
            });
            HubConnection.On<string>("SendJsonMessage", async (message) =>
            {
                        Console.WriteLine($"Message received : {message}");
                        var msg = JsonConvert.DeserializeObject<NotificationMessage>(message);
                        Notif.Insert(0,msg );
            });
            await HubConnection.StartAsync();            
            Console.WriteLine("Connected to the hub.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while connecting: {ex.Message}");
        }
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }
    }
}
