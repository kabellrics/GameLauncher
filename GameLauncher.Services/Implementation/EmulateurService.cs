using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameFinder.Common;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Models.ScreenScraper;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NexusMods.Paths;

namespace GameLauncher.Services.Implementation;
public class EmulateurService : BaseService, IEmulateurService
{
    private readonly IItemsService _itemsService;
    private readonly ISteamGridDbService _steangriddbService;
    private readonly IIGDBService _igdbService;
    private readonly IScreenscraperService _screenscraperService;
    private readonly IGenreService _genreService;
    private readonly IDevService _devService;
    private readonly IEditeurService _editeurService;
    private readonly IAssetDownloader _assetDownloader;
    public EmulateurService(GameLauncherContext dbContext, IHubContext<SignalRNotificationHub, INotificationService> notifService, ISteamGridDbService steangriddbService
        , IIGDBService igdbService, IScreenscraperService screenscraperService, IItemsService itemsService, IAssetDownloader assetDownloader
        , IGenreService genreService, IDevService devService, IEditeurService editeurService) : base(dbContext, notifService)
    {
        this._steangriddbService = steangriddbService;
        this._igdbService = igdbService;
        this._screenscraperService = screenscraperService;
        this._genreService = genreService;
        this._editeurService = editeurService;
        this._devService = devService;
        this._itemsService = itemsService;
        this._assetDownloader = assetDownloader;
    }
    public async Task ScanFolderForRom(ScanProfile scanprofile)
    {
        SendNotification(MsgCategory.StartTask, "Debut du scan", $"Debut du scan du dossier {scanprofile.FolderPath} à la recherche de rom pour {scanprofile.Platforms} sur l'émulateur {scanprofile.Profile.Name}");
        var filewithcorrectextension = new List<string>();
        foreach (var ext in scanprofile.Profile.ImageExtensions)
        {
            var searchPattern = $"*.{ext}";
            filewithcorrectextension.AddRange(Directory.GetFiles(scanprofile.FolderPath, searchPattern, SearchOption.TopDirectoryOnly));
        }
        if (filewithcorrectextension.Count == 0) { return; }
        SendNotification(MsgCategory.Info, $"{filewithcorrectextension.Count} fichiers trouvés", $"{filewithcorrectextension.Count} fichiers trouvés avec les extensions {string.Join(",", scanprofile.Profile.ImageExtensions)}");
        foreach (var file in filewithcorrectextension)
        {
            SendNotification(MsgCategory.Info, $"Debut de traitement du fichier {file}", $"Debut du traitement du fichier {file} pour le profile {scanprofile.Profile.Name}");
            Item item = new Item();
            item.Name = Path.GetFileNameWithoutExtension(file);
            item.SearchName = item.Name;
            item.LUPlatformesId = scanprofile.Platforms.Codename;
            item.LUProfileId = scanprofile.Profile.Id;
            item.Path = file;
            item.AddingDate = DateTime.Now;
            item.StoreId = string.Empty;
            item.Logo = string.Empty;
            item.Cover = string.Empty;
            item.Banner = string.Empty;
            item.Artwork = string.Empty;
            item.Video = string.Empty;
            SendNotification(MsgCategory.Info, $"Recherche de Metadata pour {item.Name}", $"Recherche de Metadata pour {item.Name} avec le provider {scanprofile.MetaProvider}");
            Jeux sscpgame = null;
            if (scanprofile.MetaProvider == MetadataProvider.Screenscraper || scanprofile.LogoProvider == LogoProvider.Screenscraper ||
                scanprofile.FanartProvider == FanartProvider.Screenscraper || scanprofile.CoverProvider == CoverProvider.Screenscraper ||
                scanprofile.VideoProvider == VideoProvider.Screenscraper)
                sscpgame = GetSSCPGame(item);
            if (scanprofile.MetaProvider == MetadataProvider.Screenscraper)
            {
                GetSSCPMetadata(item, sscpgame);
            }
            else if (scanprofile.MetaProvider == MetadataProvider.IGDB)
            {
                GetIGDBMetadata(item);
            }
            SendNotification(MsgCategory.Info, $"Recherche de Logo pour {item.Name}", $"Recherche de Logo pour {item.Name} avec le provider {scanprofile.LogoProvider}");
            if (scanprofile.LogoProvider == LogoProvider.SteamGridDB) { GetSGDBLogo(item); }
            else if (scanprofile.LogoProvider == LogoProvider.Screenscraper)
            {
                GetSSCPLogo(item, sscpgame);
            }

            SendNotification(MsgCategory.Info, $"Recherche de Cover pour {item.Name}", $"Recherche de Cover pour {item.Name} avec le provider {scanprofile.CoverProvider}");
            if (scanprofile.CoverProvider == CoverProvider.SteamGridDB) { GetSGDBCover(item); }
            else if (scanprofile.CoverProvider == CoverProvider.Screenscraper)
            {
                GetSSCPCover(item, sscpgame);
            }
            else if (scanprofile.CoverProvider == CoverProvider.IGDB) { GetIGDBCover(item); }

            SendNotification(MsgCategory.Info, $"Recherche de Fanart pour {item.Name}", $"Recherche de Fanart pour {item.Name} avec le provider {scanprofile.FanartProvider}");
            if (scanprofile.FanartProvider == FanartProvider.Screenscraper)
            {
                GetSSCPFanart(item, sscpgame);
            }
            else if (scanprofile.FanartProvider == FanartProvider.IGDB) { GetIGDBFanart(item); }

            SendNotification(MsgCategory.Info, $"Recherche de Video pour {item.Name}", $"Recherche de Video pour {item.Name} avec le provider {scanprofile.VideoProvider}");
            if (scanprofile.VideoProvider == VideoProvider.Screenscraper)
            {
                GetSSCPVideo(item, sscpgame);
            }
            else if (scanprofile.VideoProvider == VideoProvider.IGDB) { }

            SendNotification(MsgCategory.Info, $"Recherche de Heroe pour {item.Name}", $"Recherche de Heroe pour {item.Name} avec le provider SteamGridDB");
            GetSGDBHeroes(item);
            _itemsService.UpdateItem(item);
            SendNotification(MsgCategory.Create, $"Enregistrement de {item.Name}", $"Enregistrement de {item.Name} pour la plateforme {scanprofile.Profile.Name}");
        }
        SendNotification(MsgCategory.EndTask, "Fin du scan", $"Fin du scan du dossier {scanprofile.FolderPath}.{filewithcorrectextension.Count} jeux trouvé pour {scanprofile.Platforms} sur l'émulateur {scanprofile.Profile.Name}");
    }
    private Jeux GetSSCPGame(Item item)
    {
        var sscpgames = _screenscraperService.SearchGameByFileName(item.Path);
        return sscpgames.FirstOrDefault();
    }
    private void GetSSCPLogo(Item item, Jeux game)
    {
        item.Logo = game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "fr")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "eu")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "ss")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "wor")?.url ??
                        string.Empty;
    }
    private void GetSSCPCover(Item item, Jeux game)
    {
        item.Cover = game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "fr")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "eu")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "ss")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "wor")?.url ??
                        string.Empty;
    }
    private void GetSSCPFanart(Item item, Jeux game)
    {
        var artwork = game.medias.FirstOrDefault(x => x.type == "fanart")?.url ?? string.Empty;
        var screen = game.medias.FirstOrDefault(x => x.type == "ss")?.url ?? string.Empty;
        var screentitle = game.medias.FirstOrDefault(x => x.type == "sstitle")?.url ?? string.Empty;
        var screen169 = game.medias.FirstOrDefault(x => x.type == "ssfronton16-9")?.url ?? string.Empty;
        if (!string.IsNullOrEmpty(artwork)) { item.Artwork = artwork; }
        else if (!string.IsNullOrEmpty(screen)) { item.Artwork = screen; }
        else if (!string.IsNullOrEmpty(screentitle)) { item.Artwork = screentitle; }
        else if (!string.IsNullOrEmpty(screen169)) { item.Artwork = screen169; }
    }
    private void GetSSCPVideo(Item item, Jeux game)
    {
        item.Video = game.medias.FirstOrDefault(x => x.type == "video")?.url ?? string.Empty;
    }
    private void GetSGDBCover(Item item)
    {
        var sgdbsearch = _steangriddbService.SearchByName(item.SearchName);
        if (sgdbsearch != null)
        {
            var sgdbfirstgame = sgdbsearch.First();
            if (sgdbfirstgame != null)
            {
                var heroes = _steangriddbService.GetGridBoxartForId(sgdbfirstgame.id);
                item.Cover = heroes.First().url;
            }
        }
    }
    private void GetSGDBLogo(Item item)
    {
        var sgdbsearch = _steangriddbService.SearchByName(item.SearchName);
        if (sgdbsearch != null)
        {
            var sgdbfirstgame = sgdbsearch.First();
            if (sgdbfirstgame != null)
            {
                var heroes = _steangriddbService.GetLogoForId(sgdbfirstgame.id);
                item.Logo = heroes.First().url;
            }
        }
    }
    private void GetSGDBHeroes(Item item)
    {
        var sgdbsearch = _steangriddbService.SearchByName(item.SearchName);
        if (sgdbsearch != null)
        {
            var sgdbfirstgame = sgdbsearch.First();
            if (sgdbfirstgame != null)
            {
                var heroes = _steangriddbService.GetHeroesForId(sgdbfirstgame.id);
                item.Banner = heroes.First().url;
            }
        }
    }
    private void GetIGDBCover(Item item)
    {
        var igdbgames = _igdbService.GetGameByName(item.SearchName);
        if (igdbgames != null)
        {
            var igdbgame = igdbgames.FirstOrDefault();
            if (igdbgame != null)
            {
                var detailgame = _igdbService.GetDetailsGame(igdbgame.id);
                if (detailgame != null)
                {
                    var cover = detailgame.cover?.url ?? string.Empty;
                    if (!string.IsNullOrEmpty(cover))
                    {
                        var filename = Path.GetFileName(cover);
                        item.Cover = $"https://images.igdb.com/igdb/image/upload/t_cover_big/{filename}";
                    }
                }
            }
        }
    }
    private void GetIGDBFanart(Item item)
    {
        var igdbgames = _igdbService.GetGameByName(item.SearchName);
        if (igdbgames != null)
        {
            var igdbgame = igdbgames.FirstOrDefault();
            if (igdbgame != null)
            {
                var detailgame = _igdbService.GetDetailsGame(igdbgame.id);
                if (detailgame != null)
                {
                    var artwork = detailgame.artworks.FirstOrDefault()?.url ?? string.Empty;
                    if (!string.IsNullOrEmpty(artwork))
                    {
                        var filename = Path.GetFileName(artwork);
                        item.Artwork = $"https://images.igdb.com/igdb/image/upload/t_1080p/{filename}";
                    }
                }
            }
        }
    }
    private void GetIGDBMetadata(Item item)
    {
        var igdbgames = _igdbService.GetGameByName(item.Name);
        if (igdbgames != null && igdbgames.Any())
        {
            var game = _igdbService.GetDetailsGame(igdbgames.First().id);
            if (game != null)
            {
                item.Name = game.name;
                item.SearchName = game.name;
                item.Description = game.summary;
                item.ReleaseDate = ConvertFromIGDB(game.first_release_date);
                _dbContext.Items.Add(item);
                var genres = game.genres.Select(g => g.name);
                foreach (var genre in genres)
                    _genreService.AddGenreToItem(genre, item, _dbContext);
                var devs = _igdbService.GetCompaniesDetail(game.involved_companies.Where(x => x.developer).Select(x => x.company.ToString()));
                foreach (var dev in devs)
                    _devService.AddDevToItem(dev.name, item, _dbContext);
                var editeurs = _igdbService.GetCompaniesDetail(game.involved_companies.Where(x => x.publisher).Select(x => x.company.ToString()));
                foreach (var editeur in editeurs)
                    _editeurService.AddEditeurToItem(editeur.name, item, _dbContext);
                _dbContext.Items.Update(item);
                _dbContext.SaveChanges();
            }
        }
    }
    private DateTime ConvertFromIGDB(int value)
    {
        var result = DateTime.MinValue;
        try
        {
            long parsedUnixTimestamp;
            if (long.TryParse(value.ToString(), out parsedUnixTimestamp)) ;
            return DateTimeOffset.FromUnixTimeSeconds(parsedUnixTimestamp).DateTime;
        }
        catch (Exception ex) { }
        return result;
    }
    private void GetSSCPMetadata(Item item, Jeux game)
    {
        if (game != null)
        {
            item.Name = game.noms.FirstOrDefault(x => x.region == "fr")?.text ??
                        game.noms.FirstOrDefault(x => x.region == "eu")?.text ??
                        game.noms.FirstOrDefault(x => x.region == "ss")?.text ??
                        game.noms.FirstOrDefault(x => x.region == "wor")?.text ?? string.Empty;
            item.SearchName = game.noms.FirstOrDefault(x => x.region == "wor")?.text ??
                        game.noms.FirstOrDefault(x => x.region == "us")?.text ??
                        game.noms.FirstOrDefault(x => x.region == "ss")?.text ??
                        game.noms.FirstOrDefault(x => x.region == "eu")?.text ??
                        game.noms.FirstOrDefault(x => x.region == "fr")?.text ?? string.Empty;
            item.Description = game.synopsis.FirstOrDefault(x => x.langue == "fr")?.text ?? game.synopsis.FirstOrDefault()?.text ?? string.Empty;
            var stringdate = game.dates.FirstOrDefault(x => x.region == "fr")?.text ??
                             game.dates.FirstOrDefault(x => x.region == "eu")?.text ??
                             game.dates.FirstOrDefault(x => x.region == "ss")?.text ??
                             game.dates.FirstOrDefault(x => x.region == "wor")?.text ?? string.Empty;
            if (!string.IsNullOrEmpty(stringdate))
                item.ReleaseDate = ParseDate(stringdate);
            item.Genres = new List<ItemGenre>();
            item.Develloppeurs = new List<ItemDev>();
            item.Editeurs = new List<ItemEditeur>();
            _dbContext.Items.Add(item);
            var genres = game.genres.SelectMany(x => x.noms).Where(x => x.langue == "fr");
            foreach (var genre in genres)
                _genreService.AddGenreToItem(genre.text, item, _dbContext);
            _devService.AddDevToItem(game.developpeur.text, item, _dbContext);
            _editeurService.AddEditeurToItem(game.editeur.text, item, _dbContext);
            _dbContext.Items.Update(item);
            _dbContext.SaveChanges();
        }
    }
    public static DateTime ParseDate(string dateString)
    {
        DateTime dateValue;
        var formats = new[] { "yyyy-MM-dd", "yyyy/MM/dd", "yyyy-MM", "yyyy/MM", "yyyy" };
        var culture = CultureInfo.InvariantCulture;
        var style = DateTimeStyles.None;

        foreach (var format in formats)
        {
            if (DateTime.TryParseExact(dateString, format, culture, style, out dateValue))
            {
                // If only the year is provided, set it to the first day of the year
                if (format == "yyyy")
                {
                    dateValue = new DateTime(dateValue.Year, 1, 1);
                }
                return dateValue;
            }
        }

        throw new FormatException("The date string is not in a recognized format.");
    }
    public IEnumerable<LUEmulateur> GetLocalEmulator()
    {
        return _dbContext.Emulateurs.Where(x => x.IsLocal);
    }
    public IEnumerable<LUProfile> GetLocalEmulatorProfile()
    {
        var localemus = _dbContext.Emulateurs.Where(x => x.IsLocal);
        var localprofiles = new List<LUProfile>();
        foreach (var emu in localemus)
        {
            var templocalprofiles = _dbContext.Profiles.Where(x => x.LUEmulateurId == emu.Id && x.IsLocal);
            foreach (var item in templocalprofiles)
            {
                var profilename = item.Name;
                item.Name = $"{emu.Name} - {profilename}";
                localprofiles.Add(item);
            }
        }
        return localprofiles;
    }
    public IEnumerable<LUEmulateur> ScanFolderForEmulator(string directoryPath)
    {
        SendNotification(MsgCategory.StartTask, "Début de Scan d'émulateur", $"Début du scan dans le dossier {directoryPath}");
        List<LUEmulateur> reponse = new List<LUEmulateur>();
        if (Directory.Exists(directoryPath))
        {
            try
            {
                reponse.AddRange(RecursiveScan(directoryPath));
            }
            catch (Exception ex)
            {
                SendNotification(MsgCategory.Error, ex.Source, ex.Message);
            }
        }
        else
        {
            SendNotification(MsgCategory.Error, "Folder Not Found", $"Le dossier {directoryPath} n'existe pas");
        }
        SendNotification(MsgCategory.EndTask, "Fin de Scan", $"Fin de Scan d'émulateur dans le dossier {directoryPath}");
        return reponse.GroupBy(x => x.Name).Select(grp => grp.First()).ToList();
    }
    public IEnumerable<LUEmulateur> RecursiveScan(string directoryPath)
    {
        // Get all files in the current directory and add them to the database
        var files = Directory.GetFiles(directoryPath, "*.exe");

        foreach (var file in files)
        {
            var profiles = _dbContext.Profiles.ToList();
            var filename = Path.GetFileName(file);
            var currentprofiles = profiles.Where(x => DeFormatExe(x.StartupExecutable).Equals(filename, StringComparison.OrdinalIgnoreCase));
            var localprofiles = new List<LUEmulateur>();
            foreach (var profile in currentprofiles)
            {
                if (profile.ProfileFiles != null && profile.ProfileFiles.Any())
                {
                    var corepath = Path.Combine(Path.GetDirectoryName(file), profile.ProfileFiles.First().Replace("'", string.Empty));
                    if (File.Exists(corepath))
                    {
                        profile.StartupExecutable = file;
                        profile.IsLocal = true;
                    }
                }
                else
                {
                    profile.StartupExecutable = file;
                    profile.IsLocal = true;
                }
                _dbContext.Profiles.Update(profile);
                _dbContext.SaveChanges(true);
                var emu = _dbContext.Emulateurs.FirstOrDefault(x => x.Id == profile.LUEmulateurId);
                if (emu != null)
                {
                    emu.IsLocal = true;
                    _dbContext.Emulateurs.Update(emu);
                    _dbContext.SaveChanges(true);
                    localprofiles.Add(emu);
                    CreateEmuAsItem(emu, profile, _dbContext);
                    SendNotification(MsgCategory.Create, "Profil d'émulation trouvé", $"Emulateur {emu.Name}-{profile.Name} enregistré");
                }
            }
            foreach (var localprofile in localprofiles)
                yield return localprofile;
        }

        // Recursively scan all subdirectories
        var directories = Directory.GetDirectories(directoryPath);
        foreach (var directory in directories)
        {
            foreach (var item in RecursiveScan(directory))
                yield return item;
        }
    }

    private void CreateEmuAsItem(LUEmulateur emu, LUProfile profile, GameLauncherContext dbContext)
    {
        if (!_dbContext.Items.Any(x => x.Name == emu.Name))
        {
            Item itememu = new Item();
            itememu.Name = emu.Name;
            itememu.SearchName = emu.Name;
            itememu.Path = profile.StartupExecutable;
            itememu.LUPlatformesId = _dbContext.Platformes.First(x => x.Name == "Emulators").Codename;
            itememu.AddingDate = DateTime.Now;
            itememu.Develloppeurs = new List<ItemDev>();
            itememu.Editeurs = new List<ItemEditeur>();
            itememu.Genres = new List<ItemGenre>();
            itememu.StoreId = string.Empty;
            itememu.Logo = string.Empty;
            itememu.Cover = string.Empty;
            itememu.Banner = string.Empty;
            itememu.Artwork = string.Empty;
            itememu.Video = string.Empty;
            itememu.Description = string.Empty;
            itememu.ReleaseDate = DateTime.MinValue;
            dbContext.Items.Add(itememu);
            var listgames = _steangriddbService.SearchByName(itememu.SearchName);
            var firstgame = listgames.FirstOrDefault();
            if (firstgame != null)
            {
                var assetfolder = _assetDownloader.CreateItemAssetFolder(itememu.ID);
                var listlogo = _steangriddbService.GetLogoForId(firstgame.id);
                var listboxart = _steangriddbService.GetGridBoxartForId(firstgame.id);
                var listhero = _steangriddbService.GetHeroesForId(firstgame.id);
                var firstlogo = listlogo.FirstOrDefault();
                var firstboxart = listboxart.FirstOrDefault();
                var firsthero = listhero.FirstOrDefault();
                if (firstlogo != null)
                {
                    _assetDownloader.DownloadFile(firstlogo.url, Path.Combine(assetfolder, "logo.png"));
                    itememu.Logo = Path.Combine(assetfolder, "logo.png");
                }
                if (firstboxart != null)
                {
                    _assetDownloader.DownloadFile(firstboxart.url, Path.Combine(assetfolder, "cover.jpg"));
                    itememu.Cover = Path.Combine(assetfolder, "cover.jpg");
                }
                if (firsthero != null)
                {
                    _assetDownloader.DownloadFile(firsthero.url, Path.Combine(assetfolder, "banner.jpg"));
                    itememu.Banner = Path.Combine(assetfolder, "banner.jpg");
                }
            }
            //dbContext.Items.Update(itememu);
            SendNotification(MsgCategory.Create, "Ajout d'un emulateur dans les Items", $"Ajout de {itememu.Name} dans les Items");
            dbContext.SaveChanges();
        }
    }

    public async IAsyncEnumerable<LUEmulateur> RecursiveScanAsync(string directoryPath)
    {
        // Get all files in the current directory and add them to the database
        var files = Directory.GetFiles(directoryPath, "*.exe");
        foreach (var file in files)
        {
            var profiles = _dbContext.Profiles.ToList();
            var filename = Path.GetFileName(file);
            var profile = profiles.FirstOrDefault(x => DeFormatExe(x.StartupExecutable).Equals(filename, StringComparison.OrdinalIgnoreCase));
            if (profile != null)
            {
                profile.StartupExecutable = file;
                _dbContext.Profiles.Update(profile);
                _dbContext.SaveChanges(true);
                var emu = _dbContext.Emulateurs.FirstOrDefault(x => x.Id == profile.LUEmulateurId);
                if (emu != null)
                {
                    emu.IsLocal = true;
                    _dbContext.Emulateurs.Update(emu);
                    _dbContext.SaveChanges(true);
                    yield return emu;
                }
            }
        }

        // Recursively scan all subdirectories
        var directories = Directory.GetDirectories(directoryPath);
        foreach (var directory in directories)
        {
            await foreach (var item in RecursiveScanAsync(directory))
                yield return item;
        }
    }
    private string DeFormatExe(string? exePath)
    {
        if (!string.IsNullOrWhiteSpace(exePath))
        {
            var result = exePath.Replace("^", string.Empty).Replace("$", string.Empty).Replace(".*", string.Empty).Replace("\\", string.Empty);
            return result;
        }
        else
            return string.Empty;
    }
    private string DeFormatProfilePath(string exePath)
    {
        return exePath.Replace("^", string.Empty).Replace("$", string.Empty).Replace("\u0027", string.Empty);
    }
}
