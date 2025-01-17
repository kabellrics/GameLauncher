using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;

namespace GameLauncher.ObservableObjet;
public class ObservableFrontApp : ObservableObject
{
    public FrontApp Item;
    public ObservableFrontApp(FrontApp item)
    {
        Item = item;
    }
    public int Id
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
    public string Path
    {
        get => Item.Path;
        set
        {
            SetProperty(Item.Path, value, Item, (syteme, item) => Item.Path = item);
        }
    }
    public CollectionDisplay CollectionDisplay
    {
        get => Item.CollectionDisplay;
        set
        {
            SetProperty(Item.CollectionDisplay, value, Item, (syteme, item) => Item.CollectionDisplay = item);
        }
    }
    public ItemDisplay ItemDisplay
    {
        get => Item.ItemDisplay;
        set
        {
            SetProperty(Item.ItemDisplay, value, Item, (syteme, item) => Item.ItemDisplay = item);
        }
    }
}
