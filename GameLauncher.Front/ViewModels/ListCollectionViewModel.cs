using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLauncher.Front.Contracts.Services;
using GameLauncher.Front.Contracts.ViewModels;
using GameLauncher.Front.ViewModels.Observable;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface.Front;
using SharpDX.Multimedia;
using Windows.Services.Maps;

namespace GameLauncher.Front.ViewModels;

public partial class ListCollectionViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ICollectionService _collectionService;
    private readonly IStartingService _startingService;
    public ObservableCollection<FullCollectionTrueItem> Source { get; } = new ObservableCollection<FullCollectionTrueItem>();

    public ObservableCollection<ObsCollection> SourceCollection { get; } = new ObservableCollection<ObsCollection>();
    public ObservableCollection<ObsItem> SourceItem { get; } = new ObservableCollection<ObsItem>();

    [ObservableProperty]
    private ObsItem _currentItem;
    [ObservableProperty]
    private int _currentItemListIndex;
    [ObservableProperty]
    private int _currentCollectionListIndex;
    public ListCollectionViewModel(INavigationService navigationService, ICollectionService collectionService, IStartingService startingService)
    {
        _navigationService = navigationService;
        _collectionService = collectionService;
        _startingService = startingService;
    }
    public void OnNavigatedFrom()
    {
    }
    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is Task<IEnumerable<FullCollectionTrueItem>> tache)
        {
            var items = await tache;
            await GetAllCollectionAsync(items);
        }
    }
    private async Task GetAllCollectionAsync(IEnumerable<FullCollectionTrueItem> collec)
    {
        Source.Clear();
        SourceCollection.Clear(); var isFirstIteration = true;
        //var collec = await _apiService.GetFullCollection();
        foreach (var item in collec)
        {
            try
            {
                Source.Add(item);
            }
            catch (Exception ex)
            {
                //throw;
            }
            SourceCollection.Add(new ObsCollection(item.Collection));
            if (isFirstIteration)
            {
                CurrentCollectionListIndex = 0;
                InitListItem(SourceCollection.FirstOrDefault());
                isFirstIteration = false;
            }
            await Task.Delay(100);
        }
    }
    public async Task GotoDetail(ItemCompleteInfo item)
    {
        _navigationService.NavigateTo(typeof(ItemDetailViewModel).FullName!, item);
    }
    public async Task StartItem(ItemCompleteInfo item)
    {
        await _startingService.StartITem(item.ID);
    }
    [RelayCommand]
    private void OnItemObsCollectionClick(ObsCollection? clickedItem)
    {
        if (clickedItem != null)
        {
            InitListItem(clickedItem);
        }
    }
    [RelayCommand]
    private void OnItemObsItemClick(ObsItem? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.NavigateTo(typeof(ItemDetailViewModel).FullName!, clickedItem);
        }
    }

    private async void InitListItem(ObsCollection? clickedItem)
    {
        if (clickedItem != null)
        {
            SourceItem.Clear(); var isFirstIteration = true;
            var items = Source.First(x => x.Collection.ID == clickedItem.Id).Items;
            foreach (var item in items)
            {
                SourceItem.Add(new ObsItem(item.Item));
                if (isFirstIteration)
                {
                    CurrentItemListIndex = 0;
                    CurrentItem = SourceItem.First();
                }
                await Task.Delay(100);
            }

        }
    }
}
