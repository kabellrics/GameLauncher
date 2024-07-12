using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface IPlateformeService
{
    IEnumerable<LUPlatformes> GetAll();
    LUPlatformes Get(string id);
}