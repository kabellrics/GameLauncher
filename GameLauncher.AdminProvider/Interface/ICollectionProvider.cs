using GameLauncher.Models;
using GameLauncher.ObservableObjet;

namespace GameLauncher.AdminProvider.Interface;
public interface ICollectionProvider
{
    Task AddToCollectionEnd(Guid id, Guid gameid);
    Task<IEnumerable<Item>> GetAllItemInside(Guid id);
    Task<IEnumerable<ObsCollection>> GetCollectionsAsync();
    Task UpdateCollection(ObsCollection item);
    Task UpdateCollectionItemOrder(Guid id, Guid gameid, int newOrder);
}