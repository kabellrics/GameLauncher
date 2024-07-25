using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Connector;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Models.EpicGame;
using GameLauncher.ObservableObjet;
using RestSharp;

namespace GameLauncher.AdminProvider;
public class EmulateurProvider : IEmulateurProvider
{
    private readonly EmulatorConnector emuconnector;
    private readonly LookupConnector lookconnector;
    public EmulateurProvider()
    {
        emuconnector = new EmulatorConnector("https://localhost:7197");
        lookconnector = new LookupConnector("https://localhost:7197");
    }
    public async Task<IEnumerable<ObservableEmulateur>> GetLocalEmusAsync()
    {
        var rep = await emuconnector.GetLocalEmusAsync();
        return rep.Select(x=> new ObservableEmulateur(x));
    }
    public async IAsyncEnumerable<ObservableProfile> GetLocalProfilesAsync()
    {
        var rep = await emuconnector.GetLocalProfilesAsync();
        foreach (var profile in rep) {
            if(profile.IsLocal) 
            yield return await FromProfile(profile);
        }
    }
    public async Task<IEnumerable<ObservableEmulateur>> ScanFolder(string folder)
    {
        var rep = await emuconnector.ScanFolder(folder);
        return rep.Select(x => new ObservableEmulateur(x));
    }
    private async Task<ObservableProfile> FromProfile(LUProfile lUProfile)
    {
        ObservableProfile profile = new ObservableProfile(lUProfile);
        if(lUProfile.ImageExtensions != null && lUProfile.ImageExtensions.Count() >0)
        profile.ImageExtensions = string.Join(",",lUProfile.ImageExtensions.ToList());
        if (lUProfile.ProfileFiles != null && lUProfile.ProfileFiles.Count() > 0)
            profile.ProfileFiles = string.Join(",",lUProfile.ProfileFiles.ToList());

        if (lUProfile.Platforms != null && lUProfile.Platforms.Count() > 0)
        {
            var platfomrslist = new List<ObservablePlateforme>();
                await foreach(var plats in GetPlatfomsFromProfile(lUProfile))
            {
                platfomrslist.Add(plats);
            }
            profile.Platforms = string.Join(",", platfomrslist.Select(x=>x.Name));
        }
        return profile;
    }

    public async IAsyncEnumerable<ObservablePlateforme> GetPlatfomsFromProfile(LUProfile lUProfile)
    {
        foreach (var plat in lUProfile.Platforms)
        {
            yield return new ObservablePlateforme(await lookconnector.GetPlateformebycodename(plat));
        }

    }
    public async Task ScanWithProfile(ScanProfile profile)
    {
        await emuconnector.ScanWithProfile(profile);
    }
}
