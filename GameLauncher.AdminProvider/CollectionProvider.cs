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
using GameLauncher.Services.Interface;
using Newtonsoft.Json;
using RestSharp;

namespace GameLauncher.AdminProvider;
public class CollectionProvider : ICollectionProvider
{
    //private readonly CollectionConnector colectionconnector;
    private readonly ICollectionService colectionconnector;
    public CollectionProvider(ICollectionService connector)
    {
        colectionconnector = connector;
        //colectionconnector = new CollectionConnector("https://localhost:7197");
    }
    public async Task<DefaultCollectionMessage> GetDefaultCollectionStatus()
    {
        return colectionconnector.GetDefaultCollectionStatus();
    }
    public async Task<DefaultCollectionMessage> CreateDefaultCollection(DefaultCollectionMessage collectionMessage)
    {
        return colectionconnector.CreateDefaultColection(collectionMessage);
    }
    public async Task<IEnumerable<String>> GetPredefineCollection()
    {
        return colectionconnector.GetPredefineCollection();
    }
    //public async Task<IEnumerable<FullCollectionItem>> GetFullCollection()
    //{
    //    return await colectionconnector.GetAllFull();
    //}
    public async Task<IEnumerable<ObsCollection>> GetCollectionsAsync()
    {
        var collecs = colectionconnector.GetAll();
        var obscollecs = new List<ObsCollection>();
        foreach (var colle in collecs.OrderBy(x=>x.Order)) { obscollecs.Add(new ObsCollection(colle)); }
        return obscollecs;
    }
    public async IAsyncEnumerable<ObsCollection> GetCollectionsAsyncEnumerable()
    {
        var collecs = colectionconnector.GetAll();
        foreach (var colle in collecs.OrderBy(x=>x.Order))
        {
            var obsitem = new ObsCollection(colle);
            yield return obsitem;
        }
    }
    public async Task<IEnumerable<Item>> GetAllItemInside(Guid id)
    {
        return null;// colectionconnector.GetAllItemInside(id);
    }
    public async IAsyncEnumerable<ObservableItem> GetAllItemInsideAsync(Guid id)
    {
        var items = colectionconnector.GetAllItemInside(id);
        await foreach (var item in items)
            yield return new ObservableItem(item.Item);
    }
    public async IAsyncEnumerable<ObservableItemInCollection> GetAllItemInsideAsyncStream(Guid id)
    {
        await foreach (var item in colectionconnector.GetAllItemInside(id))
            yield return new ObservableItemInCollection(item.Item,item.CollectionItem);
    }
    public async Task CreateCollectionFromPlateforme()
    {
        colectionconnector.CreateCollectionFromPlateforme();
    }
    public async Task AddToCollectionEnd(Guid id, Guid gameid)
    {
        colectionconnector.AddToCollectionEnd(id, gameid);
    }
    public async Task UpdateCollectionItemOrder(Guid id, Guid gameid, int newOrder)
    {
        colectionconnector.UpsertCollectionItem(id, gameid, newOrder);
    }
    public async Task UpdateCollection(ObsCollection item)
    {
        colectionconnector.Update(item._collection);
    }
    public async Task DeleteCollectionItem(Guid id)
    {
        colectionconnector.DelteCollectionItem(id);
    }
    public async Task DeleteCollection(Guid id)
    {
        colectionconnector.DelteCollection(id);
    }
    public async Task CreateCollection(Collection item)
    {
        colectionconnector.CreateCollection(item);
    }
}
