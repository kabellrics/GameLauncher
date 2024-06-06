using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GameLauncher.Admin.Contracts.Services;
using GameLauncher.Admin.Contracts.ViewModels;
using GameLauncher.Admin.Core.Contracts.Services;
using GameLauncher.Admin.Core.Models;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Models.ScreenScraper;
using GameLauncher.ObservableObjet;

namespace GameLauncher.Admin.ViewModels;

public partial class BibliothèqueViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ISampleDataService _sampleDataService;
    private readonly IItemProvider _itemProvider;

    public ObservableCollection<ObservableItem> Source { get; } = new ObservableCollection<ObservableItem>();

    public BibliothèqueViewModel(INavigationService navigationService, ISampleDataService sampleDataService, IItemProvider itemProvider)
    {
        _navigationService = navigationService;
        _sampleDataService = sampleDataService;
        _itemProvider = itemProvider;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // TODO: Replace with real data.
        //var data = await _sampleDataService.GetContentGridDataAsync();
        await InitializeData(_itemProvider.GetAllItems());
        //foreach (var item in data)
        //{
        //    Source.Add(item);
        //}
    }

    private async Task InitializeData(IAsyncEnumerable<ObservableItem> asyncitems)
    {
        var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        await Task.Run(async () =>
        {
            await foreach (var item in asyncitems)
            {
                dispatcherQueue.TryEnqueue(() =>
                {
                    Source.Add(item);
                });
            }
        });
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private void OnItemClick(ObservableItem? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(BibliothèqueDetailViewModel).FullName!, clickedItem);
        }
    }
}
