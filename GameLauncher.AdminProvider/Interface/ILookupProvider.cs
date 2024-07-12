using GameLauncher.Models;
using GameLauncher.ObservableObjet;

namespace GameLauncher.AdminProvider.Interface;
public interface ILookupProvider
{
    Task<bool> FusionDev(Guid idToDelete, Guid idToKeep);
    Task<bool> FusionEditeur(Guid idToDelete, Guid idToKeep);
    Task<bool> FusionGenre(Guid idToDelete, Guid idToKeep);
    Task<IEnumerable<Develloppeur>> GetDevsAsync();
    Task<IEnumerable<Editeur>> GetEditeursAsync();
    Task<IEnumerable<Genre>> GetGenresAsync();
    Task<IEnumerable<LUPlatformes>> GetPlatformesAsync();
    Task UpdateDev(ObservableDevelloppeur item);
    Task UpdateEditeur(ObservableEditeur item);
    Task UpdateGenre(ObservableGenre item);
    Task<LUPlatformes> GetPlateformebycodename(string codename);
}