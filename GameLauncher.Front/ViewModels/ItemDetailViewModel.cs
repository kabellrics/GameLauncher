using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLauncher.Front.Contracts.Services;
using GameLauncher.Front.Contracts.ViewModels;
using GameLauncher.Front.ViewModels.Observable;
using GameLauncher.Services.Interface.Front;
using Windows.Services.Maps;

namespace GameLauncher.Front.ViewModels;

public partial class ItemDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IItemsService _itemsService;
    [ObservableProperty]
    private ObsItem _currentItem;
    private ICommand _toggleFavorisCommand;
    public ICommand ToggleFavorisCommand
    {
        get
        {
            return _toggleFavorisCommand ?? (_toggleFavorisCommand = new RelayCommand(ToggleFavoris));
        }
    }
    public ItemDetailViewModel(INavigationService navigationService, IItemsService itemsService)
    {
        _navigationService = navigationService;
        _itemsService = itemsService;
    }
    public void OnNavigatedFrom()
    {
    }
    public async void OnNavigatedTo(object parameter)
    {
        await InitItemData(parameter);
    }
    public void ToggleFavoris()
    {
        CurrentItem.IsFavorite = !CurrentItem.IsFavorite;
        _itemsService.ToggleItemFavorite(CurrentItem.Id);
    }
    private async Task InitItemData(object parameter)
    {
        if (parameter == null || parameter is not ObsItem) { _navigationService.GoBack(); }
        CurrentItem = parameter as ObsItem;
    }
    public void GoBack()
    {
        _navigationService.GoBack();
    }
    [RelayCommand]
    private void OnGoBackClick()
    {
        GoBack();
    }
}
