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
            foreach (var item in items)
            {
                yield return new ObservableItem(item);
            }
        }
    }
}
