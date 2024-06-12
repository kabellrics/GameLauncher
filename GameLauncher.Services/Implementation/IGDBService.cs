using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using GameLauncher.Models.IGDB;
using GameLauncher.Services.Interface;
using System.Xml.Linq;

namespace GameLauncher.Services.Implementation;
public class IGDBService : IIGDBService
{
    private string apipath = @"https://api.igdb.com/v4";
    private string clientid;
    private string secret;
    private string Bearer;

    public IGDBService()
    {
        var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "SSCP.gral");
        string[] lines = File.ReadAllLines(filepath);
        clientid = lines[5];
        secret = lines[6];
        GetBearer();
    }
    private void GetBearer()
    {
        try
        {
            var client = new RestClient();
            var request = new RestRequest("https://id.twitch.tv/oauth2/token", Method.Post);
            //request.Resource = "/token";
            request.AddParameter("client_id", clientid);
            request.AddParameter("client_secret", secret);
            request.AddParameter("grant_type", "client_credentials");

            var response = client.Execute<AccessResponse>(request);
            var responseData = response.Data;
            Bearer = responseData.access_token;
        }
        catch (Exception ex)
        {
            //throw;
        }
    }
    public IEnumerable<Company> GetCompaniesDetail(IEnumerable<string> involvedComps)
    {
        try
        {
            string urlrequest = "https://api.igdb.com/v4/companies"; ;
            var client = new RestClient();
            var request = new RestRequest(urlrequest, Method.Post);
            request.AddHeader("Client-ID", clientid);
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Accept", "application/json");
            request.AddBody($"fields name; where id = ({string.Join(',',involvedComps)});");
            var requesturi = client.BuildUri(request);

            var response = client.Execute<IEnumerable<Company>>(request, Method.Post);
            return (IEnumerable<Company>)response.Data;
        }
        catch (Exception ex)
        {
            //throw;
        }
        return null;
    }
    public IEnumerable<IGDBGame> GetGameByName(string name)
    {
        try
        {
            //string urlrequest = "https://api.igdb.com/v4/games/?search=" + name + "&fields=id,name,version_title";
            string urlrequest = "https://api.igdb.com/v4/games";
            var client = new RestClient();
            var request = new RestRequest(urlrequest, Method.Post);
            request.AddHeader("Client-ID", clientid);
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Accept", "application/json");
            request.AddBody("fields id,name,cover.*,artworks.*,involved_companies.*,first_release_date,genres.*,storyline,summary,version_title; search \"" + name + "\";");
            var requesturi = client.BuildUri(request);

            var response = client.Execute<IEnumerable<IGDBGame>>(request, Method.Post);
            return (IEnumerable<IGDBGame>)response.Data;
        }
        catch (Exception ex)
        {
            //throw;
        }
        return null;
    }
    public IGDBGame GetDetailsGame(int id)
    {
        //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,cover.*,artwork.*,involved_companies.*,first_release_date,genres.*,storyline,summary,version_title";
        string urlrequest = "https://api.igdb.com/v4/games";
        var client = new RestClient();
        var request = new RestRequest(urlrequest, Method.Post);
        request.AddHeader("Client-ID", clientid);
        request.AddHeader("Authorization", $"Bearer {Bearer}");
        request.AddHeader("Accept", "application/json");
        request.AddBody("fields id,name,cover.*,artworks.*,involved_companies.*,first_release_date,genres.*,storyline,summary,version_title; where id= " + id.ToString()+";");
        var requesturi = client.BuildUri(request);

        var response = client.Execute<IEnumerable<IGDBGame>>(request, Method.Post);
        var game = response.Data.FirstOrDefault();// .Content;
        game.cover.url = game.cover.url.Replace("t_thumb", "t_1080p");
        if(game.artworks !=null)
        game.artworks.ForEach(x=> x.url = x.url.Replace("t_thumb", "t_1080p"));
        return game;
    }
    
    public IEnumerable<Video> GetVideosByGameId(int id)
    {
        //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
        string urlrequest = "https://api.igdb.com/v4/games";
        var client = new RestClient();
        var request = new RestRequest(urlrequest, Method.Post);
        request.AddHeader("Client-ID", clientid);
        request.AddHeader("Authorization", $"Bearer {Bearer}");
        request.AddHeader("Accept", "application/json");
        request.AddBody("fields id,videos.*; where id =" + id.ToString() + ";");

        var response = client.Execute<IGDBGame>(request, Method.Post);
        var rawdata = response.Content.Substring(1, response.Content.Length - 2);

        JObject jsondata = JObject.Parse(rawdata);
        if (jsondata["videos"] != null)
        {
            IList<JToken> results = jsondata["videos"].Children().ToList();
            IList<Video> searchResults = new List<Video>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                Video searchResult = result.ToObject<Video>();
                searchResults.Add(searchResult);
            }
            return searchResults;
        }
        else
            return null;
    }
    
    public string GetCoverLink(string hash)
    {
        return $"https://images.igdb.com/igdb/image/upload/t_cover_big/" + hash + ".jpg";
    }
    public string GetArtWorkLink(string hash)
    {
        return $"https://images.igdb.com/igdb/image/upload/t_1080p/" + hash + ".jpg";
    }
}
