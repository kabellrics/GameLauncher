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
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NexusMods.Paths;

namespace GameLauncher.Services.Implementation;
public class EmulateurService : IEmulateurService
{
    private readonly GameLauncherContext dbContext;
    private readonly IItemsService itemsService;
    private readonly IHubContext<SignalRNotificationHub, INotificationService> notifService;
    private readonly ISteamGridDbService steangriddbService;
    private readonly IIGDBService igdbService;
    private readonly IScreenscraperService screenscraperService;
    private readonly IGenreService genreService;
    private readonly IDevService devService;
    private readonly IEditeurService editeurService;
    public EmulateurService(GameLauncherContext dbContext, IHubContext<SignalRNotificationHub, INotificationService> notifService, ISteamGridDbService steangriddbService
        ,IIGDBService igdbService, IScreenscraperService screenscraperService,IItemsService itemsService
        ,IGenreService genreService,IDevService devService,IEditeurService editeurService)
    {
        this.dbContext = dbContext;
        this.notifService = notifService;
        this.steangriddbService = steangriddbService;
        this.igdbService = igdbService;
        this.screenscraperService = screenscraperService;
        this.genreService = genreService;
        this.editeurService = editeurService;
        this.devService = devService;
        this.itemsService = itemsService;
    }
    public async Task ScanFolderForRom(ScanProfile scanprofile)
    {
        var filewithcorrectextension = new List<string>();
        foreach (var ext in scanprofile.Profile.ImageExtensions)
        {
            var searchPattern = $"*.{ext}";
            filewithcorrectextension.AddRange(Directory.GetFiles(scanprofile.FolderPath, searchPattern, SearchOption.TopDirectoryOnly));
        }
        if(filewithcorrectextension.Count == 0) { return; }
        foreach (var file in filewithcorrectextension)
        {
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
            if (scanprofile.MetaProvider == MetadataProvider.Screenscraper)
            {
                GetSSCPMetadata(file, item);
            }
            else if (scanprofile.MetaProvider == MetadataProvider.IGDB)
            {
                GetIGDBMetadata(item);
            }
            if (scanprofile.LogoProvider == LogoProvider.SteamGridDB) { GetSGDBLogo(item); }
            else if (scanprofile.LogoProvider == LogoProvider.Screenscraper) { }

            if (scanprofile.CoverProvider == CoverProvider.SteamGridDB) { GetSGDBCover(item); }
            else if (scanprofile.CoverProvider == CoverProvider.Screenscraper) { }
            else if (scanprofile.CoverProvider == CoverProvider.IGDB) { GetIGDBCover(item); }

            if (scanprofile.FanartProvider == FanartProvider.Screenscraper) { }
            else if (scanprofile.FanartProvider == FanartProvider.IGDB) { GetIGDBFanart(item); }

            if (scanprofile.VideoProvider == VideoProvider.Screenscraper) {  }
            else if (scanprofile.VideoProvider == VideoProvider.IGDB) { }

            GetSGDBHeroes(item);
            itemsService.UpdateItem(item);
        }
    }

    private void GetSSCPLogo(Item item)
    {
        var sscpgames = screenscraperService.SearchGameByFileName(item.Path);
        var game = sscpgames.FirstOrDefault();
        item.Logo = game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "fr")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "eu")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "ss")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "wor")?.url ??
                        string.Empty;
    }
    private void GetSSCPCover(Item item)
    {
        var sscpgames = screenscraperService.SearchGameByFileName(item.Path);
        var game = sscpgames.FirstOrDefault();
        item.Cover = game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "fr")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "eu")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "ss")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "wor")?.url ??
                        string.Empty;
    }
    private void GetSSCPFanart(Item item)
    {
        var sscpgames = screenscraperService.SearchGameByFileName(item.Path);
        var game = sscpgames.FirstOrDefault();
        var artwork = game.medias.FirstOrDefault(x => x.type == "fanart")?.url ?? string.Empty;
        var screen = game.medias.FirstOrDefault(x => x.type == "ss")?.url ?? string.Empty;
        var screentitle = game.medias.FirstOrDefault(x => x.type == "sstitle")?.url ?? string.Empty;
        var screen169 = game.medias.FirstOrDefault(x => x.type == "ssfronton16-9")?.url ?? string.Empty;
        if(!string.IsNullOrEmpty(artwork)){item.Artwork = artwork;}
        else if(!string.IsNullOrEmpty(screen)){item.Artwork = screen; }
        else if(!string.IsNullOrEmpty(screentitle)){item.Artwork = screentitle; }
        else if(!string.IsNullOrEmpty(screen169)){item.Artwork = screen169; }
    }
    private void GetSSCPVideo(Item item)
    {
        var sscpgames = screenscraperService.SearchGameByFileName(item.Path);
        var game = sscpgames.FirstOrDefault();
        item.Video = game.medias.FirstOrDefault(x => x.type == "video")?.url ?? string.Empty;
    }
    private void GetSGDBCover(Item item)
    {
        var sgdbsearch = steangriddbService.SearchByName(item.SearchName);
        if (sgdbsearch != null)
        {
            var sgdbfirstgame = sgdbsearch.First();
            if (sgdbfirstgame != null)
            {
                var heroes = steangriddbService.GetGridBoxartForId(sgdbfirstgame.id);
                item.Cover = heroes.First().url;
            }
        }
    }
    private void GetSGDBLogo(Item item)
    {
        var sgdbsearch = steangriddbService.SearchByName(item.SearchName);
        if (sgdbsearch != null)
        {
            var sgdbfirstgame = sgdbsearch.First();
            if (sgdbfirstgame != null)
            {
                var heroes = steangriddbService.GetLogoForId(sgdbfirstgame.id);
                item.Logo = heroes.First().url;
            }
        }
    }
    private void GetSGDBHeroes(Item item)
    {
        var sgdbsearch = steangriddbService.SearchByName(item.SearchName);
        if (sgdbsearch != null)
        {
            var sgdbfirstgame = sgdbsearch.First();
            if (sgdbfirstgame != null)
            {
                var heroes = steangriddbService.GetHeroesForId(sgdbfirstgame.id);
                item.Banner = heroes.First().url;
            }
        }
    }
    private void GetIGDBCover(Item item)
    {
        var igdbgames = igdbService.GetGameByName(item.SearchName);
        if(igdbgames != null)
        {
            var igdbgame = igdbgames.FirstOrDefault();
            if(igdbgame != null)
            {
                var detailgame = igdbService.GetDetailsGame(igdbgame.id);
                if(detailgame != null)
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
        var igdbgames = igdbService.GetGameByName(item.SearchName);
        if (igdbgames != null)
        {
            var igdbgame = igdbgames.FirstOrDefault();
            if (igdbgame != null)
            {
                var detailgame = igdbService.GetDetailsGame(igdbgame.id);
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
        var igdbgames = igdbService.GetGameByName(item.Name);
        if (igdbgames != null && igdbgames.Any())
        {
            var game = igdbService.GetDetailsGame(igdbgames.First().id);
            if (game != null)
            {
                item.Name = game.name;
                item.SearchName = game.name;
                item.Description = game.summary;
                item.ReleaseDate = ConvertFromIGDB(game.first_release_date);
                dbContext.Items.Add(item);
                var genres = game.genres.Select(g => g.name);
                foreach (var genre in genres)
                    genreService.AddGenreToItem(genre, item, dbContext);
                var devs = igdbService.GetCompaniesDetail(game.involved_companies.Where(x => x.developer).Select(x => x.company.ToString()));
                foreach (var dev in devs)
                    devService.AddDevToItem(dev.name, item, dbContext);
                var editeurs = igdbService.GetCompaniesDetail(game.involved_companies.Where(x => x.publisher).Select(x => x.company.ToString()));
                foreach (var editeur in editeurs)
                    editeurService.AddEditeurToItem(editeur.name, item, dbContext);
                dbContext.Items.Update(item);
                dbContext.SaveChanges();
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
    private void GetSSCPMetadata(string file, Item item)
    {
        var sscpgames = screenscraperService.SearchGameByFileName(file);
        var game = sscpgames.FirstOrDefault();
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
            dbContext.Items.Add(item);
            var genres = game.genres.SelectMany(x => x.noms).Where(x => x.langue == "fr");
            foreach (var genre in genres)
                genreService.AddGenreToItem(genre.text, item, dbContext);
            devService.AddDevToItem(game.developpeur.text, item, dbContext);
            editeurService.AddEditeurToItem(game.editeur.text, item, dbContext);
            dbContext.Items.Update(item);
            dbContext.SaveChanges();
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
        return dbContext.Emulateurs.Where(x => x.IsLocal);
    }
    public IEnumerable<LUProfile> GetLocalEmulatorProfile()
    {
        var localemus = dbContext.Emulateurs.Where(x => x.IsLocal);
        var localprofiles = new List<LUProfile>();
        foreach (var emu in localemus)
        {
            var templocalprofiles = dbContext.Profiles.Where(x => x.LUEmulateurId == emu.Id && x.IsLocal);
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
        notifService.Clients.All.SendMessage(new NotificationMessage { Type = Models.APIObject.MsgCategory.StartTask, MessageTitle="Début de Scan d'émulateur", MessageCorps = $"Début du scan dans le dossier {directoryPath}" });
        List<LUEmulateur> reponse = new List<LUEmulateur>();
        if (Directory.Exists(directoryPath))
        {
            try
            {
                reponse.AddRange(RecursiveScan(directoryPath));
            }
            catch (Exception ex)
            {
                notifService.Clients.All.SendMessage(new Models.APIObject.NotificationMessage { Type = Models.APIObject.MsgCategory.Error, MessageTitle = ex.ToString(), MessageCorps = ex.Message });
            }
        }
        else
        {
            notifService.Clients.All.SendMessage(new Models.APIObject.NotificationMessage { Type = Models.APIObject.MsgCategory.Error, MessageTitle = "Folder Not Found", MessageCorps = $"Le dossier {directoryPath} n'existe pas" });
        }
        notifService.Clients.All.SendMessage(new Models.APIObject.NotificationMessage { Type = Models.APIObject.MsgCategory.EndTask, MessageTitle = "Fin de Scan", MessageCorps = $"Fin de Scan d'émulateur dans le dossier {directoryPath}" });
        return reponse.GroupBy(x => x.Name).Select(grp => grp.First()).ToList();
    }
    public IEnumerable<LUEmulateur> RecursiveScan(string directoryPath)
    {
        // Get all files in the current directory and add them to the database
        var files = Directory.GetFiles(directoryPath, "*.exe");

        foreach (var file in files)
        {
            var profiles = dbContext.Profiles.ToList();
            var filename = Path.GetFileName(file);
            var currentprofiles = profiles.Where(x => DeFormatExe(x.StartupExecutable).Equals(filename, StringComparison.OrdinalIgnoreCase));
            var localprofiles = new List<LUEmulateur>();
            foreach (var profile in currentprofiles)
            {
                    if (profile.ProfileFiles != null && profile.ProfileFiles.Any())
                    {
                        var corepath = Path.Combine(Path.GetDirectoryName(file), profile.ProfileFiles.First().Replace("'",string.Empty));
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
                dbContext.Profiles.Update(profile);
                dbContext.SaveChanges(true);
                var emu = dbContext.Emulateurs.FirstOrDefault(x => x.Id == profile.LUEmulateurId);
                if (emu != null)
                {
                    emu.IsLocal = true;
                    dbContext.Emulateurs.Update(emu);
                    dbContext.SaveChanges(true);
                    localprofiles.Add(emu);
                    notifService.Clients.All.SendMessage(new Models.APIObject.NotificationMessage { Type=Models.APIObject.MsgCategory.Create,MessageTitle="Profil d'émulation trouvé", MessageCorps=$"Emulateur {emu.Name}-{profile.Name} enregistré"});
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
    public async IAsyncEnumerable<LUEmulateur> RecursiveScanAsync(string directoryPath)
    {
        // Get all files in the current directory and add them to the database
        var files = Directory.GetFiles(directoryPath, "*.exe");
        foreach (var file in files)
        {
            var profiles = dbContext.Profiles.ToList();
            var filename = Path.GetFileName(file);
            var profile = profiles.FirstOrDefault(x => DeFormatExe(x.StartupExecutable).Equals(filename, StringComparison.OrdinalIgnoreCase));
            if (profile != null)
            {
                profile.StartupExecutable = file;
                dbContext.Profiles.Update(profile);
                dbContext.SaveChanges(true);
                var emu = dbContext.Emulateurs.FirstOrDefault(x => x.Id == profile.LUEmulateurId);
                if (emu != null)
                {
                    emu.IsLocal = true;
                    dbContext.Emulateurs.Update(emu);
                    dbContext.SaveChanges(true);
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
