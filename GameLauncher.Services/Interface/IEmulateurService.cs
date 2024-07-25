using GameLauncher.Models;
using GameLauncher.Models.APIObject;

namespace GameLauncher.Services.Interface;

public interface IEmulateurService
{
    IEnumerable<LUEmulateur> ScanFolderForEmulator(string directoryPath);
    IAsyncEnumerable<LUEmulateur> RecursiveScanAsync(string directoryPath);
    IEnumerable<LUEmulateur> RecursiveScan(string directoryPath);
    IEnumerable<LUEmulateur> GetLocalEmulator();
    IEnumerable<LUProfile> GetLocalEmulatorProfile();
    Task ScanFolderForRom(ScanProfile scanprofile);
}