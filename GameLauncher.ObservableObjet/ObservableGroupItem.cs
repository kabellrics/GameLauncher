using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GameLauncher.ObservableObjet;
public class ObservableGroupItem : ObservableRecipient
{
    private string _groupname;
    public string GroupName
    {
        get => _groupname;
        set
        {
            SetProperty(ref _groupname, value);
        }
    }
    public ObservableCollection<ObservableItem> Items;
}
