using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface IDevService
{
    IEnumerable<Develloppeur> GetAll();
    IEnumerable<Develloppeur> GetAllForItem(Guid id);
    ItemDev AddDevToItem(string editeurname, Item item);
}