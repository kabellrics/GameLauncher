using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Models.APIObject;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.ViewModels;

namespace GameLauncherAdmin.ViewModels;

public partial class EmulateurImporterViewModel : ObservableRecipient, INavigationAware
{
    private readonly IEmulateurProvider _emuProvider;
    [ObservableProperty]
    private bool _expandeEmulatorList;
    [ObservableProperty]
    private bool _expandeProfileEmulatorList;
    [ObservableProperty]
    private bool _expandeProfilePlateformList;
    [ObservableProperty]
    private bool _expandeFinalStartScanning;
    [ObservableProperty]
    private ObservableScannerProfile _scanningProfile;
    [ObservableProperty]
    private ObservableEmulateur _choosenEmulator;
    public ObservableCollection<ObservableEmulateur> Source { get; } = new ObservableCollection<ObservableEmulateur>();
    public ObservableCollection<ObservableProfile> SourceProfile { get; } = new ObservableCollection<ObservableProfile>();
    public ObservableCollection<ObservablePlateforme> SourcePlateforme { get; } = new ObservableCollection<ObservablePlateforme>();
    private ICommand _scanEmulatorCommand;
    public ICommand ScanEmulatorCommand
    {
        get
        {
            return _scanEmulatorCommand ?? (_scanEmulatorCommand = new RelayCommand<string>(ScanEmulator));
        }
    }
    private ICommand _cancelCommand;
    public ICommand CancelCommand
    {
        get
        {
            return _cancelCommand ?? (_cancelCommand = new RelayCommand(Cancel));
        }
    }
    private ICommand _startScanCommand;
    public ICommand StartScanCommand
    {
        get
        {
            return _startScanCommand ?? (_startScanCommand = new RelayCommand(StartScan));
        }
    }


    public EmulateurImporterViewModel(IEmulateurProvider emuProvider)
    {
        _emuProvider = emuProvider;
        ScanningProfile = new ObservableScannerProfile();

    }
    public void OnNavigatedFrom()
    {
    }
    public async void OnNavigatedTo(object parameter)
    {
        ExpandeEmulatorList = true;
        ExpandeProfileEmulatorList = false;
        ExpandeProfilePlateformList = false;
        ExpandeFinalStartScanning = false;
        await GetAllLocalEmulateurAsync();
        SourcePlateforme.Clear();
    }

    private void Cancel()
    {
        ExpandeEmulatorList = true;
        ExpandeProfileEmulatorList = false;
        ExpandeProfilePlateformList = false;
        ExpandeFinalStartScanning = false;
        SourceProfile.Clear();
        ScanningProfile = new ObservableScannerProfile();
    }
    public async void ScanEmulator(string? obj)
    {
        var updateTask = _emuProvider.ScanFolder(obj);
    }
    private async Task GetAllLocalEmulateurAsync()
    {
            Source.Clear();
            foreach (var item in await _emuProvider.GetLocalEmusAsync())
            {
                Source.Add(item);
            }
    }
    public async Task GetProfileForEmulateurAsync(ObservableEmulateur eum)
    {
        ExpandeEmulatorList = false;
        ExpandeProfileEmulatorList = true;
        ExpandeProfilePlateformList = false;
        ExpandeFinalStartScanning = false;
        SourceProfile.Clear();
        ChoosenEmulator = eum;
        await foreach(var item in _emuProvider.GetLocalProfilesAsync())
        {
            if (item.Profile.LUEmulateurId == eum.Id)
                SourceProfile.Add(item);
        }
    }
    public async Task GetPlateformeForProfileAsync(ObservableProfile profile)
    {
        ExpandeEmulatorList = false;
        ExpandeProfileEmulatorList = false;
        ExpandeProfilePlateformList = true;
        ExpandeFinalStartScanning = false;
        SourcePlateforme.Clear();
        ScanningProfile.Profile = profile.Profile;
        await foreach(var item in _emuProvider.GetPlatfomsFromProfile(profile.Profile))
        {
                SourcePlateforme.Add(item);
        }
    }
    public async Task PrepareScanningProfileAsync(ObservablePlateforme platforme)
    {
        ExpandeEmulatorList = false;
        ExpandeProfileEmulatorList = false;
        ExpandeProfilePlateformList = false;
        ExpandeFinalStartScanning = true;
        ScanningProfile.Platforms = platforme.plateforme;
    }
    public async void StartScan()
    {
        await _emuProvider.ScanWithProfile(ScanningProfile.ExportScanProfile());
    }
}
