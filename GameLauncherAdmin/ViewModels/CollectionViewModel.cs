using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLauncher.AdminProvider;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Models;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Contracts.ViewModels;

namespace GameLauncherAdmin.ViewModels;

public partial class CollectionViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ICollectionProvider _collecProvider;
    private readonly IItemProvider _itemProvider;
    public ObservableCollection<ObsCollection> Source { get; } = new ObservableCollection<ObsCollection>();
    private ICommand _createCollectionForPlateformeCommand;

    public ICommand CreateCollectionForPlateformeCommand
    {
        get
        {
            return _createCollectionForPlateformeCommand ?? (_createCollectionForPlateformeCommand = new RelayCommand(CreateCollectionForPlateforme));
        }
    }

    public CollectionViewModel(INavigationService navigationService, ICollectionProvider collecProvider, IItemProvider itemProvider)
    {
        _navigationService = navigationService;
        _collecProvider = collecProvider;
        _itemProvider = itemProvider;
    }
    public void OnNavigatedFrom()
    {
    }
    public async void OnNavigatedTo(object parameter)
    {
        await GetAllCollectionAsync();
    }

    private async Task GetAllCollectionAsync()
    {
        Source.Clear();
        await foreach (var item in _collecProvider.GetCollectionsAsyncEnumerable())
        {
            Source.Add(item);
        }
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
