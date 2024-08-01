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
using Newtonsoft.Json.Linq;

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

    [ObservableProperty]
    private bool _haveCollecAllGames;
    [ObservableProperty]
    private bool _haveCollecFavorite;
    [ObservableProperty]
    private bool _haveCollecNeverPlayed;
    [ObservableProperty]
    private bool _haveCollecLastPlayed;
    [ObservableProperty]
    private bool _haveCollecLastAdded;
    [ObservableProperty]
    private bool _haveCollecEmulator;
    public ObservableCollection<ObsCollection> Source { get; } = new ObservableCollection<ObsCollection>();
    public ObservableCollection<String> SourcePredefinePlaylist { get; } = new ObservableCollection<String>();

    [ObservableProperty]
    private string _predefineCollecChose;
    [ObservableProperty]
    private string _predefineSearchValue;
    [ObservableProperty]
    private List<String> _sourcePredefinePlaylistFiltered;


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

    private ICommand _refreshCommand;
    public ICommand RefreshCommand
    {
        get
        {
            return _refreshCommand ?? (_refreshCommand = new RelayCommand(Refresh));
        }
    }

    private async void Refresh()
    {
        await GetAllCollectionAsync();
        await GetDefaultCollectionStatus();
        await GetPreDefinePlaylist();
    }

    private ICommand _GenerateDefaultCollectionCommand;
    public ICommand GenerateDefaultCollectionCommand
    {
        get
        {
            return _GenerateDefaultCollectionCommand ?? (_GenerateDefaultCollectionCommand = new RelayCommand(GenerateDefaultCollection));
        }
    }

    private ICommand _GeneratePredefineCollectionCommand;
    public ICommand GeneratePredefineCollectionCommand
    {
        get
        {
            return _GeneratePredefineCollectionCommand ?? (_GeneratePredefineCollectionCommand = new RelayCommand(GeneratePredefineCollection));
        }
    }

    public CollectionViewModel(INavigationService navigationService, ICollectionProvider collecProvider, IItemProvider itemProvider)
    {
        _navigationService = navigationService;
        _collecProvider = collecProvider;
        _itemProvider = itemProvider;
        ShowCreateCollection = false;
        SourcePredefinePlaylistFiltered = new List<string>();
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
        await GetDefaultCollectionStatus();
        await GetPreDefinePlaylist();
    }
    public void FiltedPredefine()
    {
        SourcePredefinePlaylistFiltered = SourcePredefinePlaylist.Where(s => s.IndexOf(PredefineSearchValue, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
    }
    private async Task GetPreDefinePlaylist()
    {
        SourcePredefinePlaylist.Clear();
        SourcePredefinePlaylistFiltered = new List<string>();
        var predefineList = await _collecProvider.GetPredefineCollection();
        foreach (var predefine in predefineList) { SourcePredefinePlaylist.Add(predefine); }
    }
    private async Task GetDefaultCollectionStatus()
    {
        var collecStatus = await _collecProvider.GetDefaultCollectionStatus();
        HaveCollecAllGames = collecStatus.CollecAllGames;
        HaveCollecEmulator = collecStatus.CollecEmulator;
        HaveCollecFavorite = collecStatus.CollecFavorite;
        HaveCollecLastPlayed = collecStatus.CollecLastPlayed;
        HaveCollecNeverPlayed = collecStatus.CollecNeverPlayed;
    }

    private async void GenerateDefaultCollection()
    {
        var collecStatus = new DefaultCollectionMessage();
        collecStatus.CollecAllGames = HaveCollecAllGames;
        collecStatus.CollecEmulator = HaveCollecEmulator;
        collecStatus.CollecFavorite = HaveCollecFavorite;
        collecStatus.CollecLastPlayed = HaveCollecLastPlayed;
        collecStatus.CollecNeverPlayed = HaveCollecNeverPlayed;
        await _collecProvider.CreateDefaultCollection(collecStatus);
        HaveCollecAllGames = collecStatus.CollecAllGames;
        HaveCollecEmulator = collecStatus.CollecEmulator;
        HaveCollecFavorite = collecStatus.CollecFavorite;
        HaveCollecLastPlayed = collecStatus.CollecLastPlayed;
        HaveCollecNeverPlayed = collecStatus.CollecNeverPlayed;
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


    private async void GeneratePredefineCollection()
    {
        var order = Source.Any() ? Source.Max(x => x.Order): 0;
        Collection collection = new Collection();
        collection.Order = order + 1;
        collection.Name = PredefineCollecChose;
        collection.CodeName = PredefineCollecChose;
        collection.Fanart = string.Empty;
        var fanartfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "Playlists", "Fanart", $"{collection.CodeName}.jpg");
        if (File.Exists(fanartfile))
            collection.Fanart = fanartfile;
        else
            collection.Fanart = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "Playlists", "Fanart", $"{collection.CodeName}.png");
        collection.Logo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "Playlists", "Clear Logo", $"{collection.CodeName}.png");
        collection.ID = Guid.NewGuid();
        collection.Items = new List<CollectionItem>();
        await _collecProvider.CreateCollection(collection);
        PredefineCollecChose = string.Empty;
    }
    private void CreateCollection()
    {
        var order = Source.Any() ? Source.Max(x => x.Order) : 0;
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
