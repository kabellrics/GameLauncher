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
    public async Task<IEnumerable<SearchResult>> GetIGDBGameByName(string name)
    {
        var request = new RestRequest("api/IGDB/GetGameByName", Method.Get);
        request.AddParameter("name", name);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<SearchResult>>(response.Content);
        }
        else
        {
            return new List<SearchResult>();
        }
    }
    public async Task<IEnumerable<Video>> GetIGDBVideosByGameId(int id)
    {
        var request = new RestRequest("api/IGDB/GetGameVideo", Method.Get);
        request.AddParameter("id", id);
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
        var request = new RestRequest("api/IGDB/GetDetailsGame", Method.Get);
        request.AddParameter("id", id);
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
        var request = new RestRequest("api/IGDB/GetDetailsGame", Method.Get);
        request.AddParameter("ids", string.Join(',',ids.Select(x=>x.company.ToString())));
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Company>>(response.Content);
        }
        else
        {
            return new List<Company>;
        }
    }

    public async Task<IEnumerable<Jeux>> SearchScreenscraperGameByFileName(string filename)
    {
        var request = new RestRequest("api/Screenscraper/SearchGameByFileName", Method.Get);
        request.AddParameter("filename", filename);
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
        var request = new RestRequest("api/Screenscraper/SearchByName", Method.Get);
        request.AddParameter("name", name);
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
        var request = new RestRequest("api/SteamGridDB/SearchByName", Method.Get);
        request.AddParameter("name", name);
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
        var request = new RestRequest("api/SteamGridDB/GetHeroesForId", Method.Get);
        request.AddParameter("gameid", gameId);
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
        var request = new RestRequest("api/SteamGridDB/GetLogoForId", Method.Get);
        request.AddParameter("gameid", gameId);
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
        var request = new RestRequest("api/SteamGridDB/GetGridBoxartForId", Method.Get);
        request.AddParameter("gameid", gameId);
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
