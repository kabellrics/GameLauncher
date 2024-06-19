using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface IEditeurService
{
    IEnumerable<Editeur> GetAll();
    IEnumerable<Editeur> GetAllForItem(Guid id);
    ItemEditeur AddEditeurToItem(string editeurname, Item item);
    void UpdateEditeurInItem(Item Item, List<Editeur> newEditeurs);
}