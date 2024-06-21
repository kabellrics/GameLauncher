using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLauncher.AdminProvider.Interface;

namespace GameLauncherAdmin.ViewModels;

public partial class StoreImporterViewModel : ObservableRecipient
{
    private readonly IStoreProvider _storeProvider;
    public StoreImporterViewModel(IStoreProvider storeProvider)
    {
        _storeProvider = storeProvider;
    }

    private ICommand _scanSteamCommand;
    private ICommand _scanEpicCommand;
    private ICommand _scanEAPlayCommand;
    private ICommand _cleanSteamCommand;
    private ICommand _cleanEpicCommand;
    private ICommand _cleanEAPlayCommand;
    public ICommand ScanSteamCommand
    {
        get
        {
            return _scanSteamCommand ?? (_scanSteamCommand = new RelayCommand(ScanSteam));
        }
    }
    public ICommand ScanEpicCommand
    {
        get
        {
            return _scanEpicCommand ?? (_scanEpicCommand = new RelayCommand(ScanEpic));
        }
    }
    public ICommand ScanEAPlayCommand
    {
        get
        {
            return _scanEAPlayCommand ?? (_scanEAPlayCommand = new RelayCommand(ScanEAPlay));
        }
    }
    public ICommand CleanSteamCommand
    {
        get
        {
            return _cleanSteamCommand ?? (_cleanSteamCommand = new RelayCommand(CleanSteam));
        }
    }
    public ICommand CleanEpicCommand
    {
        get
        {
            return _cleanEpicCommand ?? (_cleanEpicCommand = new RelayCommand(CleanEpic));
        }
    }
    public ICommand CleanEAPlayCommand
    {
        get
        {
            return _cleanEAPlayCommand ?? (_cleanEAPlayCommand = new RelayCommand(CleanEAPlay));
        }
    }

    private async void ScanSteam() {await _storeProvider.GetSteamGameAsync();}
    private async void ScanEpic() {await _storeProvider.GetEpicGameAsync();}
    private async void ScanEAPlay() {await _storeProvider.GetEAOriginGameAsync();}
    private async void CleanSteam() {await _storeProvider.CleaningSteamGameAsync();}
    private async void CleanEpic() {await _storeProvider.CleaningEpicGameAsync();}
    private async void CleanEAPlay() {await _storeProvider.CleaningEAGameAsync();}
}
