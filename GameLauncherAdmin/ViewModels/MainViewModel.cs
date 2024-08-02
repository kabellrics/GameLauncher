using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Contracts.ViewModels;

namespace GameLauncherAdmin.ViewModels;

public partial class MainViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IItemProvider _itemService;

    public ObservableCollection<ObservableItem> ItemsWithoutArtwork = new();
    public ObservableCollection<ObservableItem> ItemsWithoutBanner = new();
    public ObservableCollection<ObservableItem> ItemsWithoutCover = new();
    public ObservableCollection<ObservableItem> ItemsWithoutDescription = new();
    public ObservableCollection<ObservableItem> ItemsWithoutDevelloppeurs = new();
    public ObservableCollection<ObservableItem> ItemsWithoutEditeurs = new();
    public ObservableCollection<ObservableItem> ItemsWithoutGenres = new();
    public ObservableCollection<ObservableItem> ItemsWithoutLogo = new();
    public ObservableCollection<ObservableItem> ItemsWithoutReleaseDate = new();
    public ObservableCollection<ObservableItem> ItemsWithoutVideo = new();
    public ObservableCollection<ObsCollection> CollectionsWithoutArtwork = new();
    public ObservableCollection<ObsCollection> CollectionsWithoutLogo = new();
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
        InitStats();
    }
    public MainViewModel(INavigationService navigationService, IItemProvider itemService)
    {
        _navigationService = navigationService;
        _itemService = itemService;
    }
    public async void OnNavigatedTo(object parameter)
    {
        InitStats();
    }
    private async void InitStats()
    {
        var stats = await _itemService.GetStatsAsync();
        ItemsWithoutArtwork.Clear();
        foreach (var item in stats.ItemsWithoutArtwork) { ItemsWithoutArtwork.Add(new ObservableItem(item)); }
        ItemsWithoutBanner.Clear();
        foreach (var item in stats.ItemsWithoutBanner) { ItemsWithoutBanner.Add(new ObservableItem(item)); }
        ItemsWithoutCover.Clear();
        foreach (var item in stats.ItemsWithoutCover) { ItemsWithoutCover.Add(new ObservableItem(item)); }
        ItemsWithoutDescription.Clear();
        foreach (var item in stats.ItemsWithoutDescription) { ItemsWithoutDescription.Add(new ObservableItem(item)); }
        ItemsWithoutDevelloppeurs.Clear();
        foreach (var item in stats.ItemsWithoutDevelloppeurs) { ItemsWithoutDevelloppeurs.Add(new ObservableItem(item)); }
        ItemsWithoutEditeurs.Clear();
        foreach (var item in stats.ItemsWithoutEditeurs) { ItemsWithoutEditeurs.Add(new ObservableItem(item)); }
        ItemsWithoutGenres.Clear();
        foreach (var item in stats.ItemsWithoutGenres) { ItemsWithoutGenres.Add(new ObservableItem(item)); }
        ItemsWithoutLogo.Clear();
        foreach (var item in stats.ItemsWithoutLogo) { ItemsWithoutLogo.Add(new ObservableItem(item)); }
        ItemsWithoutReleaseDate.Clear();
        foreach (var item in stats.ItemsWithoutReleaseDate) { ItemsWithoutReleaseDate.Add(new ObservableItem(item)); }
        ItemsWithoutVideo.Clear();
        foreach (var item in stats.ItemsWithoutVideo) { ItemsWithoutVideo.Add(new ObservableItem(item)); }
        CollectionsWithoutArtwork.Clear();
        foreach (var item in stats.CollectionsWithoutArtwork) { CollectionsWithoutArtwork.Add(new ObsCollection(item)); }
        CollectionsWithoutLogo.Clear();
        foreach (var item in stats.CollectionsWithoutLogo) { CollectionsWithoutLogo.Add(new ObsCollection(item)); }

    }
    [RelayCommand]
    private void OnItemClick(ObservableItem? clickedItem)
    {
        if (clickedItem != null)
        {
            //_navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(BibliothequeDetailViewModel).FullName!, clickedItem);
        }
    }
    public void OnNavigatedFrom()
    {
    }
}
