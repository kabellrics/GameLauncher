using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFinder.Common;
using GameFinder.RegistryUtils;
using GameFinder.StoreHandlers.EGS;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Services.Interface;
using NexusMods.Paths;

namespace GameLauncher.Services.Implementation;
public class EpicGameFinderService : IEpicGameFinderService
{
    private readonly GameLauncherContext dbContext;
    private readonly IAssetDownloader assetDownloader;
    private readonly ISteamGridDbService steangriddbService;
    public EpicGameFinderService(GameLauncherContext dbContext, IAssetDownloader assetDownloader, ISteamGridDbService steangriddbService)
    {
        this.dbContext = dbContext;
        this.assetDownloader = assetDownloader;
        this.steangriddbService = steangriddbService;
    }
    public async Task GetGameAsync()
    {
        var resultlist = new List<Item>();
        var handler = new EGSHandler(WindowsRegistry.Shared, FileSystem.Shared);
        var results = handler.FindAllGames();
        foreach (var result in results) { 
        Item item = new Item();
            item.Name = result.AsT0.DisplayName;
            item.SearchName = item.Name;
            item.StoreId = result.AsT0.CatalogItemId.Value;
            item.Platformes = dbContext.Platformes.First(x => x.Name == "Epic Games Store");
            item.Path = $"com.epicgames.launcher://apps/{item.StoreId}?action=launch&silent=true";
            item.Logo = string.Empty;
            item.Cover = string.Empty;
            item.Banner = string.Empty;
            item.Artwork = string.Empty;
            item.Video = string.Empty;
            item.Description = string.Empty;
            item.ReleaseDate = DateTime.MinValue;
            item.Develloppeurs = new List<Develloppeur>();
            item.Editeurs = new List<Editeur>();
            item.MetadataGenres = new List<MetadataGenre>();
            dbContext.Items.Add(item);
            LookForSteamGridDBAsset(item);
        }
        dbContext.SaveChanges();
    }
    private void LookForSteamGridDBAsset(Item game)
    {
        var listgames = steangriddbService.SearchByName(game.Name);
        var firstgame = listgames.FirstOrDefault();
        if (firstgame != null)
        {
            var assetfolder = assetDownloader.CreateItemAssetFolder(game.ID);
            var listlogo = steangriddbService.GetLogoForId(firstgame.id);
            var listboxart = steangriddbService.GetGridBoxartForId(firstgame.id);
            var listhero = steangriddbService.GetHeroesForId(firstgame.id);
            var firstlogo = listlogo.FirstOrDefault();
            var firstboxart = listboxart.FirstOrDefault();
            var firsthero = listhero.FirstOrDefault();
            if (firstlogo != null)
            {
                assetDownloader.DownloadFile(firstlogo.url, Path.Combine(assetfolder, "logo.png"));
                game.Logo = Path.Combine(assetfolder, "logo.png");
            }
            if (firstboxart != null)
            {
                assetDownloader.DownloadFile(firstboxart.url, Path.Combine(assetfolder, "cover.jpg"));
                game.Cover = Path.Combine(assetfolder, "cover.jpg");
            }
            if (firsthero != null)
            {
                assetDownloader.DownloadFile(firsthero.url, Path.Combine(assetfolder, "banner.jpg"));
                game.Cover = Path.Combine(assetfolder, "banner.jpg");
            }
        }
    }
}
