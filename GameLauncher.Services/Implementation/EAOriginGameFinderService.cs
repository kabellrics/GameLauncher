using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFinder.StoreHandlers.EADesktop.Crypto.Windows;
using GameFinder.StoreHandlers.EADesktop;
using System.Xml.Serialization;
using NexusMods.Paths;
using GameFinder.Common;
using Microsoft.EntityFrameworkCore;
using GameLauncher.DAL;
using GameLauncher.Services.Interface;
using GameLauncher.Models;
using GameLauncher.Models.EAOrigin;

namespace GameLauncher.Services.Implementation;
public class EAOriginGameFinderService : IEAOriginGameFinderService
{
    private readonly GameLauncherContext dbContext;
    private readonly IAssetDownloader assetDownloader;
    private readonly ISteamGridDbService steangriddbService;
    public EAOriginGameFinderService(GameLauncherContext dbContext, IAssetDownloader assetDownloader, ISteamGridDbService steangriddbService)
    {
        this.dbContext = dbContext;
        this.assetDownloader = assetDownloader;
        this.steangriddbService = steangriddbService;
    }
    public async Task GetGameAsync()
    {
        var resultlist = new List<Item>();
        try
        {
            var handler = new EADesktopHandler(FileSystem.Shared, new HardwareInfoProvider());
            var results = handler.FindAllGames();
            var gamesfind = new List<EADesktopGame>();
            foreach (var result in results)
            {
                // using the switch method
                result.Switch(game =>
                {
                    Console.WriteLine($"Found {game}");
                }, error =>
                {
                    Console.WriteLine(error);
                });

                // using the provided extension functions
                if (result.TryGetGame(out var game))
                {
                    Console.WriteLine($"Found {game}");
                    gamesfind.Add(game);
                }
                else
                {
                    Console.WriteLine(result.AsError());
                }
            }
            if (gamesfind.Count > 0)
            {
                foreach (var item in gamesfind)
                {
                    if (!dbContext.Items.Any(x => x.StoreId == item.EADesktopGameId.Value))
                    {
                        var ManifestgamePath = Path.Combine(item.BaseInstallPath.GetFullPath(), "__Installer", "installerdata.xml");
                        XmlSerializer serializer = new XmlSerializer(typeof(DiPManifest));
                        try
                        {
                            using (FileStream fs = new FileStream(ManifestgamePath, FileMode.Open))
                            {
                                var ManifestContent = (DiPManifest)serializer.Deserialize(fs);
                                var exe = new Item();
                                exe.Name = ManifestContent.gameTitles.FirstOrDefault(x => x.locale == "fr_FR").Value;
                                exe.SearchName = exe.Name;
                                //exe.Name = dipManifest.gameTitles.gameTitle.FirstOrDefault(x => x.Locale == "fr_FR")?.Value;
                                var notrialexe = ManifestContent.runtime.FirstOrDefault(x => x.trial == 0);
                                //var notrialexe = dipManifest.runtime.launcher.FirstOrDefault(x => x.trial == 0);
                                exe.Path = $"{item.BaseInstallPath.GetFullPath()}/{getExeName(notrialexe.filePath)}";
                                exe.StoreId = item.EADesktopGameId.Value;
                                exe.Platformes = dbContext.Platformes.First(x => x.Name == "EA Origin");
                                exe.LUPlatformesId = exe.Platformes.ID;
                                resultlist.Add(exe);
                            }
                        }
                        catch (Exception ex)
                        {
                            //throw;
                        }
                    }
                }
            }
            resultlist = resultlist.GroupBy(x => x.Path).Select(x => x.First()).ToList();
            foreach (var result in resultlist)
            {
                result.Logo = string.Empty;
                result.Cover = string.Empty;
                result.Banner = string.Empty;
                result.Artwork = string.Empty;
                result.Video = string.Empty;
                result.Description = string.Empty;
                result.ReleaseDate = DateTime.MinValue;
                result.Develloppeurs = new List<ItemDev>();
                result.Editeurs = new List<ItemEditeur>();
                result.Genres = new List<ItemGenre>();
                dbContext.Items.Add(result);
                LookForSteamGridDBAsset(result);
            }
            dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            //throw;
        }
    }

    private void LookForSteamGridDBAsset(Item game)
    {
        var listgames = steangriddbService.SearchByName(game.Name);
        var firstgame = listgames.FirstOrDefault();
        if (firstgame != null) {
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
                game.Banner = Path.Combine(assetfolder, "banner.jpg");
            }
        }
    }

    private string getExeName(string path)
    {
        int indexOfBracket = path.IndexOf("]");

        if (indexOfBracket >= 0)
        {
            // Extraire le texte après "]"
            string textAfterBracket = path.Substring(indexOfBracket + 1);

            // Maintenant, textAfterBracket contient "bfv.exe"
            return textAfterBracket;
        }
        else
        {
            // La chaîne d'entrée ne contient pas de "]"
            return path;
        }

    }
}
