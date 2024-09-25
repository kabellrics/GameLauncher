using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Connector;
using GameLauncher.Models;
using GameLauncher.ObservableObjet;
using GameLauncher.Services.Interface;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.AdminProvider;
public class LookupProvider : ILookupProvider
{
    //private readonly LookupConnector lookupconnector;
    private readonly IGenreService genreService;
    private readonly IDevService devService;
    private readonly IEditeurService editService;
    private readonly IPlateformeService plateformeService;
    public LookupProvider(IGenreService genre, IDevService dev, IEditeurService edit, IPlateformeService plateforme)
    {
        genreService = genre;
        devService = dev;
        editService = edit;
        plateformeService = plateforme;
        //lookupconnector = new LookupConnector("https://localhost:7197");
    }
    public async Task<IEnumerable<Develloppeur>> GetDevsAsync()
    {
        return devService.GetAll();
    }
    public async Task<IEnumerable<Editeur>> GetEditeursAsync()
    {
        return editService.GetAll();
    }
    public async Task<IEnumerable<Genre>> GetGenresAsync()
    {
        return genreService.GetAll();
    }
    public async Task<IEnumerable<LUPlatformes>> GetPlatformesAsync()
    {
        return plateformeService.GetAll();
    }
    public async Task<bool> FusionDev(Guid idToDelete, Guid idToKeep)
    {
        devService.Fusionnage(idToDelete, idToKeep);
        return true;
    }
    public async Task<bool> FusionEditeur(Guid idToDelete, Guid idToKeep)
    {
        editService.Fusionnage(idToDelete, idToKeep);
        return true;
    }
    public async Task<bool> FusionGenre(Guid idToDelete, Guid idToKeep)
    {
        genreService.Fusionnage(idToDelete, idToKeep);
        return true;
    }
    public async Task UpdateDev(ObservableDevelloppeur item)
    {
        devService.Update(item.Item);
    }
    public async Task UpdateEditeur(ObservableEditeur item)
    {
        editService.Update(item.Item);
    }
    public async Task UpdateGenre(ObservableGenre item)
    {
        genreService.Update(item.Item);
    }
    public async Task<LUPlatformes> GetPlateformebycodename(string codename)
    {
        return plateformeService.Get(codename);
    }
}
