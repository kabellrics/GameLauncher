using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface IItemsService
{
    IEnumerable<Item> GetAll();
    void UpdateItem(Item item);
    Item Insert(Item item);
    IAsyncEnumerable<Item> GetAllAsync();
    void DeleteItem(Guid id);
}