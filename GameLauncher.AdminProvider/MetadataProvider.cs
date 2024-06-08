using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Connector;
using GameLauncher.Models;
using GameLauncher.Models.IGDB;
using GameLauncher.Models.SteamGridDB;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.AdminProvider;
public class MetadataProvider
{
    private readonly MetadataConnector apiconnector;
    public MetadataProvider()
    {
        apiconnector = new MetadataConnector("https://localhost:7197");
    }
    public async Task<IEnumerable<DataSearch>> SearchSteamGridDBGameByName(string name)
    {
        return await apiconnector.SearchSteamGridDBGameByName(name);
    }
    public async Task<IEnumerable<ImgResult>> SearchSteamGridDBBannerFor(int gameId)
    {
        return await apiconnector.SearchSteamGridDBBannerFor(gameId);
    }
    public async Task<IEnumerable<ImgResult>> SearchSteamGridDBLogoFor(int gameId)
    {
        return await apiconnector.SearchSteamGridDBLogoFor(gameId);
    }
    public async Task<IEnumerable<ImgResult>> SearchSteamGridDBBoxartFor(int gameId)
    {
        return await apiconnector.SearchSteamGridDBBoxartFor(gameId);
    }

    public async Task<IEnumerable<SearchResult>> GetIGDBGameByName(string name)
    {
        return await apiconnector.GetIGDBGameByName(name);
    }
    public async Task<IEnumerable<Video>> GetIGDBVideosByGameId(int id)
    {
        return await apiconnector.GetIGDBVideosByGameId(id);
    }
    public async Task<Item> GetIGDBDetailsGame(int id)
    {
        var igdbGame = await apiconnector.GetIGDBDetailsGame(id);
        return await ItemFromIGDBGame(igdbGame);
    }
    private async Task<Item> ItemFromIGDBGame(IGDBGame game)
    {
        Item item = new Item();
        item.Name = game.name;
        item.SearchName = game.name;
        item.Description = game.summary;
        item.ReleaseDate = DateTime.Parse(game.first_release_date.ToString());
        item.Genres = game.genres.Select(x => new Models.Genre { ID = Guid.NewGuid(), Name = x.name, Items = new List<Item>() {item} }).ToList();
        item.Artwork = game.artworks.FirstOrDefault().url ?? System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
        item.Cover = game.cover.url ?? System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "default.png");
        var devs = await apiconnector.GetIGDBCompany(game.involved_companies.Where(x => x.developer).ToList());
        item.Develloppeurs = devs.Select(x=> new Develloppeur { ID=Guid.NewGuid(),Name=x.name, Items = new List<Item>() { item } } ).ToList();
        var edits = await apiconnector.GetIGDBCompany(game.involved_companies.Where(x => x.publisher).ToList());
        item.Editeurs = edits.Select(x=> new Editeur { ID=Guid.NewGuid(),Name=x.name, Items = new List<Item>() { item } } ).ToList();
        return item;
    }
}
