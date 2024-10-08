﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GameLauncher.AdminProvider
{
    public class ItemProvider : IItemProvider
    {
        //private readonly GameLauncherClient apiconnector;
        private readonly IItemsService apiconnector;
        private readonly IStatService statsService;
        private readonly IGenreService genreService;
        private readonly IDevService devService;
        private readonly IEditeurService editService;
        private readonly IPlateformeService plateformeService;
        //private readonly LookupConnector lookconnector;
        public ItemProvider(IItemsService api, IStatService stats, IGenreService genre, IDevService dev, IEditeurService edit, IPlateformeService plateforme)
        {
            apiconnector = api;
            statsService = stats;
            genreService = genre;
            devService = dev;
            editService = edit;
            plateformeService = plateforme;
            //apiconnector = new GameLauncherClient("https://localhost:7197");
            //lookconnector = new LookupConnector("https://localhost:7197");
        }
        public async Task<StatsObject> GetStatsAsync()
        {
            return statsService.GetStatistiques();
        }
        public async IAsyncEnumerable<ObservableItem> GetAllItemsStream()
        {
            await foreach (var item in apiconnector.GetAllAsync())
            {
                var obsItem = new ObservableItem(item);
                obsItem.Platforme = plateformeService.Get(item.LUPlatformesId);
                var devs = devService.GetAllForItem(item.ID);
                foreach (var dev in devs.OrderBy(x => x.Name))
                    obsItem.Develloppeurs.Add(new ObservableDevelloppeur(dev));
                var edits = editService.GetAllForItem(item.ID);
                foreach (var edit in edits.OrderBy(x => x.Name))
                    obsItem.Editeurs.Add(new ObservableEditeur(edit));
                var genres = genreService.GetAllForItem(item.ID);
                foreach (var genre in genres.OrderBy(x => x.Name))
                    obsItem.Genres.Add(new ObservableGenre(genre));
                yield return obsItem;
            }
        }
        public async IAsyncEnumerable<ObservableItem> GetAllItemsAsyncEnumerable()
        {
            var items = apiconnector.GetAll();
            foreach (var item in items.OrderBy(x=>x.LUPlatformesId).ThenBy(x=>x.Name))
            {
                var obsItem = new ObservableItem(item);
                var devs = devService.GetAllForItem(item.ID);
                foreach (var dev in devs.OrderBy(x => x.Name))
                    obsItem.Develloppeurs.Add(new ObservableDevelloppeur(dev));
                var edits = editService.GetAllForItem(item.ID);
                foreach (var edit in edits.OrderBy(x => x.Name))
                    obsItem.Editeurs.Add(new ObservableEditeur(edit));
                var genres = genreService.GetAllForItem(item.ID);
                foreach (var genre in genres.OrderBy(x => x.Name))
                    obsItem.Genres.Add(new ObservableGenre(genre));
                yield return obsItem;
            }
        }
        public async Task<IEnumerable<ObservableItem>> GetAllItemsAsync()
        {
            var obsitems = new List<ObservableItem>();
            var items = apiconnector.GetAll();
            foreach (var item in items.OrderBy(x => x.LUPlatformesId).ThenBy(x => x.Name))
            {
                var obsItem = new ObservableItem(item);
                var devs = devService.GetAllForItem(item.ID);
                foreach (var dev in devs.OrderBy(x => x.Name))
                    obsItem.Develloppeurs.Add(new ObservableDevelloppeur(dev));
                var edits = editService.GetAllForItem(item.ID);
                foreach (var edit in edits.OrderBy(x => x.Name))
                    obsItem.Editeurs.Add(new ObservableEditeur(edit));
                var genres = genreService.GetAllForItem(item.ID);
                foreach (var genre in genres.OrderBy(x => x.Name))
                    obsItem.Genres.Add(new ObservableGenre(genre));
                obsitems.Add(obsItem);
            }
            return obsitems;
        }
        public async Task<IEnumerable<ObservableGroupItem>> GetAllItemsGrouped()
        {
            var items = apiconnector.GetAll();
            var obsitems = new List<ObservableItem>();
            foreach (var item in items.OrderBy(x => x.LUPlatformesId).ThenBy(x => x.Name))
            {
                var obsItem = new ObservableItem(item);
                var devs = devService.GetAllForItem(item.ID);
                foreach (var dev in devs.OrderBy(x => x.Name))
                    obsItem.Develloppeurs.Add(new ObservableDevelloppeur(dev));
                var edits = editService.GetAllForItem(item.ID);
                foreach (var edit in edits.OrderBy(x => x.Name))
                    obsItem.Editeurs.Add(new ObservableEditeur(edit));
                var genres = genreService.GetAllForItem(item.ID);
                foreach (var genre in genres.OrderBy(x => x.Name))
                    obsItem.Genres.Add(new ObservableGenre(genre));
                obsitems.Add(obsItem);
            }
            return obsitems.GroupBy(x => x.Platforme.Name)
                .Select(x => new ObservableGroupItem { GroupName = x.Key, Items = new ObservableCollection<ObservableItem>(x.ToList()) }).OrderBy(x=>x.GroupName);
        }
        public async Task UpdateItem(ObservableItem item)
        {
            apiconnector.UpdateItem(item.Item);
        }
        public async Task UpdatesGenresForItem(Item item, List<Genre> newGenres)
        {
            genreService.UpdateGenreInItem(item, newGenres);
            //await apiconnector.UpdateGenreForItem(item,newGenres);
        }
        public async Task UpdatesDevsForItem(Item item, List<Develloppeur> newDevs)
        {
            devService.UpdateDevInItem(item, newDevs);
            //await apiconnector.UpdateDevForItem(item, newDevs);
        }
        public async Task UpdatesEditsForItem(Item item, List<Editeur> newEdits)
        {
            editService.UpdateEditeurInItem(item, newEdits);
            //await apiconnector.UpdateEditForItem(item, newEdits);
        }
        public async Task DeleteItem(Guid id)
        {
            apiconnector.DeleteItem(id);
        }
    }
}
