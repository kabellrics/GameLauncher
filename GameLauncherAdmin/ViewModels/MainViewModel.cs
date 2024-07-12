using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.AdminProvider.Interface;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Contracts.ViewModels;

namespace GameLauncherAdmin.ViewModels;

public partial class MainViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    public async void OnNavigatedTo(object parameter)
    {
        _navigationService.NavigateTo(typeof(BibliothequeViewModel).FullName!);
    }
    public void OnNavigatedFrom()
    {
    }
}
