using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Services.Interface;
using GameLauncher.Models.Steam;
using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Genre = GameLauncher.Models.Genre;
using Microsoft.EntityFrameworkCore;
using GameLauncher.Models.APIObject;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.SignalR;

namespace GameLauncher.Services.Implementation
{
    public class SteamGameFinderService : BaseService, ISteamGameFinderService
    {
        private readonly IAssetDownloader assetDownloader;
        private readonly ISteamGridDbService steangriddbService;
        private readonly IDevService devService;
        private readonly IEditeurService editService;
        private readonly IGenreService genreService;
        public SteamGameFinderService(GameLauncherContext dbContext, IAssetDownloader assetDownloader, ISteamGridDbService steangriddbService
            , IDevService devService, IEditeurService editService,IGenreService genreService) : base(dbContext)
        {
            this.assetDownloader = assetDownloader;
            this.steangriddbService = steangriddbService;
            this.devService = devService;
            this.editService = editService;
            this.genreService = genreService;
        }
        public async Task CleaningGame()
        {
            string steamfolder = GetSteamFolder();
            if (steamfolder != null)
            {
                var foldersToSearch = GetFoldersToSearch(steamfolder);
                var appManifestFiles = GetAppManifestFiles(foldersToSearch);
                var storeIdList = GetStoreIdList(appManifestFiles);

                var gameToRemoves = _dbContext.Items.Where(x => x.LUPlatformesId == "Steam" && !storeIdList.Contains(x.StoreId));
                _dbContext.Items.RemoveRange(gameToRemoves);
                _dbContext.SaveChanges();
                SendNotification(MsgCategory.EndTask, "Suppression de Jeux Steam car désinstallés", $"Suppression de {gameToRemoves.Count()} jeux Steam désintallés");
            }
        }

        public async Task GetGameAsync()
        {
            string steamfolder = GetSteamFolder();
            if (steamfolder != null)
            {
                var foldersToSearch = GetFoldersToSearch(steamfolder);
                var appManifestFiles = GetAppManifestFiles(foldersToSearch);

                foreach (var file in appManifestFiles)
                {
                    try
                    {
                        var appVProperty = VdfConvert.Deserialize(File.ReadAllText(file));
                        var steamId = appVProperty.Value["appid"].ToString();
                        var steamName = appVProperty.Value["name"].ToString();

                        if (!_dbContext.Items.Any(x => x.StoreId == steamId))
                        {
                            Item game = new Item
                            {
                                StoreId = steamId,
                                Name = steamName,
                                SearchName = steamName,
                                Path = $"steam://rungameid/{steamId}",
                                LUPlatformesId = _dbContext.Platformes.First(x => x.Name == "Steam").Codename,
                                AddingDate=DateTime.Now,
                                Logo = string.Empty,
                                Cover = string.Empty,
                                Banner = string.Empty,
                                Artwork = string.Empty,
                                Video = string.Empty,
                                Description = string.Empty,
                                ReleaseDate = DateTime.MinValue,
                                Genres = new List<ItemGenre>(),
                                Develloppeurs = new List<ItemDev>(),
                                Editeurs = new List<ItemEditeur>()
                            };

                            _dbContext.Items.Add(game);
                            var assetFolder = assetDownloader.CreateItemAssetFolder(game.ID);
                            game = await GetSteamInfos(game, assetFolder);
                            _dbContext.SaveChanges();
                            SendNotification(MsgCategory.Create, "Ajout d'un Jeux Steam", $"Ajout de {game.Name} depuis Steam");
                        }
                    }
                    catch (Exception ex)
                    {
                        SendNotification(MsgCategory.Error, ex.Message, ex.StackTrace);
                    }
                }
                SendNotification(MsgCategory.EndTask, $"Fin de l'ajout de jeux depuis Steam", $"Fin de l'ajout de jeux depuis Steam");
            }
        }

        private string GetSteamFolder()
        {
            var key64 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam";
            var key32 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam";
            return Environment.Is64BitOperatingSystem
                ? (string)Microsoft.Win32.Registry.GetValue(key64, "InstallPath", string.Empty)
                : (string)Microsoft.Win32.Registry.GetValue(key32, "InstallPath", string.Empty);
        }

        private List<string> GetFoldersToSearch(string steamfolder)
        {
            var foldersToSearch = new List<string> { Path.Combine(steamfolder, "steamapps") };
            var volvo = VdfConvert.Deserialize(File.ReadAllText(Path.Combine(steamfolder, "steamapps", "libraryfolders.vdf")));
            var children = volvo.Value.Children();

            foreach (var child in children)
            {
                var childKV = (VProperty)child;
                var childValueKV = childKV.Value;
                var pathChildKV = childValueKV.FirstOrDefault();
                if (pathChildKV != null && Directory.Exists(((VProperty)pathChildKV).Value.ToString()))
                {
                    foldersToSearch.Add(Path.Combine(((VProperty)pathChildKV).Value.ToString(), "steamapps"));
                }
            }
            return foldersToSearch;
        }

        private List<string> GetAppManifestFiles(List<string> foldersToSearch)
        {
            var appManifestFiles = new List<string>();
            foreach (var folderToSeek in foldersToSearch)
            {
                appManifestFiles.AddRange(Directory.GetFiles(folderToSeek, "appmanifest_*.acf").ToList());
            }
            return appManifestFiles;
        }

        private List<string> GetStoreIdList(List<string> appManifestFiles)
        {
            var storeIdList = new List<string>();
            foreach (var file in appManifestFiles)
            {
                try
                {
                    var appVProperty = VdfConvert.Deserialize(File.ReadAllText(file));
                    storeIdList.Add(appVProperty.Value["appid"].ToString());
                }
                catch (Exception ex)
                {
                    // Log exception or handle it accordingly
                }
            }
            return storeIdList;
        }



        private async Task<Item> GetSteamInfos(Item game, string assetfolderpath)
        {
            var urlinfo = @"https://store.steampowered.com/api/appdetails?appids=%STEAMID%&l=french";
            var URLInfosPath = urlinfo.Replace("%STEAMID%", game.StoreId);
            try
            {
                string jsoninfos;
                using (WebClient wc = new WebClient())
                {
                    jsoninfos = wc.DownloadString(URLInfosPath);
                }
                JObject json = JObject.Parse(jsoninfos);
                var datajson = json[game.StoreId.ToString()]["data"];
                if (datajson != null)
                {
                    var data = JsonConvert.DeserializeObject<Data>(datajson.ToString());

                    game.Path = $"steam://rungameid/{game.StoreId.ToString()}";
                    game.IsFavorite = false;
                    game = await Task.Run(async () =>
                    {
                        try
                        {
                            game.Description = data.detailed_description;
                            game.ReleaseDate = DateTime.Parse(data.release_date.date);

                            game.Develloppeurs = ExtractDev(data, game).ToList();
                            game.Editeurs = ExtractEditor(data, game).ToList();
                            game.Genres = ExtractGenre(data, game).ToList();

                            var logopath = @"https://cdn.cloudflare.steamstatic.com/steam/apps/%STEAMID%/logo.png";
                            var logotask = assetDownloader.DownloadFile(logopath.Replace("%STEAMID%", game.StoreId), Path.Combine(assetfolderpath, "logo.png"));
                            var coverpath = @"https://cdn.cloudflare.steamstatic.com/steam/apps/%STEAMID%/library_600x900.jpg";
                            var covertask = assetDownloader.DownloadFile(coverpath.Replace("%STEAMID%", game.StoreId), Path.Combine(assetfolderpath, "cover.jpg"));
                            var bannerpath = @"https://steamcdn-a.akamaihd.net/steam/apps/%STEAMID%/library_hero.jpg";
                            var bannertask = assetDownloader.DownloadFile(bannerpath.Replace("%STEAMID%", game.StoreId), Path.Combine(assetfolderpath, "banner.jpg"));
                            var videopath = RemoveTextAfterExtension("mp4", data.movies.FirstOrDefault().mp4.max);
                            var videotask = assetDownloader.DownloadFile(videopath, Path.Combine(assetfolderpath, "video.mp4"));
                            var artworkpath = RemoveTextAfterExtension("jpg", data.screenshots.FirstOrDefault().path_full);
                            var artworktask = assetDownloader.DownloadFile(artworkpath, Path.Combine(assetfolderpath, "artwork.jpg"));

                            await Task.WhenAll(logotask, covertask, bannertask, videotask, artworktask);

                            game.Logo = Path.Combine(assetfolderpath, "logo.png");
                            game.Cover = Path.Combine(assetfolderpath, "cover.jpg");
                            game.Banner = Path.Combine(assetfolderpath, "banner.jpg");
                            game.Artwork = Path.Combine(assetfolderpath, "artwork.jpg");
                            game.Video = Path.Combine(assetfolderpath, "video.mp4");
                        }
                        catch (Exception) { }

                        return game;
                    });
                }
                if (!File.Exists(game.Logo) || !File.Exists(game.Cover) || !File.Exists(game.Banner))
                {
                    try
                    {
                        var listgames = steangriddbService.SearchByName(game.Name);
                        var firstgame = listgames.FirstOrDefault();
                        if (firstgame != null)
                        {
                            var assetfolder = assetDownloader.CreateItemAssetFolder(game.ID);

                            if (string.IsNullOrEmpty(game.Logo))
                            {
                                var listlogo = steangriddbService.GetLogoForId(firstgame.id);
                                var firstlogo = listlogo.FirstOrDefault();
                                if (firstlogo != null)
                                {
                                    assetDownloader.DownloadFile(firstlogo.url, Path.Combine(assetfolder, "logo.png"));
                                    game.Logo = Path.Combine(assetfolder, "logo.png");
                                }
                            }
                            if (string.IsNullOrEmpty(game.Cover))
                            {
                                var listboxart = steangriddbService.GetGridBoxartForId(firstgame.id);
                                var firstboxart = listboxart.FirstOrDefault();
                                if (firstboxart != null)
                                {
                                    assetDownloader.DownloadFile(firstboxart.url, Path.Combine(assetfolder, "cover.jpg"));
                                    game.Cover = Path.Combine(assetfolder, "cover.jpg");
                                }
                            }
                            if (string.IsNullOrEmpty(game.Banner))
                            {
                                var listhero = steangriddbService.GetHeroesForId(firstgame.id);
                                var firsthero = listhero.FirstOrDefault();
                                if (firsthero != null)
                                {
                                    assetDownloader.DownloadFile(firsthero.url, Path.Combine(assetfolder, "banner.jpg"));
                                    game.Banner = Path.Combine(assetfolder, "banner.jpg");
                                }
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception();
            }
            return game;
        }

        private IEnumerable<ItemGenre> ExtractGenre(Data data, Item game)
        {
            var metadatagenres = data.genres;
            foreach (var metagenre in metadatagenres)
            {
                yield return genreService.AddGenreToItem(metagenre.description, game);
            }
        }
        private IEnumerable<ItemEditeur> ExtractEditor(Data data, Item game)
        {
            var devs = data.publishers;
            foreach (var dev in devs)
            {
                yield return editService.AddEditeurToItem(dev, game);
            }
        }
        private IEnumerable<ItemDev> ExtractDev(Data data, Item game) 
        {
            var devs = data.developers;
            foreach (var dev in devs)
            {
                yield return devService.AddDevToItem(dev,game);
            }
        }

        private string RemoveTextAfterExtension(string extension,string input)
        {
            int index = input.IndexOf(extension, StringComparison.OrdinalIgnoreCase);

            if (index >= 0)
            {
                return input.Substring(0, index + 3); // +3 pour inclure "mp4"
            }
            else
            {
                return input; // Retourne la chaîne d'origine si "mp4" n'est pas trouvé
            }
        }
    }
}
