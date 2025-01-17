using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Models.APIObject;
using GameLauncher.ObservableObjet;
using GameLauncher.Services.Interface;
using GameLauncherAdmin.Contracts.ViewModels;

namespace GameLauncherAdmin.ViewModels;

public partial class FrontSettingsViewModel : ObservableRecipient, INavigationAware
{
    private readonly IFrontAppService _frontAppProvider;
    //public CollectionDisplay
    [ObservableProperty]
    private ObservableFrontApp current;
    public Array CollectionDisplays = Enum.GetValues(typeof(CollectionDisplay));
    public Array ItemDisplays = Enum.GetValues(typeof(ItemDisplay));
    public FrontSettingsViewModel(IFrontAppService frontAppProvider)
    {
        _frontAppProvider = frontAppProvider;
    }

    public void OnNavigatedFrom()
    {
    }
    public async void OnNavigatedTo(object parameter)
    {
        await GetData();
    }
    private async Task GetData()
    {
        current = new ObservableFrontApp(_frontAppProvider.GetDefault());
    }
}
