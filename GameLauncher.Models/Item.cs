using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameLauncher.Models;
public class Item
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID
    {
        get; set;
    }
    public string LUPlatformesId
    {
        get; set;
    }
    public Guid? LUProfileId
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    public string SearchName
    {
        get; set;
    }
    public string Path
    {
        get; set;
    }
    public string Description
    {
        get; set;
    }
    public bool IsFavorite
    {
        get; set;
    }
    public DateTime ReleaseDate
    {
        get; set;
    }
    public DateTime AddingDate
    {
        get; set;
    }
    public List<ItemDev>? Develloppeurs
    {
        get; set;
    }
    public List<ItemEditeur>? Editeurs
    {
        get; set;
    }
    public List<ItemGenre>? Genres
    {
        get; set;
    }
    public string Cover
    {
        get; set;
    }
    public string Logo
    {
        get; set;
    }
    public string Banner
    {
        get; set;
    }
    public string Artwork
    {
        get; set;
    }
    public string Video
    {
        get; set;
    }
    public string StoreId
    {
        get; set;
    }
    public int NbStart
    {
        get; set;
    }
    public DateTime LastStartDate
    {
        get; set;
    }

    [JsonIgnore]
    public List<CollectionItem>? Collections
    {
        get; set;
    }
}
