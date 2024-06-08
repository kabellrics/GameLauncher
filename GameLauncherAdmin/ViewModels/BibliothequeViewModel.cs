using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Contracts.ViewModels;
using GameLauncherAdmin.Core.Contracts.Services;
using GameLauncherAdmin.Core.Models;

namespace GameLauncherAdmin.ViewModels;

public partial class BibliothequeViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ISampleDataService _sampleDataService;
    private readonly IItemProvider _itemProvider;

    public ObservableCollection<ObservableItem> Source { get; } = new ObservableCollection<ObservableItem>();
    public ObservableGroupedCollection<string, ObservableItem> GroupedItems{get; private set;} = new();
    public BibliothequeViewModel(INavigationService navigationService, ISampleDataService sampleDataService, IItemProvider itemProvider)
    {
        _navigationService = navigationService;
        _sampleDataService = sampleDataService;
        _itemProvider = itemProvider;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();
        GroupedItems.Clear();
        var obsitems = await _itemProvider.GetAllItemsAsync();
        GroupedItems = new ObservableGroupedCollection<string, ObservableItem>(
            obsitems.GroupBy(x => x.Platforme.Name).OrderBy(g => g.Key)
            );
        OnPropertyChanged(nameof(GroupedItems));
        await InitializeData(_itemProvider.GetAllItemsAsyncEnumerable());
        //// TODO: Replace with real data.
        //var data = await _sampleDataService.GetContentGridDataAsync();
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
            //_navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(BibliothequeDetailViewModel).FullName!, clickedItem);
        }
    }
}
