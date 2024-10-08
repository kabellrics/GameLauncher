﻿using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.ObservableObjet;

namespace GameLauncher.AdminProvider.Interface;
public interface ICollectionProvider
{
    Task AddToCollectionEnd(Guid id, Guid gameid);
    Task<IEnumerable<Item>> GetAllItemInside(Guid id);
    Task<IEnumerable<String>> GetPredefineCollection();
    IAsyncEnumerable<ObservableItemInCollection> GetAllItemInsideAsyncStream(Guid id);
    Task<IEnumerable<ObsCollection>> GetCollectionsAsync();
    IAsyncEnumerable<ObsCollection> GetCollectionsAsyncEnumerable();
    Task UpdateCollection(ObsCollection item);
    Task UpdateCollectionItemOrder(Guid id, Guid gameid, int newOrder);
    Task CreateCollectionFromPlateforme();
    IAsyncEnumerable<ObservableItem> GetAllItemInsideAsync(Guid id);
    Task DeleteCollectionItem(Guid id);
    Task CreateCollection(Collection item);
    Task DeleteCollection(Guid id);
    Task<DefaultCollectionMessage> GetDefaultCollectionStatus();
    Task<DefaultCollectionMessage> CreateDefaultCollection(DefaultCollectionMessage collectionMessage);
    //Task<IEnumerable<FullCollectionItem>> GetFullCollection();


}