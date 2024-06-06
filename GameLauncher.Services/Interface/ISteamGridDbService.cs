using GameLauncher.Models.SteamGridDB;

namespace GameLauncher.Services.Interface;
public interface ISteamGridDbService
{
    DataSearch GetGameSteamId(string steamId);
    IEnumerable<ImgResult> GetGridBannerForId(int gameId);
    IEnumerable<ImgResult> GetGridBoxartForId(int gameId);
    IEnumerable<ImgResult> GetHeroesForId(int gameId);
    IEnumerable<ImgResult> GetLogoForId(int gameId);
    IEnumerable<DataSearch> SearchByName(string name);
}