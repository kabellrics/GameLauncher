using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.ViewModels;
using GameLauncherAdmin.Core.Contracts.Services;
using GameLauncherAdmin.Core.Models;

namespace GameLauncherAdmin.ViewModels;

public partial class BibliothequeDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    [ObservableProperty]
    private ObservableItem? item;

    public BibliothequeDetailViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
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

    public void OnNavigatedFrom()
    {
    }
}
