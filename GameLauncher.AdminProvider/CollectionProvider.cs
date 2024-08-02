using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Connector;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.ObservableObjet;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.AdminProvider;
public class CollectionProvider : ICollectionProvider
{
    private readonly CollectionConnector colectionconnector;
    public CollectionProvider()
    {
        colectionconnector = new CollectionConnector("https://localhost:7197");
    }
    public async Task<DefaultCollectionMessage> GetDefaultCollectionStatus()
    {
        return await colectionconnector.GetDefaultCollectionStatus();
    }
    public async Task<DefaultCollectionMessage> CreateDefaultCollection(DefaultCollectionMessage collectionMessage)
    {
        return await colectionconnector.CreateDefaultCollection(collectionMessage);
    }
    public async Task<IEnumerable<String>> GetPredefineCollection()
    {
        return await colectionconnector.GetPredefineCollection();
    }
    public async Task<IEnumerable<FullCollectionItem>> GetFullCollection()
    {
        return await colectionconnector.GetFullCollection();
    }
    public async Task<IEnumerable<ObsCollection>> GetCollectionsAsync()
    {
        var collecs = await colectionconnector.GetCollectionsAsync();
        var obscollecs = new List<ObsCollection>();
        foreach (var colle in collecs.OrderBy(x=>x.Order)) { obscollecs.Add(new ObsCollection(colle)); }
        return obscollecs;
    }
    public async IAsyncEnumerable<ObsCollection> GetCollectionsAsyncEnumerable()
    {
        var collecs = await colectionconnector.GetCollectionsAsync();
        foreach (var colle in collecs.OrderBy(x=>x.Order))
        {
            var obsitem = new ObsCollection(colle);
            yield return obsitem;
        }
    }
    public async Task<IEnumerable<Item>> GetAllItemInside(Guid id)
    {
        return await colectionconnector.GetAllItemInside(id);
    }
    public async IAsyncEnumerable<ObservableItem> GetAllItemInsideAsync(Guid id)
    {
        var items = await colectionconnector.GetAllItemInside(id);
        foreach (var item in items)
            yield return new ObservableItem(item);
    }
    public async IAsyncEnumerable<ObservableItemInCollection> GetAllItemInsideAsyncStream(Guid id)
    {
        await foreach (var item in colectionconnector.GetAllItemInsideStream(id))
            yield return new ObservableItemInCollection(item.Item,item.CollectionItem);
    }
    public async Task CreateCollectionFromPlateforme()
    {
        await colectionconnector.CreateCollectionFromPlateforme();
    }
    public async Task AddToCollectionEnd(Guid id, Guid gameid)
    {
        await colectionconnector.AddToCollectionEnd(id, gameid);
    }
    public async Task UpdateCollectionItemOrder(Guid id, Guid gameid, int newOrder)
    {
        await colectionconnector.UpdateCollectionItemOrder(id, gameid, newOrder);
    }
    public async Task UpdateCollection(ObsCollection item)
    {
        await colectionconnector.UpdateCollection(item._collection);
    }
    public async Task DeleteCollectionItem(Guid id)
    {
        await colectionconnector.DeleteCollectionItem(id);
    }
    public async Task DeleteCollection(Guid id)
    {
        await colectionconnector.DeleteCollection(id);
    }
    public async Task CreateCollection(Collection item)
    {
        await colectionconnector.CreateCollection(item);
    }
}
