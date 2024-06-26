using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Contracts.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.ViewModels;

public partial class CollectionDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ICollectionProvider _collecProvider;
    private readonly IItemProvider _itemProvider;
    [ObservableProperty]
    private ObsCollection _collection;
    public ObservableCollection<ObservableItemInCollection> ItemCollections { get; } = new ObservableCollection<ObservableItemInCollection>();
    public List<String> SortType = new List<string> { "Nom Ascendant", "Nom Descendant", "Date de sortie Ascendant", "Date de sortie descendante", "Date d'ajout ascendant", "Date d'ajout descendant" };
    public CollectionDetailViewModel(INavigationService navigationService, ICollectionProvider collecProvider, IItemProvider itemProvider)
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
        if (parameter is ObsCollection detailitem)
        {
            //var data = await _sampleDataService.GetContentGridDataAsync();
            //Item = data.First(i => i.OrderID == orderID);
            Collection = detailitem;
            //await foreach (var item in _collecProvider.GetAllItemInsideAsyncStream(Collection.Id)) 
            await foreach (var item in _collecProvider.GetAllItemInsideAsyncStream(Collection.Id)) 
            {
                //ItemCollections.Insert(item.Order,item);
                ItemCollections.Add(item);
            }
        }
    }
    public void SortObs(string sortype)
    {
        var Itemscollec = new List<ObservableItemInCollection>();
        switch (sortype)
        {
            case "Nom Ascendant": Itemscollec = ItemCollections.OrderBy(x=>x.Name).ToList();
                break;
            case "Nom Descendant": Itemscollec = ItemCollections.OrderByDescending(x=>x.Name).ToList();
                break;
            case "Date de sortie Ascendant": Itemscollec = ItemCollections.OrderBy(x=>x.ReleaseDate).ToList();
                break;
            case "Date de sortie descendante": Itemscollec = ItemCollections.OrderBy(x=>x.ReleaseDate).ToList();
                break;
            case "Date d'ajout ascendant": Itemscollec = ItemCollections.OrderBy(x=>x.AddingDate).ToList();
                break;
            case "Date d'ajout descendant": Itemscollec = ItemCollections.OrderByDescending(x=>x.AddingDate).ToList();
                break;
        }
        int order = 1;
        ItemCollections.Clear();
        foreach (var item in Itemscollec)
        {
            item.Order = order++;
            ItemCollections.Add(item);
        }
    }
    public void ReInitOrder()
    {
        int order = 1;
        foreach (var item in ItemCollections)
        {
            item.Order = order++;
        }
    }
}
