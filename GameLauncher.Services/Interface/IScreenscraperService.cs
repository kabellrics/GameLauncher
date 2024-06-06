
using GameLauncher.Models.ScreenScraper;

namespace GameLauncher.Services.Interface;
public interface IScreenscraperService
{
    IEnumerable<Jeux> SearchGameByFileName(string filename);
    IEnumerable<Jeux> SearchGameByName(string name);
}