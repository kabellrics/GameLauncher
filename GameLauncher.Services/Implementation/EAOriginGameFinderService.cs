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
using Microsoft.AspNetCore.SignalR;
using GameLauncher.Models.APIObject;

namespace GameLauncher.Services.Implementation;
public class EAOriginGameFinderService : BaseService, IEAOriginGameFinderService
{
    private readonly GameLauncherContext _dbContext;
    private readonly IAssetDownloader assetDownloader;
    private readonly ISteamGridDbService steangriddbService;
    public EAOriginGameFinderService(GameLauncherContext dbContext, IHubContext<SignalRNotificationHub, INotificationService> notifService, IAssetDownloader assetDownloader, ISteamGridDbService steangriddbService) : base(dbContext, notifService)
    {
        this.assetDownloader = assetDownloader;
        this.steangriddbService = steangriddbService;
    }
    public async Task CleaningGame()
    {
        var handler = new EADesktopHandler(FileSystem.Shared, new HardwareInfoProvider());
        var results = handler.FindAllGames();
        var gamesfind = new List<EADesktopGame>();
        var storeIdList = results.Select(x => x.AsT0.EADesktopGameId.Value);
        var gameToRemoves = _dbContext.Items.Where(x => x.LUPlatformesId == "EA Play" && !storeIdList.Contains(x.StoreId));
        _dbContext.Items.RemoveRange(gameToRemoves);
        _dbContext.SaveChanges();
        SendNotification(MsgCategory.EndTask, "Fin du nettoyage de jeu EA Play", $"Suppression de {gameToRemoves.Count()} jeux EA Play car désintallés");
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
                    if (!_dbContext.Items.Any(x => x.StoreId == item.EADesktopGameId.Value))
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
                                exe.LUPlatformesId = _dbContext.Platformes.First(x => x.Name == "EA Play").Codename;
                                exe.AddingDate = DateTime.Now;
                                resultlist.Add(exe);
                            }
                        }
                        catch (Exception ex)
                        {
                            SendNotification(MsgCategory.Error, ex.Message, ex.StackTrace);
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
                _dbContext.Items.Add(result);
                SendNotification(MsgCategory.Create, "Ajout d'un jeu EA Play", $"Ajout de {result.Name} depuis EA Play");
                LookForSteamGridDBAsset(result);
                SendNotification(MsgCategory.Update, "Metadata pour un Jeu EA Play", $"Ajout de Metadata pour {result.Name}");
            }
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            SendNotification(MsgCategory.Error,ex.Message,ex.StackTrace);
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
