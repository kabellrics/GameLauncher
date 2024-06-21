using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface IGenreService
{
    IEnumerable<Genre> GetAll();
    IEnumerable<Genre> GetAllForItem(Guid id);
    ItemGenre AddGenreToItem(string genrename, Item item);
    void UpdateGenreInItem(Item Item, List<Genre> newgenres);
    void Fusionnage(Guid idToDelete, Guid idToKeep);
    void Update(Genre updateditem);
}