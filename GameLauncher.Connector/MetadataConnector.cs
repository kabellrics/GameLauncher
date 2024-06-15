using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Models;
using GameLauncher.Models.IGDB;
using GameLauncher.Models.ScreenScraper;
using GameLauncher.Models.SteamGridDB;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.Connector;
public class MetadataConnector
{
    private readonly RestClient _client;

    public MetadataConnector(string baseUrl)
    {
        _client = new RestClient(baseUrl);
    }
    public async Task<IEnumerable<IGDBGame>> GetIGDBGameByName(string name)
    {
        var request = new RestRequest($"api/IGDB/GetGameByName/{name}", Method.Get);
        //var request = new RestRequest("api/IGDB/GetGameByName", Method.Get);
        //request.AddParameter("name", name);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<IGDBGame>>(response.Content);
        }
        else
        {
            return new List<IGDBGame>();
        }
    }
    public async Task<IEnumerable<Video>> GetIGDBVideosByGameId(int id)
    {
        var request = new RestRequest($"api/IGDB/GetGameVideo/{id}", Method.Get);
        //request.AddParameter("id", id);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Video>>(response.Content);
        }
        else
        {
            return new List<Video>();
        }
    }
    public async Task<IGDBGame> GetIGDBDetailsGame(int id)
    {
        var request = new RestRequest($"api/IGDB/GetDetailsGame/{id}", Method.Get);
        //request.AddParameter("id", id);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IGDBGame>(response.Content);
        }
        else
        {
            return null;
        }
    }
    public async Task<IEnumerable<Company>> GetIGDBCompany(List<InvolvedCompany> ids)
    {
        var request = new RestRequest($"api/IGDB/GetCompaniesName/{string.Join(',', ids.Select(x => x.company.ToString()))}", Method.Get);
        //request.AddParameter("ids", string.Join(',',ids.Select(x=>x.company.ToString())));
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Company>>(response.Content);
        }
        else
        {
            return new List<Company>();
        }
    }

    public async Task<IEnumerable<Jeux>> SearchScreenscraperGameByFileName(string filename)
    {
        var request = new RestRequest($"api/Screenscraper/SearchGameByFileName/{filename}", Method.Get);
        //request.AddParameter("filename", filename);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Jeux>>(response.Content);
        }
        else
        {
            return new List<Jeux>();
        }
    }
    public async Task<IEnumerable<Jeux>> SearchScreenscraperGameByName(string name)
    {
        var request = new RestRequest($"api/Screenscraper/SearchByName/{name}", Method.Get);
        //request.AddParameter("name", name);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Jeux>>(response.Content);
        }
        else
        {
            return new List<Jeux>();
        }
    }

    public async Task<IEnumerable<DataSearch>> SearchSteamGridDBGameByName(string name)
    {
        var request = new RestRequest($"api/SteamGridDB/SearchByName/{name}", Method.Get);
        //request.AddParameter("name", name);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<DataSearch>>(response.Content);
        }
        else
        {
            return new List<DataSearch>();
        }
    }
    public async Task<IEnumerable<ImgResult>> SearchSteamGridDBBannerFor(int gameId)
    {
        var request = new RestRequest($"api/SteamGridDB/GetHeroesForId/{gameId}", Method.Get);
        //request.AddParameter("gameid", gameId);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<ImgResult>>(response.Content);
        }
        else
        {
            return new List<ImgResult>();
        }
    }
    public async Task<IEnumerable<ImgResult>> SearchSteamGridDBLogoFor(int gameId)
    {
        var request = new RestRequest($"api/SteamGridDB/GetLogoForId/{gameId}", Method.Get);
        //request.AddParameter("gameid", gameId);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<ImgResult>>(response.Content);
        }
        else
        {
            return new List<ImgResult>();
        }
    }
    public async Task<IEnumerable<ImgResult>> SearchSteamGridDBBoxartFor(int gameId)
    {
        var request = new RestRequest($"api/SteamGridDB/GetGridBoxartForId/{gameId}", Method.Get);
        //request.AddParameter("gameid", gameId);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<ImgResult>>(response.Content);
        }
        else
        {
            return new List<ImgResult>();
        }
    }
}
