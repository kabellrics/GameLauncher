using GameLauncher.Models;
using GameLauncher.Models.APIObject;

namespace GameLauncher.Services.Interface;
public interface ICollectionService
{
    void AddToCollectionEnd(Guid id, Guid gameid);
    IEnumerable<Collection> GetAll();
    IAsyncEnumerable<ItemInCollection> GetAllItemInside(Guid id);
    void Update(Collection updatedcollection);
    void UpsertCollectionItem(Guid id, Guid gameid, int newOrder);
    void CreateCollectionFromPlateforme();
    bool DelteCollectionItem(Guid id);
    bool DelteCollection(Guid id);
    void CreateCollection(Collection collec);
    DefaultCollectionMessage GetDefaultCollectionStatus();
    DefaultCollectionMessage CreateDefaultColection(DefaultCollectionMessage collectionMessage);
    List<String> GetPredefineCollection();
}