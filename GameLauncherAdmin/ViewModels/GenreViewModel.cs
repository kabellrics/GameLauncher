using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLauncher.AdminProvider;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.ViewModels;
using Microsoft.UI.Xaml;

namespace GameLauncherAdmin.ViewModels;

public partial class GenreViewModel : ObservableRecipient, INavigationAware
{
    private readonly ILookupProvider _lookupProvider;
    public ObservableCollection<ObservableGenre> Source { get; } = new ObservableCollection<ObservableGenre>();
    public ObservableCollection<ObservableGenre> FusionList { get; } = new ObservableCollection<ObservableGenre>();
    [ObservableProperty]
    private ObservableGenre current;
    [ObservableProperty]
    private ObservableGenre absorbeItem;
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
    public GenreViewModel(ILookupProvider lookupProvider)
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
        var genres = await _lookupProvider.GetGenresAsync();
        foreach (var genre in genres.OrderBy(x => x.Name)) Source.Add(new ObservableGenre(genre));
    }

    private async void Save()
    {
        await _lookupProvider.UpdateGenre(Current);
        await _lookupProvider.FusionGenre(AbsorbeItem.Id, Current.Id);
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
