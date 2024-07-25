using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Models;

namespace GameLauncher.ObservableObjet;
public class ObservableEmulateur : ObservableObject
{
    public LUEmulateur Emulateur;
    public ObservableEmulateur(LUEmulateur emulateur)
    {
        Emulateur = emulateur;
    }
    public String Id
    {
        get => Emulateur.Id;
    }
    public string Name
    {
        get => Emulateur.Name;
        set
        {
            SetProperty(Emulateur.Name, value, Emulateur, (syteme, item) => Emulateur.Name = item);
        }
    }
    public string Website
    {
        get => Emulateur.Website;
        set
        {
            SetProperty(Emulateur.Website, value, Emulateur, (syteme, item) => Emulateur.Website = item);
        }
    }
}
