using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Connector;
using GameLauncher.Models;
using GameLauncher.ObservableObjet;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.AdminProvider;
public class LookupProvider : ILookupProvider
{
    private readonly LookupConnector lookupconnector;
    public LookupProvider()
    {
        lookupconnector = new LookupConnector("https://localhost:7197");
    }
    public async Task<IEnumerable<Develloppeur>> GetDevsAsync()
    {
        return await lookupconnector.GetDevsAsync();
    }
    public async Task<IEnumerable<Editeur>> GetEditeursAsync()
    {
        return await lookupconnector.GetEditeursAsync();
    }
    public async Task<IEnumerable<Genre>> GetGenresAsync()
    {
        return await lookupconnector.GetGenresAsync();
    }
    public async Task<IEnumerable<LUPlatformes>> GetPlatformesAsync()
    {
        return await lookupconnector.GetPlatformesAsync();
    }
    public async Task<bool> FusionDev(Guid idToDelete, Guid idToKeep)
    {
        return await lookupconnector.FusionDev(idToDelete, idToKeep);
    }
    public async Task<bool> FusionEditeur(Guid idToDelete, Guid idToKeep)
    {
        return await lookupconnector.FusionEditeur(idToDelete, idToKeep);
    }
    public async Task<bool> FusionGenre(Guid idToDelete, Guid idToKeep)
    {
        return await lookupconnector.FusionGenre(idToDelete, idToKeep);
    }
    public async Task UpdateDev(ObservableDevelloppeur item)
    {
        await lookupconnector.UpdateDevAsync(item.Item);
    }
    public async Task UpdateEditeur(ObservableEditeur item)
    {
        await lookupconnector.UpdateEditeurAsync(item.Item);
    }
    public async Task UpdateGenre(ObservableGenre item)
    {
        await lookupconnector.UpdateGenreAsync(item.Item);
    }
    public async Task<LUPlatformes> GetPlateformebycodename(string codename)
    {
        return await lookupconnector.GetPlateformebycodename(codename);
    }
}
