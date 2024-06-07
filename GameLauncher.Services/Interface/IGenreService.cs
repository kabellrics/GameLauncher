using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface IGenreService
{
    IEnumerable<Genre> GetAll();
    IEnumerable<Genre> GetAllForItem(Guid id);
}