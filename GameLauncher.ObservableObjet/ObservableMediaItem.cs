using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GameLauncher.ObservableObjet;
public partial class ObservableMediaItem : ObservableObject
{
    public override string ToString()
    {
        var newdate = DateTime.Parse(Date);
        return $"{Name} ({newdate.Year.ToString()})";
    }
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _date;
    public ObservableMediaItem()
    {
        Logos = new ObservableCollection<string>();
        Covers = new ObservableCollection<string>();
        Artworks = new ObservableCollection<string>();
        Banners = new ObservableCollection<string>();
        Videos = new ObservableCollection<string>();
    }
    public ObservableCollection<string> Logos;
    public ObservableCollection<string> Covers;
    public ObservableCollection<string> Artworks;
    public ObservableCollection<string> Banners;
    public ObservableCollection<string> Videos;
}
