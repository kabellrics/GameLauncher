using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;

namespace GameLauncher.ObservableObjet;
public partial class ObservableScannerProfile: ObservableObject
{
    public ObservableScannerProfile()
    {
        MetaProvider = MetadataProvider.Screenscraper;
        LogoProvider = LogoProvider.Screenscraper;
        CoverProvider = CoverProvider.SteamGridDB;
        FanartProvider = FanartProvider.Screenscraper;
        VideoProvider = VideoProvider.Screenscraper;
    }
    public ScanProfile ExportScanProfile()
    {
        ScanProfile scanProfile = new ScanProfile();
        scanProfile.FolderPath = FolderPath;
        scanProfile.Profile = Profile;
        scanProfile.Platforms = Platforms;
        scanProfile.MetaProvider = MetaProvider;
        scanProfile.LogoProvider = LogoProvider;
        scanProfile.CoverProvider = CoverProvider;
        scanProfile.VideoProvider = VideoProvider;
        scanProfile.FanartProvider = FanartProvider;
        return scanProfile;
    }
    [ObservableProperty]
    private string _folderPath;
    [ObservableProperty]
    private LUProfile _profile;
    [ObservableProperty]
    private LUPlatformes _platforms;
    [ObservableProperty]
    private MetadataProvider _metaProvider;
    [ObservableProperty]
    private List<MetadataProvider> _metadataProviders = Enum.GetValues<MetadataProvider>().ToList();
    [ObservableProperty]
    private LogoProvider _logoProvider;
    [ObservableProperty]
    private List<LogoProvider> _logoProviders = Enum.GetValues<LogoProvider>().ToList();
    [ObservableProperty]
    private CoverProvider _coverProvider;
    [ObservableProperty]
    private List<CoverProvider> _coverProviders = Enum.GetValues<CoverProvider>().ToList();
    [ObservableProperty]
    private FanartProvider _fanartProvider;
    [ObservableProperty]
    private List<FanartProvider> _fanartProviders = Enum.GetValues<FanartProvider>().ToList();
    [ObservableProperty]
    private VideoProvider _videoProvider;
    [ObservableProperty]
    private List<VideoProvider> _videoProviders = Enum.GetValues<VideoProvider>().ToList();
}
