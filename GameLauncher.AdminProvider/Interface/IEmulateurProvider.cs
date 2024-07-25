using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.ObservableObjet;

namespace GameLauncher.AdminProvider.Interface;
public interface IEmulateurProvider
{
    Task<IEnumerable<ObservableEmulateur>> GetLocalEmusAsync();
    IAsyncEnumerable<ObservableProfile> GetLocalProfilesAsync();
    Task<IEnumerable<ObservableEmulateur>> ScanFolder(string folder);
    IAsyncEnumerable<ObservablePlateforme> GetPlatfomsFromProfile(LUProfile lUProfile);
    Task ScanWithProfile(ScanProfile profile);
}