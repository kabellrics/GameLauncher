using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Models;

namespace GameLauncher.ObservableObjet;
public partial class ObservableItemInCollection : ObservableObject
{
    public Item Item
    {
        get; set;
    }
    public CollectionItem CollectionItem
    {
        get; set;
    }
    public ObservableItemInCollection(Item item, CollectionItem collectionItem)
    {
        Item = item;
        CollectionItem = collectionItem;
        Editeurs = new ObservableCollection<ObservableEditeur>();
        Develloppeurs = new ObservableCollection<ObservableDevelloppeur>();
        Genres = new ObservableCollection<ObservableGenre>();
    }
    public Guid Id
    {
        get => Item.ID;
    }
    public string Name
    {
        get => Item.Name;
        set
        {
            SetProperty(Item.Name, value, Item, (syteme, item) => Item.Name = item);
        }
    }
    public int Order
    {
        get => CollectionItem.Order;
        set
        {
            SetProperty(CollectionItem.Order, value, CollectionItem, (syteme, item) => CollectionItem.Order = item);
        }
    }
    [ObservableProperty]
    private LUPlatformes _platforme;
    public string SearchName
    {
        get => Item.SearchName;
        set
        {
            SetProperty(Item.SearchName, value, Item, (syteme, item) => Item.SearchName = item);
        }
    }
    public string Path
    {
        get => Item.Path;
        set
        {
            SetProperty(Item.Path, value, Item, (syteme, item) => Item.Path = item);
        }
    }
    public string Description
    {
        get => Item.Description;
        set
        {
            SetProperty(Item.Description, value, Item, (syteme, item) => Item.Description = item);
        }
    }
    public string StoreId
    {
        get => Item.StoreId;
        set
        {
            SetProperty(Item.StoreId, value, Item, (syteme, item) => Item.StoreId = item);
        }
    }
    public string Cover
    {
        get
        {
            if (string.IsNullOrEmpty(Item.Cover)) return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
            return Item.Cover;
        }
        set
        {
            SetProperty(Item.Cover, value, Item, (syteme, item) => Item.Cover = item);
        }
    }
    public string Logo
    {
        get
        {
            if (string.IsNullOrEmpty(Item.Logo)) return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
            return Item.Logo;
        }
        set
        {
            SetProperty(Item.Logo, value, Item, (syteme, item) => Item.Logo = item);
        }
    }
    public string Banner
    {
        get
        {
            if (string.IsNullOrEmpty(Item.Banner)) return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
            return Item.Banner;
        }
        set
        {
            SetProperty(Item.Banner, value, Item, (syteme, item) => Item.Banner = item);
        }
    }
    public string Artwork
    {
        get
        {
            if (string.IsNullOrEmpty(Item.Artwork)) return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
            return Item.Artwork;
        }
        set
        {
            SetProperty(Item.Artwork, value, Item, (syteme, item) => Item.Artwork = item);
        }
    }
    public string Video
    {
        get => Item.Video;
        set
        {
            SetProperty(Item.Video, value, Item, (syteme, item) => Item.Video = item);
        }
    }
    public bool IsFavorite
    {
        get => Item.IsFavorite;
        set
        {
            SetProperty(Item.IsFavorite, value, Item, (syteme, item) => Item.IsFavorite = item);
        }
    }
    public DateTime ReleaseDate
    {
        get => Item.ReleaseDate;
        set
        {
            SetProperty(Item.ReleaseDate, value, Item, (syteme, item) => Item.ReleaseDate = item);
        }
    }
    public DateTime AddingDate
    {
        get => Item.AddingDate;
        set
        {
            SetProperty(Item.AddingDate, value, Item, (syteme, item) => Item.AddingDate = item);
        }
    }
    public DateTime LastStartDate
    {
        get => Item.LastStartDate;
        set
        {
            SetProperty(Item.LastStartDate, value, Item, (syteme, item) => Item.LastStartDate = item);
        }
    }
    public int NbStart
    {
        get => Item.NbStart;
        set
        {
            SetProperty(Item.NbStart, value, Item, (syteme, item) => Item.NbStart = item);
        }
    }

    public ObservableCollection<ObservableEditeur> Editeurs;
    public ObservableCollection<ObservableDevelloppeur> Develloppeurs;
    public ObservableCollection<ObservableGenre> Genres;
}
