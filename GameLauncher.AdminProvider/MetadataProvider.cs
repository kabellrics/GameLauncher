﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Connector;
using GameLauncher.Models;
using GameLauncher.Models.IGDB;
using GameLauncher.Models.ScreenScraper;
using GameLauncher.Models.SteamGridDB;
using GameLauncher.ObservableObjet;
using GameLauncher.Services.Interface;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.AdminProvider;
public class MetadataProvider : IMetadataProvider
{
    //private readonly MetadataConnector apiconnector;
    private readonly IIGDBService igdbService;
    private readonly IScreenscraperService screenscraperService;
    private readonly ISteamGridDbService steamGridService;
    public MetadataProvider(IIGDBService igdb, IScreenscraperService screenscraper, ISteamGridDbService steamGrid)
    {
        //apiconnector = new MetadataConnector("https://localhost:7197");
        igdbService = igdb;
        screenscraperService = screenscraper;
        steamGridService = steamGrid;
    }
    public async Task<IEnumerable<DataSearch>> SearchSteamGridDBGameByName(string name)
    {
        return steamGridService.SearchByName(name);
    }
    public async Task<IEnumerable<ImgResult>> SearchSteamGridDBBannerFor(int gameId)
    {
        return steamGridService.GetGridBannerForId(gameId);
    }
    public async Task<IEnumerable<ImgResult>> SearchSteamGridDBHeroFor(int gameId)
    {
        return steamGridService.GetHeroesForId(gameId);
    }
    public async Task<IEnumerable<ImgResult>> SearchSteamGridDBLogoFor(int gameId)
    {
        return steamGridService.GetLogoForId(gameId);
    }
    public async Task<IEnumerable<ImgResult>> SearchSteamGridDBBoxartFor(int gameId)
    {
        return steamGridService.GetGridBoxartForId(gameId);
    }
    public async Task<ObservableMediaItem> GetSteamGridDBMediaItem(DataSearch game)
    {
        ObservableMediaItem mediaItem = new ObservableMediaItem();
        mediaItem.Name = game.name;
        mediaItem.Date = ConvertFromIGDB(game.release_date).ToString();
        var logos = await SearchSteamGridDBLogoFor(game.id);
        foreach (var item in logos)
            mediaItem.Logos.Add(item.url);
        var boxarts = await SearchSteamGridDBBoxartFor(game.id);
        foreach (var item in boxarts)
            mediaItem.Covers.Add(item.url);
        var banners = await SearchSteamGridDBHeroFor(game.id);
        foreach (var item in banners)
            mediaItem.Banners.Add(item.url);
        return mediaItem;
    }
    public async Task<IEnumerable<ObservableMediaItem>> GetIGDBMediaGameByName(string name)
    {
        var result = new List<ObservableMediaItem>();
        var callresult = igdbService.GetGameByName(name);
        foreach (var item in callresult)
            result.Add(await MediaItemFromIGDBGame(item));
        return result;
    }
    public async Task<IEnumerable<ObservableItem>> GetIGDBGameByName(string name)
    {
        var result = new List<ObservableItem>();
        var callresult = igdbService.GetGameByName(name);
        foreach (var item in callresult)
            result.Add(await ItemFromIGDBGame(item));
        return result;
    }
    public async IAsyncEnumerable<ObservableItem> GetIGDBGameByNameAsync(string name)
    {
        var callresult = igdbService.GetGameByName(name);
        foreach (var item in callresult)
            yield return await ItemFromIGDBGame(item);
    }
    public async Task<IEnumerable<Video>> GetIGDBVideosByGameId(int id)
    {
        return igdbService.GetVideosByGameId(id);
    }
    public async Task<ObservableItem> GetIGDBDetailsGame(int id)
    {
        var igdbGame = igdbService.GetDetailsGame(id);
        return await ItemFromIGDBGame(igdbGame);
    }
    public async Task<IEnumerable<ObservableItem>> GetSearchScreenscraperGameByFileName(string filename)
    {
        var result = new List<ObservableItem>();
        var callresult = screenscraperService.SearchGameByFileName(filename);
        foreach (var item in callresult)
            result.Add(await ItemFromScreenscraperGame(item));
        return result;
    }
    public async IAsyncEnumerable<ObservableItem> SearchScreenscraperGameByNameAsync(string name)
    {
        var callresult = screenscraperService.SearchGameByName(name);
        foreach (var item in callresult)
            yield return await ItemFromScreenscraperGame(item);
    }
    public async Task<IEnumerable<ObservableItem>> SearchScreenscraperGameByName(string name)
    {
        var result = new List<ObservableItem>();
        var callresult = screenscraperService.SearchGameByName(name);
        foreach (var item in callresult)
            result.Add(await ItemFromScreenscraperGame(item));
        return result;
    }
    public async Task<IEnumerable<ObservableMediaItem>> SearchScreenscraperGameMediaByName(string name)
    {
        var result = new List<ObservableMediaItem>();
        var callresult = screenscraperService.SearchGameByName(name);
        foreach (var item in callresult)
            result.Add(await MediaItemFromScreenscraperGame(item));
        return result;
    }
    private async Task<ObservableMediaItem> MediaItemFromIGDBGame(IGDBGame game)
    {
        ObservableMediaItem item = new ObservableMediaItem();
        item.Name = game.name;
        item.Date = ConvertFromIGDB(game.first_release_date).ToString();
        var cover = game.cover?.url ?? string.Empty;
        if (!string.IsNullOrEmpty(cover))
            {
            var filename = Path.GetFileName(cover);
            item.Covers.Add($"https://images.igdb.com/igdb/image/upload/t_cover_big/" + filename);
        }
        var artworks = game.artworks;
        if (artworks != null)
        {
            foreach (var artwork in artworks)
            {
                var art = artwork?.url ?? string.Empty;
                if (!string.IsNullOrEmpty(art))
                {
                    var filename = Path.GetFileName(art);
                    item.Artworks.Add($"https://images.igdb.com/igdb/image/upload/t_1080p/" + filename);
                }
            } 
        }
        return item;
    }
    private async Task<ObservableItem> ItemFromIGDBGame(IGDBGame game)
    {
        Item item = new Item();
        item.Name = game.name;
        item.SearchName = game.name;
        item.Description = game.summary;
        item.ReleaseDate = ConvertFromIGDB(game.first_release_date);
        try
        {
            item.Artwork = game.artworks.FirstOrDefault()?.url ?? System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");

        }
        catch (Exception ex) { /*throw;*/ }
        try
        {
            item.Cover = game.cover?.url ?? System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
        }
        catch (Exception ex) { /*throw;*/ }
        var obsItem = new ObservableItem(item);
        try
        {
            obsItem.Genres = new ObservableCollection<ObservableGenre>(game.genres.Select(x =>new ObservableGenre(new Models.Genre { ID = Guid.NewGuid(), Name = x.name})));
        }
        catch (Exception ex) {/*throw;*/}
        try
        {
            var devs = igdbService.GetCompaniesDetail(game.involved_companies.Where(x => x.developer).Select(x=>x.company.ToString()).ToList());
            obsItem.Develloppeurs = new ObservableCollection<ObservableDevelloppeur>(devs.Select(x =>new ObservableDevelloppeur( new Develloppeur { ID = Guid.NewGuid(), Name = x.name})));
        }
        catch (Exception ex) { /*throw;*/ }
        try
        {
            var edits = igdbService.GetCompaniesDetail(game.involved_companies.Where(x => x.publisher).Select(x => x.company.ToString()).ToList());
            obsItem.Editeurs = new ObservableCollection<ObservableEditeur>(edits.Select(x => new ObservableEditeur(new Models.Editeur { ID = Guid.NewGuid(), Name = x.name })));
        }
        catch (Exception ex) { /*throw;*/ }
        return obsItem;
    }
    private DateTime ConvertFromIGDB(int value)
    {
        var result = DateTime.MinValue;
        try
        {
            long parsedUnixTimestamp;
            if (long.TryParse(value.ToString(), out parsedUnixTimestamp));
            return DateTimeOffset.FromUnixTimeSeconds(parsedUnixTimestamp).DateTime;
        }
        catch(Exception ex) { }
        return result;
    }
    private async Task<ObservableMediaItem> MediaItemFromScreenscraperGame(Jeux game)
    {
        ObservableMediaItem item = new ObservableMediaItem();
        if (game.noms != null)
        {
            item.Name = game.noms.FirstOrDefault(x => x.region == "fr")?.text ??
                game.noms.FirstOrDefault(x => x.region == "eu")?.text ??
                game.noms.FirstOrDefault(x => x.region == "ss")?.text ??
                game.noms.FirstOrDefault(x => x.region == "wor")?.text; 
        }
        if (game.dates != null)
        {
            item.Date = game.dates.FirstOrDefault(x => x.region == "fr")?.text ??
                     game.dates.FirstOrDefault(x => x.region == "eu")?.text ??
                     game.dates.FirstOrDefault(x => x.region == "ss")?.text ??
                     game.dates.FirstOrDefault(x => x.region == "wor")?.text ?? string.Empty; 
        }
        if (game.medias !=null)
        {
            var video = game.medias.FirstOrDefault(x => x.type == "video")?.url ?? string.Empty;
            if (!string.IsNullOrEmpty(video))
                item.Videos.Add(video);
            var cover = game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "fr")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "eu")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "ss")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "wor")?.url ??
                        string.Empty;
            if (!string.IsNullOrEmpty(cover))
                item.Covers.Add(cover);
            var logo = game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "fr")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel" && x.region == "fr")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "eu")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel" && x.region == "eu")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "ss")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel" && x.region == "ss")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "wor")?.url ??
                        game.medias.FirstOrDefault(x => x.type == "wheel" && x.region == "wor")?.url ??
                        string.Empty;
            if (!string.IsNullOrEmpty(logo))
                item.Logos.Add(logo);
            var artwork = game.medias.FirstOrDefault(x => x.type == "fanart")?.url ?? string.Empty;
            var screen = game.medias.FirstOrDefault(x => x.type == "ss")?.url ?? string.Empty;
            var screentitle = game.medias.FirstOrDefault(x => x.type == "sstitle")?.url ?? string.Empty;
            var screen169 = game.medias.FirstOrDefault(x => x.type == "ssfronton16-9")?.url ?? string.Empty;
            if (!string.IsNullOrEmpty(artwork))
                item.Artworks.Add(artwork);
            if (!string.IsNullOrEmpty(screen))
                item.Artworks.Add(screentitle);
            if (!string.IsNullOrEmpty(screentitle))
                item.Artworks.Add(screentitle);
            if (!string.IsNullOrEmpty(screen169))
                item.Artworks.Add(screen169); 
        }
        return item;
    }
    private async Task<ObservableItem> ItemFromScreenscraperGame(Jeux game)
    {
        Item item = new Item();
        item.Name = game.noms.FirstOrDefault(x => x.region == "fr")?.text ??
                    game.noms.FirstOrDefault(x => x.region == "eu")?.text ??
                    game.noms.FirstOrDefault(x => x.region == "ss")?.text ??
                    game.noms.FirstOrDefault(x => x.region == "wor")?.text;
        item.SearchName = game.noms.FirstOrDefault(x => x.region == "wor")?.text ??
                    game.noms.FirstOrDefault(x => x.region == "us")?.text ??
                    game.noms.FirstOrDefault(x => x.region == "ss")?.text ??
                    game.noms.FirstOrDefault(x => x.region == "eu")?.text ??
                    game.noms.FirstOrDefault(x => x.region == "fr")?.text;
        if (game.synopsis != null)
        {
            item.Description = game?.synopsis.FirstOrDefault(x => x.langue == "fr")?.text ?? game.synopsis.FirstOrDefault()?.text ?? string.Empty;
        }
        else
            item.Description = string.Empty;
        string stringdate;
        if (game.dates != null)
        {
            stringdate = game.dates.FirstOrDefault(x => x.region == "fr")?.text ??
                     game.dates.FirstOrDefault(x => x.region == "eu")?.text ??
                     game.dates.FirstOrDefault(x => x.region == "ss")?.text ??
                     game.dates.FirstOrDefault(x => x.region == "wor")?.text ?? string.Empty; 
        }else
            stringdate = string.Empty;
        if (!string.IsNullOrEmpty(stringdate))
            item.ReleaseDate = ParseDate(stringdate);
        item.Video = game.medias.FirstOrDefault(x => x.type == "video")?.url ?? string.Empty;
        item.Artwork = game.medias.FirstOrDefault(x => x.type == "fanart")?.url ?? string.Empty;
        item.Cover = game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "fr")?.url ??
                    game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "eu")?.url ??
                    game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "ss")?.url ??
                    game.medias.FirstOrDefault(x => x.type == "box-2D" && x.region == "wor")?.url ??
                    string.Empty;
        item.Logo = game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "fr")?.url ??
                    game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "eu")?.url ??
                    game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "ss")?.url ??
                    game.medias.FirstOrDefault(x => x.type == "wheel-hd" && x.region == "wor")?.url ??
                    string.Empty;
        var obsItem = new ObservableItem(item);
        //obsItem.Genres = new List<Models.Genre>();
        //foreach (var genre in genres)
        //obsItem.Genres.Add(new Models.Genre() { ID = Guid.NewGuid(), Name = genre.text, Items = new List<Item>()  });
        obsItem.Genres = new ObservableCollection<ObservableGenre>();
        if (game.genres != null)
        {
            var genres = game?.genres.SelectMany(x => x.noms).Where(x => x.langue == "fr");
            foreach (var genre in genres)
                obsItem.Genres.Add(new ObservableGenre(new Models.Genre() { ID = Guid.NewGuid(), Name = genre.text })); 
        }
        obsItem.Develloppeurs = new ObservableCollection<ObservableDevelloppeur>();// { new Develloppeur { ID = Guid.NewGuid(), Name = game.developpeur.text } };
        obsItem.Develloppeurs.Add(new ObservableDevelloppeur(new Develloppeur { ID = Guid.NewGuid(), Name = game?.developpeur?.text ?? string.Empty }));
        obsItem.Editeurs = new ObservableCollection<ObservableEditeur>();// { new Models.Editeur { ID = Guid.NewGuid(), Name = game.editeur.text, Items = new List<Item>() } };
        obsItem.Editeurs.Add(new ObservableEditeur(new Models.Editeur { ID = Guid.NewGuid(), Name = game?.editeur?.text ?? string.Empty }));
        obsItem.Systemname = game.systeme.text ?? string.Empty;
        return obsItem;
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
}
