using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Models.APIObject;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GameLauncher.Front.Helpers;
public class ItemLayoutSelector : DataTemplateSelector
{
    public DataTemplate? Default
    {
        get; set;
    }
    public DataTemplate? Artwork
    {
        get; set;
    }
    public DataTemplate? SteamLike
    {
        get; set;
    }
    protected override DataTemplate? SelectTemplateCore(object item, DependencyObject container)
    {
        if (item is ItemDisplay templateType)
        {
            return templateType switch
            {
                ItemDisplay.Defaut => Default,
                ItemDisplay.ArtworkNoDesc => Artwork,
                ItemDisplay.SteamLike => SteamLike,
                _ => null
            };
        }
        return Default;
    }
}
