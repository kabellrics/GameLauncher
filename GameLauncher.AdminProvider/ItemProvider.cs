﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Connector;
using GameLauncher.Models;
using GameLauncher.ObservableObjet;

namespace GameLauncher.AdminProvider
{
    public class ItemProvider : IItemProvider
    {
        private readonly GameLauncherClient apiconnector;
        private readonly LookupConnector lookconnector;
        public ItemProvider()
        {
            apiconnector = new GameLauncherClient("https://localhost:7197");
            lookconnector = new LookupConnector("https://localhost:7197");
        }
        public async IAsyncEnumerable<ObservableItem> GetAllItemsStream()
        {
            await foreach (var item in apiconnector.GetItemsStreamAsync())
            {
                var obsItem = new ObservableItem(item);
                obsItem.Platforme = await lookconnector.GetPlateformebycodename(item.LUPlatformesId);
                var devs = await apiconnector.GetDevsByItemAsync(item.ID);
                foreach (var dev in devs.OrderBy(x => x.Name))
                    obsItem.Develloppeurs.Add(new ObservableDevelloppeur(dev));
                var edits = await apiconnector.GetEditeursByItemAsync(item.ID);
                foreach (var edit in edits.OrderBy(x => x.Name))
                    obsItem.Editeurs.Add(new ObservableEditeur(edit));
                var genres = await apiconnector.GetGenresByItemAsync(item.ID);
                foreach (var genre in genres.OrderBy(x => x.Name))
                    obsItem.Genres.Add(new ObservableGenre(genre));
                yield return obsItem;
            }
        }
        public async IAsyncEnumerable<ObservableItem> GetAllItemsAsyncEnumerable()
        {
            var items = await apiconnector.GetItemsAsync();
            foreach (var item in items.OrderBy(x=>x.LUPlatformesId).ThenBy(x=>x.Name))
            {
                var obsItem = new ObservableItem(item);
                var devs = await apiconnector.GetDevsByItemAsync(item.ID);
                foreach (var dev in devs.OrderBy(x => x.Name))
                    obsItem.Develloppeurs.Add(new ObservableDevelloppeur(dev));
                var edits = await apiconnector.GetEditeursByItemAsync(item.ID);
                foreach (var edit in edits.OrderBy(x => x.Name))
                    obsItem.Editeurs.Add(new ObservableEditeur(edit));
                var genres = await apiconnector.GetGenresByItemAsync(item.ID);
                foreach (var genre in genres.OrderBy(x => x.Name))
                    obsItem.Genres.Add(new ObservableGenre(genre));
                yield return obsItem;
            }
        }
        public async Task<IEnumerable<ObservableItem>> GetAllItemsAsync()
        {
            var obsitems = new List<ObservableItem>();
            var items = await apiconnector.GetItemsAsync();
            foreach (var item in items.OrderBy(x => x.LUPlatformesId).ThenBy(x => x.Name))
            {
                var obsItem = new ObservableItem(item);
                var devs = await apiconnector.GetDevsByItemAsync(item.ID);
                foreach (var dev in devs.OrderBy(x => x.Name))
                    obsItem.Develloppeurs.Add(new ObservableDevelloppeur(dev));
                var edits = await apiconnector.GetEditeursByItemAsync(item.ID);
                foreach (var edit in edits.OrderBy(x => x.Name))
                    obsItem.Editeurs.Add(new ObservableEditeur(edit));
                var genres = await apiconnector.GetGenresByItemAsync(item.ID);
                foreach (var genre in genres.OrderBy(x => x.Name))
                    obsItem.Genres.Add(new ObservableGenre(genre));
                obsitems.Add(obsItem);
            }
            return obsitems;
        }
        public async Task<IEnumerable<ObservableGroupItem>> GetAllItemsGrouped()
        {
            var items = await apiconnector.GetItemsAsync();
            var obsitems = new List<ObservableItem>();
            foreach (var item in items.OrderBy(x => x.LUPlatformesId).ThenBy(x => x.Name))
            {
                var obsItem = new ObservableItem(item);
                var devs = await apiconnector.GetDevsByItemAsync(item.ID);
                foreach (var dev in devs.OrderBy(x => x.Name))
                    obsItem.Develloppeurs.Add(new ObservableDevelloppeur(dev));
                var edits = await apiconnector.GetEditeursByItemAsync(item.ID);
                foreach (var edit in edits.OrderBy(x => x.Name))
                    obsItem.Editeurs.Add(new ObservableEditeur(edit));
                var genres = await apiconnector.GetGenresByItemAsync(item.ID);
                foreach (var genre in genres.OrderBy(x => x.Name))
                    obsItem.Genres.Add(new ObservableGenre(genre));
                obsitems.Add(obsItem);
            }
            return obsitems.GroupBy(x => x.Platforme.Name)
                .Select(x => new ObservableGroupItem { GroupName = x.Key, Items = new ObservableCollection<ObservableItem>(x.ToList()) }).OrderBy(x=>x.GroupName);
        }
        public async Task UpdateItem(ObservableItem item)
        {
            await apiconnector.UpdateItemAsync(item.Item);
        }
        public async Task UpdatesGenresForItem(Item item, List<Genre> newGenres)
        {
            await apiconnector.UpdateGenreForItem(item,newGenres);
        }
        public async Task UpdatesDevsForItem(Item item, List<Develloppeur> newDevs)
        {
            await apiconnector.UpdateDevForItem(item, newDevs);
        }
        public async Task UpdatesEditsForItem(Item item, List<Editeur> newEdits)
        {
            await apiconnector.UpdateEditForItem(item, newEdits);
        }
    }
}
