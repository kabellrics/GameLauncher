using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Services.Interface;
using GameLauncher.Models.SteamGridDB;
using RestSharp;
using RestSharp.Authenticators;

namespace GameLauncher.Services.Implementation;
public class SteamGridDbService : ISteamGridDbService
{
    private string apipath = @"https://www.steamgriddb.com/api/v2";
    private RestClient sgdbclient;

    public SteamGridDbService()
    {
        var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "SSCP.gral");
        string[] lines = File.ReadAllLines(filepath);
        var options = new RestClientOptions(apipath)
        {
            Authenticator = new JwtAuthenticator(lines[4])
        };
        sgdbclient = new RestClient(options);
    }
    public IEnumerable<DataSearch> SearchByName(string name)
    {
        try
        {
            var request = new RestRequest($"/search/autocomplete/{name}", Method.Get);
            var response = sgdbclient.Execute<SearchByNameResult>(request);
            return response?.Data?.data ?? null;
        }
        catch (Exception ex)
        {
            //throw;
            return null;
        }
    }
    public DataSearch GetGameSteamId(string steamId)
    {
        try
        {
            var request = new RestRequest($"games/steam/{steamId}", Method.Get);
            var response = sgdbclient.Execute<DataSearch>(request);
            return response.Data;
        }
        catch (Exception ex)
        {
            //throw;
            return null;
        }
    }
    public IEnumerable<ImgResult> GetHeroesForId(int gameId)
    {
        try
        {
            var request = new RestRequest($"heroes/game/{gameId}", Method.Get);
            var response = sgdbclient.Execute<SearchHeroesByIdResult>(request);
            return response.Data.data;
        }
        catch (Exception ex)
        {
            //throw;
            return null;
        }
    }
    public IEnumerable<ImgResult> GetLogoForId(int gameId)
    {
        try
        {
            var request = new RestRequest($"logos/game/{gameId}", Method.Get);
            var response = sgdbclient.Execute<SearchLogoByIdResult>(request);
            return response.Data.data;
        }
        catch (Exception ex)
        {
            //throw;
            return null;
        }
    }
    public IEnumerable<ImgResult> GetGridBoxartForId(int gameId)
    {
        try
        {
            var request = new RestRequest($"grids/game/{gameId}?dimensions=600x900,342x482,660x930", Method.Get);
            var response = sgdbclient.Execute<SearchGridByIdResult>(request);
            return response.Data.data;
        }
        catch (Exception ex)
        {
            //throw;
            return null;
        }
    }
    public IEnumerable<ImgResult> GetGridBannerForId(int gameId)
    {
        try
        {
            var request = new RestRequest($"grids/game/{gameId}?dimensions=460x215,920x430", Method.Get);
            var response = sgdbclient.Execute<SearchGridByIdResult>(request);
            return response.Data.data;
        }
        catch (Exception ex)
        {
            //throw;
            return null;
        }
    }
}
