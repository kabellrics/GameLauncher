using GameLauncher.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.Services.Interface;
public interface IEditeurService
{
    IEnumerable<Editeur> GetAll();
    IEnumerable<Editeur> GetAllForItem(Guid id);
    ItemEditeur AddEditeurToItem(string editeurname, Item item);
    ItemEditeur AddEditeurToItem(string editeurname, Item item, DbContext dbcontext);
    void UpdateEditeurInItem(Item Item, List<Editeur> newEditeurs);
    void Fusionnage(Guid idToDelete, Guid idToKeep);
    void Update(Editeur updateditem);
}