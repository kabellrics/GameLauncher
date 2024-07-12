using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Models;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.Connector;
public class LookupConnector
{
    private readonly RestClient _client;

    public LookupConnector(string baseUrl)
    {
        _client = new RestClient(baseUrl);
    }
    public async Task<IEnumerable<Develloppeur>> GetDevsAsync()
    {
        var request = new RestRequest("/api/Dev", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Items: " + response.Content);
            return JsonConvert.DeserializeObject<IEnumerable<Develloppeur>>(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return new List<Develloppeur>();
        }
    }
    public async Task<IEnumerable<Editeur>> GetEditeursAsync()
    {
        var request = new RestRequest("/api/Editeurs", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Items: " + response.Content);
            return JsonConvert.DeserializeObject<IEnumerable<Editeur>>(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return new List<Editeur>();
        }
    }
    public async Task<IEnumerable<Genre>> GetGenresAsync()
    {
        var request = new RestRequest("/api/Genres", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Items: " + response.Content);
            return JsonConvert.DeserializeObject<IEnumerable<Genre>>(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return new List<Genre>();
        }
    }
    public async Task<IEnumerable<LUPlatformes>> GetPlatformesAsync()
    {
        var request = new RestRequest("/api/Platforme", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Items: " + response.Content);
            return JsonConvert.DeserializeObject<IEnumerable<LUPlatformes>>(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return new List<LUPlatformes>();
        }
    }
    public async Task<bool> FusionDev(Guid idToDelete, Guid idToKeep)
    {
        var request = new RestRequest($"/api/Dev/Fusion/{idToDelete}/{idToKeep}", Method.Get);
        var response = await _client.ExecuteAsync(request);
        return response.IsSuccessful;
    }
    public async Task<LUPlatformes> GetPlateformebycodename(string codename)
    {
        var request = new RestRequest($"/api/Plateforme/{codename}", Method.Get);
        var response = await _client.ExecuteAsync<LUPlatformes>(request);
        if (response.IsSuccessful)
        {
            Console.WriteLine("Plateforme: " + response.Content);
            //return JsonConvert.DeserializeObject<LUPlatformes>(response.Content);
            return response.Data;
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return null;
        }
    }
    public async Task<bool> FusionEditeur(Guid idToDelete, Guid idToKeep)
    {
        var request = new RestRequest($"/api/Editeurs/Fusion/{idToDelete}/{idToKeep}", Method.Get);
        var response = await _client.ExecuteAsync(request);
        return response.IsSuccessful;
    }
    public async Task<bool> FusionGenre(Guid idToDelete, Guid idToKeep)
    {
        var request = new RestRequest($"/api/Genres/Fusion/{idToDelete}/{idToKeep}", Method.Get);
        var response = await _client.ExecuteAsync(request);
        return response.IsSuccessful;
    }
    public async Task UpdateDevAsync(Develloppeur item)
    {
        var request = new RestRequest($"/api/Dev/{item.ID}", Method.Put);
        request.AddParameter("id", item.ID, ParameterType.UrlSegment);
        request.AddJsonBody(item);

        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception("Update Failed");
        }
    }
    public async Task UpdateEditeurAsync(Editeur item)
    {
        var request = new RestRequest($"/api/Editeurs/{item.ID}", Method.Put);
        request.AddParameter("id", item.ID, ParameterType.UrlSegment);
        request.AddJsonBody(item);

        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception("Update Failed");
        }
    }
    public async Task UpdateGenreAsync(Genre item)
    {
        var request = new RestRequest($"/api/Genres/{item.ID}", Method.Put);
        request.AddParameter("id", item.ID, ParameterType.UrlSegment);
        request.AddJsonBody(item);

        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception("Update Failed");
        }
    }
}
