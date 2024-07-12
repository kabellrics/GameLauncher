using GameLauncher.Models;
using GameLauncher.ObservableObjet;

namespace GameLauncher.AdminProvider.Interface;
public interface ICollectionProvider
{
    Task AddToCollectionEnd(Guid id, Guid gameid);
    Task<IEnumerable<Item>> GetAllItemInside(Guid id);

    IAsyncEnumerable<ObservableItemInCollection> GetAllItemInsideAsyncStream(Guid id);
    Task<IEnumerable<ObsCollection>> GetCollectionsAsync();
    IAsyncEnumerable<ObsCollection> GetCollectionsAsyncEnumerable();
    Task UpdateCollection(ObsCollection item);
    Task UpdateCollectionItemOrder(Guid id, Guid gameid, int newOrder);
    Task CreateCollectionFromPlateforme();
    IAsyncEnumerable<ObservableItem> GetAllItemInsideAsync(Guid id);
    Task DeleteCollectionItem(Guid id);
    Task CreateCollection(Collection item);
    Task DeleteCollection(Guid id);
}