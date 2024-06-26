﻿using GameLauncher.Models;
using GameLauncher.Models.APIObject;

namespace GameLauncher.Services.Interface;
public interface ICollectionService
{
    void AddToCollectionEnd(Guid id, Guid gameid);
    IEnumerable<Collection> GetAll();
    IAsyncEnumerable<ItemInCollection> GetAllItemInside(Guid id);
    void Update(Collection updatedcollection);
    void UpdateCollectionItemOrder(Guid id, Guid gameid, int newOrder);
    void CreateCollectionFromPlateforme();
}