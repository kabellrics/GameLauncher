using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface IItemsService
{
    IEnumerable<Item> GetAll();
}