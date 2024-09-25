using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Models.APIObject;

namespace GameLauncher.Front.ViewModels.Observable;
public class ObsItem : ObservableObject
{
    public ItemCompleteInfo Item;
    public override string ToString() => Name;

    public ObsItem(ItemCompleteInfo item)
    {
        Item = item;
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
    public string SearchName
    {
        get => Item.SearchName;
        set
        {
            SetProperty(Item.SearchName, value, Item, (syteme, item) => Item.SearchName = item);
        }
    }
    public string DevelloppeurValue
    {
        get => Item.DevelloppeurValue;
        set
        {
            SetProperty(Item.DevelloppeurValue, value, Item, (syteme, item) => Item.DevelloppeurValue = item);
        }
    }
    public string EditeurValue
    {
        get => Item.EditeurValue;
        set
        {
            SetProperty(Item.EditeurValue, value, Item, (syteme, item) => Item.EditeurValue = item);
        }
    }
    public string PlateformeValue
    {
        get => Item.PlateformeValue;
        set
        {
            SetProperty(Item.PlateformeValue, value, Item, (syteme, item) => Item.PlateformeValue = item);
        }
    }
    public string PlateformeLogo
    {
        get
        {
            if (string.IsNullOrEmpty(Item.LUPlatformesId)) return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
            return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "Assets", "Collection V2", $"{Item.LUPlatformesId}.png");
        }
    }
    public string GenreValue
    {
        get => Item.GenreValue;
        set
        {
            SetProperty(Item.GenreValue, value, Item, (syteme, item) => Item.GenreValue = item);
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
}
