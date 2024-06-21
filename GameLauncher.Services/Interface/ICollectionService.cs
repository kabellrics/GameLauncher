using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface ICollectionService
{
    void AddToCollectionEnd(Guid id, Guid gameid);
    IEnumerable<Collection> GetAll();
    IEnumerable<Item> GetAllItemInside(Guid id);
    void Update(Collection updatedcollection);
    void UpdateCollectionItemOrder(Guid id, Guid gameid, int newOrder);
}