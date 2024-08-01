using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Models.EpicGame;
using GameLauncher.Models.IGDB;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.Connector;
public class CollectionConnector
{
    private readonly RestClient _client;

    public CollectionConnector(string baseUrl)
    {
        _client = new RestClient(baseUrl);
    }
    public async Task<IEnumerable<Collection>> GetCollectionsAsync()
    {
        var request = new RestRequest("/api/Collection", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Items: " + response.Content);
            return JsonConvert.DeserializeObject<IEnumerable<Collection>>(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return new List<Collection>();
        }
    }
    public async Task<IEnumerable<String>> GetPredefineCollection()
    {
        var request = new RestRequest("/api/Collection/GetPredefineCollection", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Items: " + response.Content);
            return JsonConvert.DeserializeObject<IEnumerable<String>>(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return new List<String>();
        }
    }
    public async Task<DefaultCollectionMessage> GetDefaultCollectionStatus()
    {
        var request = new RestRequest("/api/Collection/GetDefaultCollectionStatus", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<DefaultCollectionMessage>(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return new DefaultCollectionMessage();
        }
    }
    public async Task<DefaultCollectionMessage> CreateDefaultCollection(DefaultCollectionMessage collectionMessage)
    {
        var request = new RestRequest("/api/Collection/CreateDefaultCollection", Method.Post);
        request.AddJsonBody(collectionMessage);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<DefaultCollectionMessage>(response.Content);
        }
        else
        {
            Console.WriteLine("Error: " + response.ErrorMessage);
            return new DefaultCollectionMessage();
        }
    }
    public async Task CreateCollectionFromPlateforme()
    {
        var request = new RestRequest($"api/Collection/CreateCollectionFromPlateforme", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            throw new Exception(response.Content);
        }
    }
    public async Task<IEnumerable<Models.Item>> GetAllItemInside(Guid id)
    {
        var request = new RestRequest($"api/Collection/GetAllItemInside/{id}", Method.Get);
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Models.Item>>(response.Content);
        }
        else
        {
            return new List<Models.Item>();
        }
    }
    public async IAsyncEnumerable<ItemInCollection> GetAllItemInsideStream(Guid id)
    {
        #region OldVersion
        //var request = new RestRequest($"api/Collection/GetAllItemInside/{id}", Method.Get);
        //var response = await _client.ExecuteAsync<List<ItemInCollection>>(request);
        //if (!response.IsSuccessful)
        //{
        //    throw new Exception($"Error retrieving items: {response.ErrorMessage}");
        //}

        //var channel = Channel.CreateUnbounded<ItemInCollection>();
        //_ = Task.Run(async () =>
        //{
        //    foreach (var item in response.Data)
        //    {
        //        await channel.Writer.WriteAsync(item);
        //    }
        //    channel.Writer.Complete();
        //});

        //// Return an IAsyncEnumerable from the channel reader
        //await foreach (var item in channel.Reader.ReadAllAsync())
        //{
        //    yield return item;
        //}            //var request = new RestRequest("/api/Items/Stream", Method.Get); 
        #endregion
        var response = _client.StreamJsonAsync<ItemInCollection>($"api/Collection/GetAllItemInsideStream/{id}", CancellationToken.None);
        await foreach (var item in response)
        {
            yield return item;
        }
    }
    public async Task AddToCollectionEnd(Guid id, Guid gameid)
    {
        var request = new RestRequest($"/api/Collection/AddToCollectionEnd/{id}/{gameid}", Method.Get);
        var response = await _client.ExecuteAsync(request);
    }
    public async Task UpdateCollectionItemOrder(Guid id, Guid gameid, int newOrder)
    {
        var request = new RestRequest($"/api/Collection/UpdateCollectionItemOrder/{id}/{gameid}/{newOrder}", Method.Get);
        var response = await _client.ExecuteAsync(request);
    }
    public async Task DeleteCollectionItem(Guid id)
    {
        var request = new RestRequest($"/api/Collection/CollectionItem/{id}", Method.Delete);
        var response = await _client.ExecuteAsync(request);
    }
    public async Task DeleteCollection(Guid id)
    {
        var request = new RestRequest($"/api/Collection/{id}", Method.Delete);
        var response = await _client.ExecuteAsync(request);
    }
    public async Task UpdateCollection(Collection item)
    {
        var request = new RestRequest($"/api/Collection/{item.ID}", Method.Put);
        request.AddParameter("id", item.ID, ParameterType.UrlSegment);
        request.AddJsonBody(item);

        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception("Update Failed");
        }
    }
    public async Task CreateCollection(Collection item)
    {
        var request = new RestRequest($"/api/Collection", Method.Post);
        request.AddJsonBody(item);
        var response = await _client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception("Creation Failed");
        }
    }
}
