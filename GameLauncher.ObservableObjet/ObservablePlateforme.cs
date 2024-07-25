using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Models;

namespace GameLauncher.ObservableObjet;
public class ObservablePlateforme : ObservableObject
{
    public LUPlatformes plateforme;
    public ObservablePlateforme(LUPlatformes Plateforme)
    {
        plateforme = Plateforme;
    }
    public Guid Id
    {
        get => plateforme.Id;
    }
    public string Name
    {
        get => plateforme.Name;
        set
        {
            SetProperty(plateforme.Name, value, plateforme, (syteme, item) => plateforme.Name = item);
        }
    }
    public string Codename
    {
        get => plateforme.Codename;
        set
        {
            SetProperty(plateforme.Codename, value, plateforme, (syteme, item) => plateforme.Codename = item);
        }
    }
}
