using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.UI;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Models;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Contracts.ViewModels;
using GameLauncherAdmin.Core.Contracts.Services;
using GameLauncherAdmin.Core.Models;
using Microsoft.UI.Xaml;

namespace GameLauncherAdmin.ViewModels;

public partial class BibliothequeDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;
    private readonly IMetadataProvider _MetadataService;
    private readonly INavigationService _navigationService;
    private readonly IItemProvider _itemService;

    [ObservableProperty]
    private bool _showMetadataSourceExpander;
    [ObservableProperty]
    private bool _showGameProposalExpander;
    [ObservableProperty]
    private bool _showDataProposalExpander;
    [ObservableProperty]
    private Visibility _visibilityMetadataSourceExpander;
    [ObservableProperty]
    private Visibility _visibilityGameProposalExpander;
    [ObservableProperty]
    private Visibility _visibilityDataProposalExpander;
    [ObservableProperty]
    private List<string> _metadataType = new List<string> { "IGDB", "Screenscraper" };
    [ObservableProperty]
    private string _metadataTypechoice;

    [ObservableProperty]
    private bool _showMediaSourceExpander;
    [ObservableProperty]
    private bool _showGameMediaProposalExpander;
    [ObservableProperty]
    private bool _showMediaProposalExpander;
    [ObservableProperty]
    private Visibility _visibilityMediaSourceExpander;
    [ObservableProperty]
    private Visibility _visibilityGameMediaProposalExpander;
    [ObservableProperty]
    private Visibility _visibilityMediaProposalExpander;
    [ObservableProperty]
    private List<string> _mediaType = new List<string> { "IGDB", "Screenscraper", "SteamGridDB" };

    [ObservableProperty]
    private string _mediaTypechoice;

    [ObservableProperty]
    private string _reconcileNewName;
    [ObservableProperty]
    private string _reconcileNewDateTime;
    [ObservableProperty]
    private string _reconcileDescription;
    [ObservableProperty]
    private ObservableCollection<ObservableEditeur> _reconcileEditeur;
    [ObservableProperty]
    private ObservableCollection<ObservableDevelloppeur> _reconcileDevs;
    [ObservableProperty]
    private ObservableCollection<ObservableGenre> _reconcileGenre;

    [ObservableProperty]
    private int intgameToReconcile;

    [ObservableProperty]
    private ObservableItem? item;

    public ObservableItem GameToReconcile;
    public ObservableMediaItem MediaGameToReconcile;
    public ObservableCollection<string> Logos;
    public ObservableCollection<string> Covers;
    public ObservableCollection<string> Artworks;
    public ObservableCollection<string> Banners;
    public ObservableCollection<string> Videos;


    [ObservableProperty]
    private Visibility _visibilityLogoProposals;
    [ObservableProperty]
    private Visibility _visibilityCoverProposals;
    [ObservableProperty]
    private Visibility _visibilityArtworkProposals;
    [ObservableProperty]
    private Visibility _visibilityBannerProposals;

    public ObservableCollection<ObservableItem> SearchMetadata
    {
        get; private set;
    }
    public ObservableCollection<ObservableMediaItem> SearchMedia
    {
        get; private set;
    }

    private ICommand _cancelCommand;
    private ICommand _cancelMediaCommand;
    private ICommand _fullcancelCommand;
    private ICommand _chooseSourceCommand;
    private ICommand _chooseSourceMediaCommand;
    private ICommand _chooseGameCommand;
    private ICommand _chooseGameMediaCommand;
    private ICommand _chooseReconcileCommand;
    private ICommand _chooseReconcileMediaCommand;
    private ICommand _fullSaveCommand;
    private ICommand _refreshCommand;
    public ICommand RefreshCommand
    {
        get
        {
            return _refreshCommand ?? (_refreshCommand = new RelayCommand(Refresh));
        }
    }

    private void Refresh()
    {
        var tempitem = Item;
        Item = null;
        Item = tempitem;
    }
    public ICommand ChooseSourceMediaCommand
    {
        get
        {
            return _chooseSourceMediaCommand ?? (_chooseSourceMediaCommand = new RelayCommand(GetMediaChoice));
        }
    }
    public ICommand ChooseGameMediaCommand
    {
        get
        {
            return _chooseGameMediaCommand ?? (_chooseGameMediaCommand = new RelayCommand(GetMediaGameChoice));
        }
    }
    public ICommand ChooseReconcileMediaCommand
    {
        get
        {
            return _chooseReconcileMediaCommand ?? (_chooseReconcileMediaCommand = new RelayCommand(GetReconcileMediaChoice));
        }
    }
    public ICommand CancelMediaCommand
    {
        get
        {
            return _cancelMediaCommand ?? (_cancelMediaCommand = new RelayCommand(CancelMedia));
        }
    }
    public ICommand ChooseSourceCommand
    {
        get
        {
            return _chooseSourceCommand ?? (_chooseSourceCommand = new RelayCommand(GetMetadataChoice));
        }
    }
    public ICommand CancelMetadataCommand
    {
        get
        {
            return _cancelCommand ?? (_cancelCommand = new RelayCommand(CancelMetaChoice));
        }
    }
    public ICommand ChooseGameCommand
    {
        get
        {
            return _chooseGameCommand ?? (_chooseGameCommand = new RelayCommand(ChooseMetadataGame));
        }
    }
    public ICommand ChooseReconcileCommand
    {
        get
        {
            return _chooseReconcileCommand ?? (_chooseReconcileCommand = new RelayCommand(GetAllMetadata));
        }
    }
    public ICommand FullCancelCommand
    {
        get
        {
            return _fullcancelCommand ?? (_fullcancelCommand = new RelayCommand(GoBackToList));
        }
    }
    public ICommand FullSaveCommand
    {
        get
        {
            return _fullSaveCommand ?? (_fullSaveCommand = new RelayCommand(SaveAndGoBack));
        }
    }

    public BibliothequeDetailViewModel(INavigationService navigationService, ISampleDataService sampleDataService, IMetadataProvider metadataProvider, IItemProvider itemService)
    {
        _navigationService = navigationService;
        _sampleDataService = sampleDataService;
        _MetadataService = metadataProvider;
        IntgameToReconcile = 0;
        ShowMetadataSourceExpander = false;
        ShowGameProposalExpander = false;
        ShowDataProposalExpander = false;
        VisibilityMetadataSourceExpander = Visibility.Visible;
        VisibilityGameProposalExpander = Visibility.Collapsed;
        VisibilityDataProposalExpander = Visibility.Collapsed;
        ShowMediaSourceExpander = false;
        ShowGameMediaProposalExpander = false;
        ShowMediaProposalExpander = false;
        VisibilityMediaSourceExpander = Visibility.Visible;
        VisibilityGameMediaProposalExpander = Visibility.Collapsed;
        VisibilityMediaProposalExpander = Visibility.Collapsed;
        SearchMetadata = new ObservableCollection<ObservableItem>();
        SearchMedia = new ObservableCollection<ObservableMediaItem>();
        ReconcileEditeur = new ObservableCollection<ObservableEditeur>();
        ReconcileDevs = new ObservableCollection<ObservableDevelloppeur>();
        ReconcileGenre = new ObservableCollection<ObservableGenre>();
        Logos = new ObservableCollection<string>(); ;
        Covers = new ObservableCollection<string>(); ;
        Artworks = new ObservableCollection<string>(); ;
        Banners = new ObservableCollection<string>(); ;
        Videos = new ObservableCollection<string>(); ;
    _itemService = itemService;
        VisibilityArtworkProposals = Visibility.Collapsed;
        VisibilityBannerProposals = Visibility.Collapsed;
        VisibilityCoverProposals = Visibility.Collapsed;
        VisibilityLogoProposals = Visibility.Collapsed;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is ObservableItem detailitem)
        {
            //var data = await _sampleDataService.GetContentGridDataAsync();
            //Item = data.First(i => i.OrderID == orderID);
            Item = detailitem;
        }
    }
    private async void GetMediaChoice()
    {
        ShowMediaSourceExpander = false;
        ShowGameMediaProposalExpander = true;
        ShowMediaProposalExpander = false;
        VisibilityMediaSourceExpander = Visibility.Collapsed;
        VisibilityGameMediaProposalExpander = Visibility.Visible;
        VisibilityMediaProposalExpander = Visibility.Collapsed;
        SearchMedia.Clear();
        if (MediaTypechoice == "IGDB")
        {
            
            foreach(var item in await _MetadataService.GetIGDBMediaGameByName(Item.SearchName))
            {
                SearchMedia.Add(item);
            }
        }

        if (MediaTypechoice == "SteamGridDB")
        {
            var result = await _MetadataService.SearchSteamGridDBGameByName(Item.SearchName);
            foreach (var item in result) 
            {
                SearchMedia.Add(await _MetadataService.GetSteamGridDBMediaItem(item));
            }
        }
        if (MediaTypechoice == "Screenscraper")
        {
            foreach(var item in await _MetadataService.SearchScreenscraperGameMediaByName(Item.SearchName))
            {
                SearchMedia.Add(item);
            }
        }
    }
    private void GetMediaGameChoice()
    {
        ShowMediaSourceExpander = false;
        ShowGameMediaProposalExpander = false;
        ShowMediaProposalExpander = true;
        VisibilityMediaSourceExpander = Visibility.Collapsed;
        VisibilityGameMediaProposalExpander = Visibility.Collapsed;
        VisibilityMediaProposalExpander = Visibility.Visible;
        Logos.Clear();
        Covers.Clear();
        Artworks.Clear();
        Banners.Clear();
        try
        {
            foreach (var item in MediaGameToReconcile.Logos) Logos.Add(item);
            foreach (var item in MediaGameToReconcile.Covers) Covers.Add(item);
            foreach (var item in MediaGameToReconcile.Artworks) Artworks.Add(item);
            foreach (var item in MediaGameToReconcile.Banners) Banners.Add(item);
            if (Artworks.Count > 0)
                VisibilityArtworkProposals = Visibility.Visible;
            if (Banners.Count > 0)
                VisibilityBannerProposals = Visibility.Visible;
            if (Covers.Count > 0)
                VisibilityCoverProposals = Visibility.Visible;
            if (Logos.Count > 0)
                VisibilityLogoProposals = Visibility.Visible;
        }
        catch (Exception ex)
        {
        }

    }
    private void GetReconcileMediaChoice()
    {
        CancelMedia();
    }
    private void CancelMedia()
    {
        VisibilityArtworkProposals = Visibility.Collapsed;
        VisibilityBannerProposals = Visibility.Collapsed;
        VisibilityCoverProposals = Visibility.Collapsed;
        VisibilityLogoProposals = Visibility.Collapsed;
        ShowMediaSourceExpander = false;
        ShowGameMediaProposalExpander = false;
        ShowMediaProposalExpander = false;
        VisibilityMediaSourceExpander = Visibility.Visible;
        VisibilityGameMediaProposalExpander = Visibility.Collapsed;
        VisibilityMediaProposalExpander = Visibility.Collapsed;
        SearchMetadata.Clear();
    }
    private void GetAllMetadata()
    {
        Item.Name = ReconcileNewName;
        Item.ReleaseDate = DateTime.Parse(ReconcileNewDateTime);
        Item.Description = ReconcileDescription;
        Item.Editeurs.Clear();
        Item.Develloppeurs.Clear();
        Item.Genres.Clear();
        foreach(var item in ReconcileEditeur) { Item.Editeurs.Add(item); }
        foreach (var item in ReconcileDevs) { Item.Develloppeurs.Add(item); }
        foreach (var item in ReconcileGenre) { Item.Genres.Add(item); }
        CancelMetaChoice();
    }
    private void ChooseMetadataGame()
    {
        ReconcileNewName = GameToReconcile.Name;
        ReconcileNewDateTime = GameToReconcile.ReleaseDate.ToString("dd/MM/yyyy");
        ReconcileDescription = GameToReconcile.Description;
        ReconcileEditeur = GameToReconcile.Editeurs;
        ReconcileDevs = GameToReconcile.Develloppeurs;
        ReconcileGenre = GameToReconcile.Genres;
        ShowMetadataSourceExpander = false;
        ShowGameProposalExpander = false;
        ShowDataProposalExpander = true;
        VisibilityMetadataSourceExpander = Visibility.Collapsed;
        VisibilityGameProposalExpander = Visibility.Collapsed;
        VisibilityDataProposalExpander = Visibility.Visible;
    }
    private void CancelMetaChoice()
    {
        ShowMetadataSourceExpander = false;
        ShowGameProposalExpander = false;
        ShowDataProposalExpander = false;
        VisibilityMetadataSourceExpander = Visibility.Visible;
        VisibilityGameProposalExpander = Visibility.Collapsed;
        VisibilityDataProposalExpander = Visibility.Collapsed;
        SearchMetadata.Clear();
        ReconcileNewName = string.Empty;
        ReconcileNewDateTime = string.Empty;
        ReconcileDescription = string.Empty;
        ReconcileEditeur.Clear();
        ReconcileDevs.Clear();
        ReconcileGenre.Clear();
    }
    private async void SaveAndGoBack()
    {
        await _itemService.UpdateItem(Item);
        await _itemService.UpdatesGenresForItem(Item.Item, Item.Genres.Select(x => x.Item).ToList());
        await _itemService.UpdatesDevsForItem(Item.Item, Item.Develloppeurs.Select(x => x.Item).ToList());
        await _itemService.UpdatesEditsForItem(Item.Item, Item.Editeurs.Select(x => x.Item).ToList());
        GoBackToList();
    }
    private void GoBackToList()
    {
        _navigationService.GoBack();
    }
    public async void GetMetadataChoice()
    {
        ShowMetadataSourceExpander = false;
        ShowGameProposalExpander = true;
        ShowDataProposalExpander = false;
        VisibilityMetadataSourceExpander = Visibility.Collapsed;
        VisibilityGameProposalExpander = Visibility.Visible;
        VisibilityDataProposalExpander = Visibility.Collapsed;
        SearchMetadata.Clear();
        if (MetadataTypechoice == "IGDB")
        {
            await InitializeGameChoose(_MetadataService.GetIGDBGameByNameAsync(Item.SearchName));
        }

        if (MetadataTypechoice == "Screenscraper")
        {
            await InitializeGameChoose(_MetadataService.SearchScreenscraperGameByNameAsync(Item.SearchName));
        }
    }
    private async Task InitializeGameChoose(IAsyncEnumerable<ObservableItem> asyncitems)
    {
        var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        await Task.Run(async () =>
        {
            await foreach (var item in asyncitems)
            {
                dispatcherQueue.TryEnqueue(() =>
                {
                    SearchMetadata.Add(item);
                });
            }
        });
    }
    public void OnNavigatedFrom()
    {
    }
}
