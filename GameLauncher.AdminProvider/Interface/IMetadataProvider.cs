using GameLauncher.Models;
using GameLauncher.Models.IGDB;
using GameLauncher.Models.ScreenScraper;
using GameLauncher.Models.SteamGridDB;
using GameLauncher.ObservableObjet;

namespace GameLauncher.AdminProvider.Interface;
public interface IMetadataProvider
{
    Task<ObservableItem> GetIGDBDetailsGame(int id);
    Task<IEnumerable<ObservableItem>> GetIGDBGameByName(string name);
    IAsyncEnumerable<ObservableItem> GetIGDBGameByNameAsync(string name);
    Task<IEnumerable<Video>> GetIGDBVideosByGameId(int id);
    Task<IEnumerable<ObservableItem>> GetSearchScreenscraperGameByFileName(string filename);
    Task<IEnumerable<ObservableItem>> SearchScreenscraperGameByName(string name);
    IAsyncEnumerable<ObservableItem> SearchScreenscraperGameByNameAsync(string name);
    Task<IEnumerable<ImgResult>> SearchSteamGridDBBannerFor(int gameId);
    Task<IEnumerable<ImgResult>> SearchSteamGridDBBoxartFor(int gameId);
    Task<IEnumerable<DataSearch>> SearchSteamGridDBGameByName(string name);
    Task<IEnumerable<ImgResult>> SearchSteamGridDBLogoFor(int gameId);
    Task<ObservableMediaItem> GetSteamGridDBMediaItem(DataSearch game);
    Task<IEnumerable<ObservableMediaItem>> GetIGDBMediaGameByName(string name);
    Task<IEnumerable<ObservableMediaItem>> SearchScreenscraperGameMediaByName(string name);