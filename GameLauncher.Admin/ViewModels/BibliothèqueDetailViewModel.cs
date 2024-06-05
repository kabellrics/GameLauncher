using CommunityToolkit.Mvvm.ComponentModel;

using GameLauncher.Admin.Contracts.ViewModels;
using GameLauncher.Admin.Core.Contracts.Services;
using GameLauncher.Admin.Core.Models;

namespace GameLauncher.Admin.ViewModels;

public partial class BibliothèqueDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    [ObservableProperty]
    private SampleOrder? item;

    public BibliothèqueDetailViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is long orderID)
        {
            var data = await _sampleDataService.GetContentGridDataAsync();
            Item = data.First(i => i.OrderID == orderID);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
