using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Models;

namespace GameLauncher.ObservableObjet
{
    public class ObservableItem : ObservableRecipient
    {
        public Item Item;
        public ObservableItem(Item item)
        {
            Item = item;
            Editeurs = new ObservableCollection<ObservableEditeur>(
                item.Editeurs?.Select(x => new ObservableEditeur(x)) ?? Enumerable.Empty<ObservableEditeur>());
            Develloppeurs = new ObservableCollection<ObservableDevelloppeur>(
                item.Develloppeurs?.Select(x => new ObservableDevelloppeur(x)) ?? Enumerable.Empty<ObservableDevelloppeur>());
            Genres = new ObservableCollection<IObservableBaseGenre>();
            if (item.Genres != null)
            {
                foreach (var genre in item.Genres)
                {
                    Genres.Add(new ObservableGenre(genre));
                }
            }

            if (item.MetadataGenres != null)
            {
                foreach (var metagenre in item.MetadataGenres)
                {
                    Genres.Add(new ObservableMetadataGenre(metagenre));
                }
            }
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
        public LUPlatformes Platforme
        {
            get => Item.Platformes;
            set
            {
                SetProperty(Item.Platformes, value, Item, (syteme, item) => Item.Platformes = item);
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
                if(string.IsNullOrEmpty(Item.Cover)) return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
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
                if (string.IsNullOrEmpty(Item.Cover)) return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
                return Item.Logo;
        }
            set
            {
                SetProperty(Item.Logo, value, Item, (syteme, item) => Item.Logo = item);
            }
        }
        public string Banner
        {
            get {
                if (string.IsNullOrEmpty(Item.Cover)) return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
                return Item.Banner;
            }
            set
            {
                SetProperty(Item.Banner, value, Item, (syteme, item) => Item.Banner = item);
            }
        }
        public string Artwork
        {
            get {
                if (string.IsNullOrEmpty(Item.Cover)) return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
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
        public ObservableCollection<IObservableBaseGenre> Genres;
    }
}
