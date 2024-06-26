using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Models;
using GameLauncher.Models.ScreenScraper;

namespace GameLauncher.ObservableObjet;
public class ObsCollection : ObservableObject
{
    public Collection _collection;
    public ObsCollection(Collection Collection)
    {
        _collection = Collection;
    }
    public Guid Id
    {
        get => _collection.ID;
    }
    public string Name
    {
        get => _collection.Name;
        set
        {
            SetProperty(_collection.Name, value, _collection, (syteme, item) => _collection.Name = item);
        }
    }
    public string CodeName
    {
        get => _collection.CodeName;
        set
        {
            SetProperty(_collection.CodeName, value, _collection, (syteme, item) => _collection.CodeName = item);
        }
    }
    public string Fanart
    {
        get => _collection.Fanart;
        set
        {
            SetProperty(_collection.Fanart, value, _collection, (syteme, item) => _collection.Fanart = item);
        }
    }
    public string Logo
    {
        get => _collection.Logo;
        set
        {
            SetProperty(_collection.Logo, value, _collection, (syteme, item) => _collection.Logo = item);
        }
    }
    public int Order
    {
        get => _collection.Order;
        set
        {
            SetProperty(_collection.Order, value, _collection, (syteme, item) => _collection.Order = item);
        }
    }
}
