using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.APIObject;
public class FullCollectionItem
{
    public Collection Collection
    {
    get; set;}
    public List<ItemInCollection> Items { get; set;}
}public class FullCollectionTrueItem
{
    public Collection Collection
    {
    get; set;}
    public ObservableCollection<TrueItemInCollection> Items { get; set;}
}
public class ItemCompleteInfo : Item
{
    public ItemCompleteInfo(Item item)
    {
        this.ID = item.ID;
        this.Name = item.Name;
        this.Description = item.Description;
        this.LUPlatformesId = item.LUPlatformesId;
        this.LUProfileId = item.LUProfileId;
        this.SearchName = item.SearchName;
        this.Path = item.Path;
        this.IsFavorite = item.IsFavorite;
        this.ReleaseDate = item.ReleaseDate;
        this.AddingDate = item.AddingDate;
        this.Cover = item.Cover;
        this.Logo = item.Logo;
        this.Banner = item.Banner;
        this.Artwork = item.Artwork;
        this.Video = item.Video;
        this.StoreId = item.StoreId;
        this.NbStart = item.NbStart;
        this.LastStartDate = item.LastStartDate;
    }
    public string PlateformeValue
    {
        get; set;
    }
    public string DevelloppeurValue
    {
        get; set;
    }
    public string EditeurValue
    {
        get; set;
    }
    public string GenreValue
    {
        get; set;
    }

}
