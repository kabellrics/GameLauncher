using GameLauncher.Models;

namespace GameLauncher.Services.Interface.Front;
public interface IItemsService
{
    IEnumerable<Item> GetAll();
    IAsyncEnumerable<Item> GetAllAsync();
    void ToggleItemFavorite(Guid updateditemID);
}