using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.ViewModels;
using Microsoft.UI.Xaml;

namespace GameLauncherAdmin.ViewModels;

public partial class EditeurViewModel : ObservableRecipient, INavigationAware
{
    private readonly ILookupProvider _lookupProvider;
    public ObservableCollection<ObservableEditeur> Source { get; } = new ObservableCollection<ObservableEditeur>();
    public ObservableCollection<ObservableEditeur> FusionList { get; } = new ObservableCollection<ObservableEditeur>();
    [ObservableProperty]
    private ObservableEditeur current;
    [ObservableProperty]
    private ObservableEditeur absorbeItem;
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
    public EditeurViewModel(ILookupProvider lookupProvider)
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
        var editeurs = await _lookupProvider.GetEditeursAsync();
        foreach (var editeur in editeurs.OrderBy(x => x.Name)) Source.Add(new ObservableEditeur(editeur));
    }

    private async void Save()
    {
        await _lookupProvider.UpdateEditeur(Current);
        await _lookupProvider.FusionEditeur(AbsorbeItem.Id, Current.Id);
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
