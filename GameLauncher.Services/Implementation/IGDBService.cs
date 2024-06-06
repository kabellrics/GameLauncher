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

namespace GameLauncher.Services.Implementation;
public class IGDBService : IIGDBService
{
    private string apipath = @"https://api.igdb.com/v4";
    private string id;
    private string secret;
    private string Bearer;

    public IGDBService()
    {
        var filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher", "SSCP.gral");
        string[] lines = File.ReadAllLines(filepath);
        id = lines[2];
        secret = lines[3];
        GetBearer();
    }
    private void GetBearer()
    {
        try
        {
            var client = new RestClient();
            var request = new RestRequest("https://id.twitch.tv/oauth2/token", Method.Post);
            //request.Resource = "/token";
            request.AddParameter("client_id", id);
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
    public IEnumerable<SearchResult> GetGameByName(string name)
    {
        try
        {
            string urlrequest = "https://api.igdb.com/v4/games/?search=" + name + "&fields=id,name,version_title";
            var client = new RestClient();
            var request = new RestRequest(urlrequest, Method.Post);
            request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
            request.AddHeader("Authorization", $"Bearer {Bearer}");
            request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");
            var requesturi = client.BuildUri(request);

            var response = client.Execute<IEnumerable<SearchResult>>(request, Method.Post);
            return (IEnumerable<SearchResult>)response.Data;
        }
        catch (Exception ex)
        {
            //throw;
        }
        return null;
    }
    public IGDBGame GetDetailsGame(int id)
    {
        string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,cover.*,first_release_date,storyline,summary,version_title";
        var client = new RestClient();
        var request = new RestRequest(urlrequest, Method.Get);
        request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
        request.AddHeader("Authorization", $"Bearer {Bearer}");
        request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

        var response = client.Execute<IEnumerable<IGDBGame>>(request);
        return response.Data.FirstOrDefault();// .Content;
    }
    public IEnumerable<Artwork> GetArtworksByGameId(int id)
    {
        //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
        string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,artworks.*";
        var client = new RestClient();
        var request = new RestRequest(urlrequest, Method.Get);
        request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
        request.AddHeader("Authorization", $"Bearer {Bearer}");
        request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

        var response = client.Execute<IGDBGame>(request);
        var rawdata = response.Content.Substring(1, response.Content.Length - 2);

        JObject jsondata = JObject.Parse(rawdata);
        if (jsondata["artworks"] != null)
        {
            IList<JToken> results = jsondata["artworks"].Children().ToList();
            IList<Artwork> searchResults = new List<Artwork>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                Artwork searchResult = result.ToObject<Artwork>();
                searchResults.Add(searchResult);
            }
            return searchResults;
        }
        else
            return null;
    }
    public IEnumerable<Genre> GetGenresByGameId(int id)
    {
        //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
        string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,genres.*";
        var client = new RestClient();
        var request = new RestRequest(urlrequest, Method.Get);
        request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
        request.AddHeader("Authorization", $"Bearer {Bearer}");
        request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

        var response = client.Execute<IGDBGame>(request);
        var rawdata = response.Content.Substring(1, response.Content.Length - 2);

        JObject jsondata = JObject.Parse(rawdata);
        if (jsondata["genres"] != null)
        {
            IList<JToken> results = jsondata["genres"].Children().ToList();
            IList<Genre> searchResults = new List<Genre>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                Genre searchResult = result.ToObject<Genre>();
                searchResults.Add(searchResult);
            }
            return searchResults;
        }
        else
            return null;
    }
    public IEnumerable<Video> GetVideosByGameId(int id)
    {
        //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
        string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,videos.*";
        var client = new RestClient();
        var request = new RestRequest(urlrequest, Method.Get);
        request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
        request.AddHeader("Authorization", $"Bearer {Bearer}");
        request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

        var response = client.Execute<IGDBGame>(request);
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
    public IEnumerable<InvolvedCompany> GetInvolvedCompanyByGameId(int id)
    {
        //string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,name,artworks.*,cover.*,first_release_date,genres.*,screenshots.*,storyline,summary,version_title,videos.*,themes.*";
        string urlrequest = "https://api.igdb.com/v4/games/" + id.ToString() + "?fields=id,involved_companies.*";
        var client = new RestClient();
        var request = new RestRequest(urlrequest, Method.Get);
        request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
        request.AddHeader("Authorization", $"Bearer {Bearer}");
        request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");

        var response = client.Execute<IGDBGame>(request);
        var rawdata = response.Content.Substring(1, response.Content.Length - 2);

        JObject jsondata = JObject.Parse(rawdata);
        if (jsondata["involved_companies"] != null)
        {
            IList<JToken> results = jsondata["involved_companies"].Children().ToList();
            IList<InvolvedCompany> searchResults = new List<InvolvedCompany>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                InvolvedCompany searchResult = result.ToObject<InvolvedCompany>();
                searchResults.Add(searchResult);
            }
            return searchResults;
        }
        else
            return null;
    }
    public IEnumerable<Company> GetDevByGameId(IEnumerable<InvolvedCompany> involvedComps)
    {
        if (involvedComps != null)
        {
            var devlist = involvedComps.Where(x => x.developer == true || x.supporting == true);
            IList<Company> searchResults = new List<Company>();
            foreach (var dev in devlist)
            {
                string urlrequest = "https://api.igdb.com/v4/companies/" + dev.company.ToString() + "?fields=change_date,change_date_category,changed_company_id,checksum,country,created_at,description,developed,logo,name,parent,published,slug,start_date,start_date_category,updated_at,url,websites;";
                //var client = new RestClient("https://api.igdb.com/v4/companies/1514?fields=change_date,change_date_category,changed_company_id,checksum,country,created_at,description,developed,logo,name,parent,published,slug,start_date,start_date_category,updated_at,url,websites;");
                var client = new RestClient();
                var request = new RestRequest(urlrequest, Method.Get);
                request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
                request.AddHeader("Authorization", $"Bearer {Bearer}");
                request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");
                var response = client.Execute<Company>(request);
                var rawdata = response.Content.Substring(1, response.Content.Length - 2);
                Company comp = JsonConvert.DeserializeObject<Company>(rawdata);
                searchResults.Add(comp);
            }
            return searchResults;
        }
        else
            return null;
    }
    public IEnumerable<Company> GetPublishersByGameId(IEnumerable<InvolvedCompany> involvedComps)
    {
        var devlist = involvedComps.Where(x => x.publisher == true);
        IList<Company> searchResults = new List<Company>();
        foreach (var dev in devlist)
        {
            try
            {
                string urlrequest = "https://api.igdb.com/v4/companies/" + dev.company.ToString() + "?fields=change_date,change_date_category,changed_company_id,checksum,country,created_at,description,developed,logo,name,parent,published,slug,start_date,start_date_category,updated_at,url,websites;";
                //var client = new RestClient("https://api.igdb.com/v4/companies/1514?fields=change_date,change_date_category,changed_company_id,checksum,country,created_at,description,developed,logo,name,parent,published,slug,start_date,start_date_category,updated_at,url,websites;");
                var client = new RestClient();
                var request = new RestRequest(urlrequest, Method.Get);
                request.AddHeader("Client-ID", "fah6fktmuph3zpfelt66hoqk4zn62i");
                request.AddHeader("Authorization", $"Bearer {Bearer}");
                request.AddHeader("Cookie", "__cfduid=d35aefabc266be82fd2fd3d888d853e571611495025");
                var response = client.Execute<Company>(request);
                var rawdata = response.Content.Substring(1, response.Content.Length - 2);
                Company comp = JsonConvert.DeserializeObject<Company>(rawdata);
                searchResults.Add(comp);
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        return searchResults;
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
