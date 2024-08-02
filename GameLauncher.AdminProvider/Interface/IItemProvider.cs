using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.ObservableObjet;

namespace GameLauncher.AdminProvider.Interface;
public interface IItemProvider
{
    IAsyncEnumerable<ObservableItem> GetAllItemsStream();
    IAsyncEnumerable<ObservableItem> GetAllItemsAsyncEnumerable();
    Task<IEnumerable<ObservableItem>> GetAllItemsAsync();
    Task<IEnumerable<ObservableGroupItem>> GetAllItemsGrouped();
    Task UpdateItem(ObservableItem item);
    Task UpdatesGenresForItem(Item item, List<Genre> newGenres);
    Task UpdatesDevsForItem(Item item, List<Develloppeur> newDevs);
    Task UpdatesEditsForItem(Item item, List<Editeur> newEdits);
    Task DeleteItem(Guid id);
    Task<StatsObject> GetStatsAsync();
}