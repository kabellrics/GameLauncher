using GameLauncher.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.Services.Interface;
public interface IDevService
{
    IEnumerable<Develloppeur> GetAll();
    IEnumerable<Develloppeur> GetAllForItem(Guid id);
    ItemDev AddDevToItem(string editeurname, Item item);
    ItemDev AddDevToItem(string editeurname, Item item, DbContext dbcontext);
    void UpdateDevInItem(Item Item, List<Develloppeur> newDevs);
    void Fusionnage(Guid idToDelete, Guid idToKeep);
    void Update(Develloppeur updateditem);
}