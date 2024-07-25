using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Models;

namespace GameLauncher.ObservableObjet;
public partial class ObservableProfile : ObservableObject
{
    public LUProfile Profile;
    public ObservableProfile(LUProfile profile)
    {
        Profile = profile;
    }
    public string Name
    {
        get => Profile.Name;
        set
        {
            SetProperty(Profile.Name, value, Profile, (syteme, item) => Profile.Name = item);
        }
    }
    public string StartupArguments
    {
        get => Profile.StartupArguments;
        set
        {
            SetProperty(Profile.StartupArguments, value, Profile, (syteme, item) => Profile.StartupArguments = item);
        }
    }
    public string StartupExecutable
    {
        get => Profile.StartupExecutable;
        set
        {
            SetProperty(Profile.StartupExecutable, value, Profile, (syteme, item) => Profile.StartupExecutable = item);
        }
    }
    [ObservableProperty]
    public string _platforms;
    [ObservableProperty]
    public string _imageExtensions;
    [ObservableProperty]
    public string _profileFiles;
}
