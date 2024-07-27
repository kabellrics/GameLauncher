using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.ViewModels;
using Microsoft.UI.Xaml;

namespace GameLauncherAdmin.ViewModels;

public partial class DevelloppeurViewModel : ObservableRecipient, INavigationAware
{
    private readonly ILookupProvider _lookupProvider;
    public ObservableCollection<ObservableDevelloppeur> Source { get; } = new ObservableCollection<ObservableDevelloppeur>();
    public ObservableCollection<ObservableDevelloppeur> FusionList { get; } = new ObservableCollection<ObservableDevelloppeur>();
    [ObservableProperty]
    private ObservableDevelloppeur current;
    [ObservableProperty]
    private ObservableDevelloppeur absorbeItem;
    [ObservableProperty]
    private Visibility _visibilityFusionList;
    private ICommand _saveCommand;
    private ICommand _getFusionListCommand;
    public ICommand SaveCommand
    {
        get
        {
            return _saveCommand ?? (_saveCommand = new RelayCommand(Save));
        }
    }
    public ICommand GetFusionListCommand
    {
        get
        {
            return _getFusionListCommand ?? (_getFusionListCommand = new RelayCommand(GetFusionList));
        }
    }
    private ICommand _refreshCommand;
    public ICommand RefreshCommand
    {
        get
        {
            return _refreshCommand ?? (_refreshCommand = new RelayCommand(Refresh));
        }
    }

    private async void Refresh()
    {
        await GetData();
    }
    public DevelloppeurViewModel(ILookupProvider lookupProvider)
    {
        _lookupProvider = lookupProvider;
        VisibilityFusionList = Visibility.Collapsed;
    }
    public void OnNavigatedFrom()
    {
    }
    public async void OnNavigatedTo(object parameter)
    {
        await GetData();
    }

    private async Task GetData()
    {
        VisibilityFusionList = Visibility.Collapsed;
        Source.Clear();
        FusionList.Clear();
        var devs = await _lookupProvider.GetDevsAsync();
        foreach (var dev in devs.OrderBy(x => x.Name)) Source.Add(new ObservableDevelloppeur(dev));
    }

    private async void Save()
    {
        await _lookupProvider.UpdateDev(Current);
        await _lookupProvider.FusionDev(AbsorbeItem.Id, Current.Id);
        await GetData();
    }
    private void GetFusionList()
    {
        FusionList.Clear();
        VisibilityFusionList = Visibility.Visible;
        if (VisibilityFusionList == Visibility.Visible)
        {
            foreach (var item in Source)
            {
                if (item.Id != Current.Id) { FusionList.Add(item); }
            }
        }
        else
        {
            VisibilityFusionList = Visibility.Collapsed;
        }
    }
}
