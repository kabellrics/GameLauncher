using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Models;
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
    public ObservableCollection<ObservableItemInCollection> ToDeleteItems { get; } = new ObservableCollection<ObservableItemInCollection>();
    public ObservableCollection<ObservableItem> AddingCollections { get; } = new ObservableCollection<ObservableItem>();
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
            Collection = detailitem;
            ItemCollections.Clear();
            ItemCollections.Clear();
            ToDeleteItems.Clear();

            await foreach (var item in _collecProvider.GetAllItemInsideAsyncStream(Collection.Id)) 
            {
                ItemCollections.Add(item);
            }
            await foreach( var item in _itemProvider.GetAllItemsStream())
            {
                if (!ItemCollections.Any(x => x.Id == item.Id))
                    AddingCollections.Add(item);
            }
        }
    }
    public void AddToCollec(IEnumerable<ObservableItem> items)
    {
        var order = ItemCollections.Max(x => x.Order);
        foreach (var item in items)
        {
            CollectionItem collecitem = new CollectionItem() { ID = Guid.NewGuid(), CollectionID = Collection.Id, ItemID = item.Id, Order = order++ };
            ItemCollections.Add(new ObservableItemInCollection(item.Item,collecitem));
        }
        ReInitOrder();
    }
    public void RemoveToCollec(IEnumerable<ObservableItemInCollection> items)
    {
        foreach (var item in items)
        {
            ToDeleteItems.Add(item);
            ItemCollections.Remove(item);
        }
        ReInitOrder();
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
    public async void SaveChanges()
    {
        ReInitOrder();
        await _collecProvider.UpdateCollection(_collection);
        foreach(var item in ItemCollections)
        {
            await _collecProvider.UpdateCollectionItemOrder(item.CollectionItem.CollectionID,item.CollectionItem.ItemID,item.Order);
        }
        foreach (var item in ToDeleteItems)
        {
            await _collecProvider.DeleteCollectionItem(item.Id);
        }
        _navigationService.GoBack();
    }
    public void SetNewLogoPath(string newLogoPath)
    {
        Collection.Logo = newLogoPath;
    }
    public void SetNewFanartPath(string newFanartPath)
    {
        Collection.Logo = newFanartPath;
    }
}
