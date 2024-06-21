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
    public Collection Collection;
    public override string ToString() => Name;
    public ObsCollection(Collection Collection)
    {
        Collection = Collection;
    }
    public Guid Id
    {
        get => Collection.ID;
    }
    public string Name
    {
        get => Collection.Name;
        set
        {
            SetProperty(Collection.Name, value, Collection, (syteme, item) => Collection.Name = item);
        }
    }
    public string CodeName
    {
        get => Collection.CodeName;
        set
        {
            SetProperty(Collection.CodeName, value, Collection, (syteme, item) => Collection.CodeName = item);
        }
    }
    public string Fanart
    {
        get => Collection.Fanart;
        set
        {
            SetProperty(Collection.Fanart, value, Collection, (syteme, item) => Collection.Fanart = item);
        }
    }
    public string Logo
    {
        get => Collection.Logo;
        set
        {
            SetProperty(Collection.Logo, value, Collection, (syteme, item) => Collection.Logo = item);
        }
    }
    public int Order
    {
        get => Collection.Order;
        set
        {
            SetProperty(Collection.Order, value, Collection, (syteme, item) => Collection.Order = item);
        }
    }
}
