using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Connector;
using GameLauncher.ObservableObjet;

namespace GameLauncher.AdminProvider
{
    public class ItemProvider : IItemProvider
    {
        private readonly GameLauncherClient apiconnector;
        public ItemProvider()
        {
            apiconnector = new GameLauncherClient("https://localhost:7197");
        }
        public async IAsyncEnumerable<ObservableItem> GetAllItems()
        {
            var items = await apiconnector.GetItemsAsync();
            foreach (var item in items.OrderBy(x=>x.Name))
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
                var metagenres = await apiconnector.GetMetadataGenresByItemAsync(item.ID);
                foreach (var metagenre in metagenres.OrderBy(x => x.Name))
                    obsItem.Genres.Add(new ObservableMetadataGenre(metagenre));
                yield return obsItem;
            }
        }
    }
}
