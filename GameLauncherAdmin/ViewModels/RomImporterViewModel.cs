using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.ViewModels;

namespace GameLauncherAdmin.ViewModels;

public partial class RomImporterViewModel : ObservableRecipient, INavigationAware
{
    private readonly IEmulateurProvider _emuProvider;
    [ObservableProperty]
    private bool _expandeEmulatorList;
    [ObservableProperty]
    private bool _expandeProfileEmulatorList;
    public ObservableCollection<ObservableEmulateur> Source { get; } = new ObservableCollection<ObservableEmulateur>();
    public ObservableCollection<ObservableProfile> SourceProfile { get; } = new ObservableCollection<ObservableProfile>();
    public RomImporterViewModel(IEmulateurProvider emuProvider)
    {
        _emuProvider = emuProvider;
    }
    public void OnNavigatedFrom()
    {
    }
    public async void OnNavigatedTo(object parameter)
    {
        await GetAllLocalEmulateurAsync();
    }
    private async Task GetAllLocalEmulateurAsync()
    {
        Source.Clear();
        foreach (var item in await _emuProvider.GetLocalEmusAsync())
        {
            Source.Add(item);
        }
    }
    public async Task GetProfileForEmulateurAsync(string id)
    {

    }
}
