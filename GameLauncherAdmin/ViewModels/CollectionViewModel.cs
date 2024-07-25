using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GameLauncher.AdminProvider;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Contracts.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.ViewModels;

public partial class CollectionViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ICollectionProvider _collecProvider;
    private readonly IItemProvider _itemProvider;
    [ObservableProperty]
    private string _newCollectionName;
    [ObservableProperty]
    private string _newCollectionCode;
    [ObservableProperty]
    private bool _showCreateCollection;
    public ObservableCollection<ObsCollection> Source { get; } = new ObservableCollection<ObsCollection>();
    private ICommand _createCollectionForPlateformeCommand;
    private ICommand _createCollectionCommand;

    public ICommand CreateCollectionForPlateformeCommand
    {
        get
        {
            return _createCollectionForPlateformeCommand ?? (_createCollectionForPlateformeCommand = new RelayCommand(CreateCollectionForPlateforme));
        }
    }
    public ICommand CreateCollectionCommand
    {
        get
        {
            return _createCollectionCommand ?? (_createCollectionCommand = new RelayCommand(CreateCollection));
        }
    }


    public CollectionViewModel(INavigationService navigationService, ICollectionProvider collecProvider, IItemProvider itemProvider)
    {
        _navigationService = navigationService;
        _collecProvider = collecProvider;
        _itemProvider = itemProvider;
        ShowCreateCollection = false;
        WeakReferenceMessenger.Default.Register<NotificationMessage>(this, async (r, m) =>
        {
            if (m is NotificationMessage msg)
            {
                //if (msg.Type == MsgType.NeedUpdate)
                //{
                //    await GetAllCollectionAsync();
                //}
            }
        });
    }
    public void OnNavigatedFrom()
    {
        int order = 1;
        foreach (var item in Source)
        {
            item.Order = order++;
            _collecProvider.UpdateCollection(item);
        }
    }
    public async void OnNavigatedTo(object parameter)
    {
        await GetAllCollectionAsync();
    }

    private async Task GetAllCollectionAsync()
    {
        var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        dispatcherQueue.TryEnqueue(async () =>
        {
            Source.Clear();
            await foreach (var item in _collecProvider.GetCollectionsAsyncEnumerable())
            {
                Source.Add(item);
            }
        });
    }

    private void CreateCollection()
    {
        var order = Source.Max(x => x.Order);
        Collection collection = new Collection();
        collection.Order = order + 1;
        collection.Name = NewCollectionName;
        collection.CodeName = NewCollectionCode;
        collection.Fanart = string.Empty;
        collection.Logo = string.Empty;
        collection.ID = Guid.NewGuid();
        collection.Items = new List<CollectionItem>();
        _collecProvider.CreateCollection(collection);
        ShowCreateCollection = false;
    }
    private async void CreateCollectionForPlateforme()
    {
        await _collecProvider.CreateCollectionFromPlateforme();
    }

    public void OnItemClick(ObsCollection? clickedItem)
    {
        if (clickedItem != null)
        {
            //_navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(CollectionDetailViewModel).FullName!, clickedItem);
        }
    }
}
