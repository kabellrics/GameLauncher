using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.Connector;
public class VideoIntroConnector
{
    private readonly RestClient _client;

    public VideoIntroConnector(string baseUrl)
    {
        _client = new RestClient(baseUrl);
    }
    public async Task<IEnumerable<IntroVideo>> GetIntroVideosAsync()
    {
        var request = new RestRequest("/api/IntroVideo", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Items: " + response.Content);
            return JsonConvert.DeserializeObject<IEnumerable<IntroVideo>>(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return new List<IntroVideo>();
        }
    }
    public async Task UpdateIntroVideo(IntroVideo item)
    {
        var request = new RestRequest($"/api/IntroVideo/{item.ID}", Method.Put);
        request.AddParameter("id", item.ID, ParameterType.UrlSegment);
        request.AddJsonBody(item);

        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception("Update Failed");
        }
    }
    public async Task DeleteIntroVideo(Guid id)
    {
        var request = new RestRequest($"/api/IntroVideo/{id}", Method.Delete);
        var response = await _client.ExecuteAsync(request);
    }
    public async Task CreateIntroVideo(FileRequest item)
    {
        var request = new RestRequest($"/api/IntroVideo", Method.Post);
        request.AddJsonBody(item);
        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception("Creation Failed");
        }
    }
}
