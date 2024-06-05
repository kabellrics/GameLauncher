using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Services.Interface;
using GameLauncher.Services.Utilitaire;
using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameLauncher.Services.Implementation
{
    public class SteamGameFinderService : ISteamGameFinderService
    {
        private readonly GameLauncherContext dbContext;
        private readonly IAssetDownloader assetDownloader;
        public SteamGameFinderService(GameLauncherContext dbContext, IAssetDownloader assetDownloader)
        {
            this.dbContext = dbContext;
            this.assetDownloader = assetDownloader;
        }
        public async Task GetGameAsync()
        {
            string steamfolder;
            var key64 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam";
            var key32 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam";
            if (Environment.Is64BitOperatingSystem)
            {
                steamfolder = (string)Microsoft.Win32.Registry.GetValue(key64, "InstallPath", string.Empty);
            }
            else
            {
                steamfolder = (string)Microsoft.Win32.Registry.GetValue(key32, "InstallPath", string.Empty);
            }

            if (steamfolder != null)
            {
                var foldersTosearch = new List<string>();
                foldersTosearch.Add(Path.Combine(steamfolder, "steamapps"));
                var volvo = VdfConvert.Deserialize(File.ReadAllText(Path.Combine(steamfolder, "steamapps", "libraryfolders.vdf")));
                var childs = volvo.Value.Children();
                foreach (var child in childs)
                {
                    var childKV = (VProperty)child;
                    var childValueKV = childKV.Value;
                    var pathchildKV = childValueKV.FirstOrDefault();
                    if (pathchildKV != null)
                    {
                        //if (Directory.Exists(((VProperty)child).Value.ToString()))
                        if (Directory.Exists(((VProperty)pathchildKV).Value.ToString()))
                        {
                            foldersTosearch.Add(Path.Combine(((VProperty)pathchildKV).Value.ToString(), "steamapps"));
                        }
                    }
                }
                var appmanifestfiles = new List<string>();
                foreach (var foldertoSeek in foldersTosearch)
                {
                    appmanifestfiles.AddRange(Directory.GetFiles(foldertoSeek, "appmanifest_*.acf").ToList());
                }

                foreach (var file in appmanifestfiles)
                {
                    try
                    {
                        //dynamic appfile = VdfConvert.Deserialize(File.ReadAllText(file));
                        var appVproperty = VdfConvert.Deserialize(File.ReadAllText(file));
                        var steamid = appVproperty.Value["appid"].ToString();
                        var steamname = appVproperty.Value["name"].ToString();
                        if (!dbContext.Items.Any(x => x.StoreId == steamid))
                        {
                            Item game = new Item();
                            game.StoreId = steamid;
                            game.Name = steamname;
                            game.SearchName = steamname;
                            game.Path = $"steam://rungameid/{game.StoreId.ToString()}";
                            game.Platformes = dbContext.Platformes.First(x=> x.Name== "Steam");
                            game.Logo = string.Empty;
                            game.Cover = string.Empty;
                            game.Banner = string.Empty;
                            game.Artwork = string.Empty;
                            game.Video = string.Empty;
                            game.Description = string.Empty;
                            game.ReleaseDate = DateTime.MinValue;

                            game.Develloppeurs = new List<Develloppeur>();
                            game.Editeurs = new List<Editeur>();
                            game.MetadataGenres = new List<MetadataGenre>();
                            dbContext.Items.Add(game);
                            var assetfolder = assetDownloader.CreateItemAssetFolder(game.ID);
                            game = await GetSteamInfos(game, assetfolder);
                            dbContext.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw;
                    }
                }
            }
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
                        game.Description = data.detailed_description;
                        game.ReleaseDate = DateTime.Parse(data.release_date.date);

                        game.Develloppeurs = ExtractDev(data,game).ToList();
                        game.Editeurs = ExtractEditor(data, game).ToList();
                        game.MetadataGenres = ExtractGenre(data, game).ToList();

                        var logopath = @"https://cdn.cloudflare.steamstatic.com/steam/apps/%STEAMID%/logo.png";
                        var logotask = assetDownloader.DownloadFile(logopath.Replace("%STEAMID%", game.StoreId), Path.Combine(assetfolderpath,"logo.png"));
                        var coverpath = @"https://cdn.cloudflare.steamstatic.com/steam/apps/%STEAMID%/library_600x900.jpg";
                        var covertask = assetDownloader.DownloadFile(coverpath.Replace("%STEAMID%", game.StoreId), Path.Combine(assetfolderpath, "cover.jpg"));
                        var bannerpath = @"https://steamcdn-a.akamaihd.net/steam/apps/%STEAMID%/library_hero.jpg";
                        var bannertask = assetDownloader.DownloadFile(bannerpath.Replace("%STEAMID%", game.StoreId), Path.Combine(assetfolderpath, "banner.jpg"));
                        var videopath = RemoveTextAfterExtension("mp4",data.movies.FirstOrDefault().mp4.max);
                        var videotask = assetDownloader.DownloadFile(videopath, Path.Combine(assetfolderpath, "video.mp4"));
                        var artworkpath = RemoveTextAfterExtension("jpg", data.screenshots.FirstOrDefault().path_full);
                        var artworktask = assetDownloader.DownloadFile(artworkpath, Path.Combine(assetfolderpath, "artwork.jpg"));

                        await Task.WhenAll(logotask, covertask, bannertask, videotask, artworktask);

                        game.Logo = Path.Combine(assetfolderpath, "logo.png");
                        game.Cover = Path.Combine(assetfolderpath, "cover.jpg");
                        game.Banner = Path.Combine(assetfolderpath, "banner.jpg");
                        game.Artwork = Path.Combine(assetfolderpath, "artwork.jpg");
                        game.Video = Path.Combine(assetfolderpath, "video.mp4");
                        return game;
                    });
                }
            }
            catch (Exception ex)
            {
                //throw new Exception();
            }
            return game;
        }

        private IEnumerable<MetadataGenre> ExtractGenre(Data data, Item game)
        {
            var metadatagenres = data.genres;
            foreach (var metagenre in metadatagenres)
            {
                if (!dbContext.MetadataGenres.Any(x => x.Name == metagenre.description))
                {
                    var dbmetagenre = new MetadataGenre { ID = Guid.NewGuid(), Name = metagenre.description, Items = new List<Item>() { game } };
                    dbContext.MetadataGenres.Add(dbmetagenre);
                    yield return dbmetagenre;
                }
                else
                {
                    var dbmetagenre = dbContext.MetadataGenres.First(x => x.Name == metagenre.description);
                    dbmetagenre.Items.Add(game);
                    dbContext.MetadataGenres.Update(dbmetagenre);
                    yield return dbmetagenre;
                }
            }
        }
        private IEnumerable<Editeur> ExtractEditor(Data data, Item game)
        {
            var devs = data.publishers;
            foreach (var dev in devs)
            {
                if (!dbContext.Editeurs.Any(x => x.Name == dev))
                {
                    var dbdev = new Editeur { ID = Guid.NewGuid(), Name = dev, Items = new List<Item>() { game } };
                    dbContext.Editeurs.Add(dbdev);
                    yield return dbdev;
                }
                else
                {
                    var dbdev = dbContext.Editeurs.First(x => x.Name == dev);
                    dbdev.Items.Add(game);
                    dbContext.Editeurs.Update(dbdev);
                    yield return dbdev;
                }
            }
        }
        private IEnumerable<Develloppeur> ExtractDev(Data data, Item game) 
        {
            var devs = data.developers;
            foreach (var dev in devs)
            {
                if (!dbContext.Develloppeurs.Any(x => x.Name == dev))
                {
                    var dbdev = new Develloppeur { ID = Guid.NewGuid(), Name = dev, Items = new List<Item>() { game } };
                    dbContext.Develloppeurs.Add(dbdev);
                    yield return dbdev;
                }
                else
                {
                    var dbdev = dbContext.Develloppeurs.First(x => x.Name == dev);
                    dbdev.Items.Add(game);
                    dbContext.Develloppeurs.Update(dbdev);
                    yield return dbdev;
                }
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
