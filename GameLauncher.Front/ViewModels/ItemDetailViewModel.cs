using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLauncher.Front.Contracts.Services;
using GameLauncher.Front.Contracts.ViewModels;
using GameLauncher.Front.Helpers;
using GameLauncher.Front.ViewModels.Observable;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface.Front;
using Microsoft.UI.Xaml.Documents;
using Windows.Services.Maps;

namespace GameLauncher.Front.ViewModels;

public partial class ItemDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IItemsService _itemsService;
    [ObservableProperty]
    private ObsItem _currentItem;
    [ObservableProperty]
    private Paragraph _currentItemDescription;
    [ObservableProperty]
    private ItemDisplay _currentdisplay;
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
        Currentdisplay = ItemDisplay.SteamLike;
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
        if (parameter == null || (parameter is not ObsItem && parameter is not TrueItemInCollection))
        {
            _navigationService.GoBack();
        }
        else if (parameter is ObsItem obsItem)
        {
            CurrentItem = obsItem;
        }
        else if (parameter is TrueItemInCollection trueItem)
        {
            CurrentItem = new ObsItem(trueItem.Item);
        }

        CurrentItemDescription = HTMLToRTF.ConvertHtmlToParagraph(CurrentItem.Description);
    }
    public void GoBack()
    {
        //_navigationService.GoBack();
        _navigationService.NavigateTo(typeof(ListCollectionViewModel).FullName!);
    }
    [RelayCommand]
    private void OnGoBackClick()
    {
        GoBack();
    }
}
