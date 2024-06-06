using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Models;

namespace GameLauncher.ObservableObjet;
public class ObservableMetadataGenre : ObservableRecipient, IObservableBaseGenre
{
    public MetadataGenre Item;
    public ObservableMetadataGenre(MetadataGenre item)
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
}
