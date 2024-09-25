using GameLauncher.Models;
using GameLauncher.Models.APIObject;

namespace GameLauncher.Services.Interface.Front;
public interface ICollectionService
{
    IEnumerable<Collection> GetAll();
    Task<IEnumerable<FullCollectionTrueItem>> GetAllFull();
    IAsyncEnumerable<ItemInCollection> GetAllItemInside(Guid id);
    IAsyncEnumerable<TrueItemInCollection> GetAllTrueItemInside(Guid id);
}