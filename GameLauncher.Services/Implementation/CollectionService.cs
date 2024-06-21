using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Services.Interface;

namespace GameLauncher.Services.Implementation;
public class CollectionService : ICollectionService
{
    private readonly GameLauncherContext dbContext;
    public CollectionService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<Collection> GetAll()
    {
        return dbContext.Collections;
    }
    public IEnumerable<Item> GetAllItemInside(Guid id)
    {
        var collecItems = dbContext.CollectiondItems.Where(x => x.CollectionID == id);
        foreach (var collecittem in collecItems.OrderBy(x => x.Order))
        {
            var item = dbContext.Items.FirstOrDefault(x => x.ID == collecittem.ItemID);
            if (item != null) yield return item;
        }
    }
    public void AddToCollectionEnd(Guid id, Guid gameid)
    {
        var collecitems = dbContext.CollectiondItems.Where(x => x.CollectionID == id);
        var lastitemorder = collecitems.Max(x => x.Order);
        var newCollecItem = new CollectionItem { CollectionID = id, ItemID = gameid, Order = lastitemorder + 1 };
        dbContext.CollectiondItems.Add(newCollecItem);
        dbContext.SaveChanges();
    }
    public void UpdateCollectionItemOrder(Guid id, Guid gameid, int newOrder)
    {
        var collecitem = dbContext.CollectiondItems.FirstOrDefault(x => x.CollectionID == id && x.ItemID == gameid);
        if (collecitem != null)
        {
            collecitem.Order = newOrder;
            var collecitemstoupdate = dbContext.CollectiondItems.Where(x => x.CollectionID == id && x.Order >= newOrder);
            foreach (var toupdatecollecitem in collecitemstoupdate)
                toupdatecollecitem.Order += 1;
            dbContext.SaveChanges();
        }

    }
    public void Update(Collection updatedcollection)
    {
        var item = dbContext.Collections.FirstOrDefault(x => x.ID == updatedcollection.ID);
        if (item != null)
        {
            item.Name = updatedcollection.Name;
            item.Order = updatedcollection.Order;
            item.CodeName = updatedcollection.CodeName;
            item.Fanart = updatedcollection.Fanart;
            item.Logo = updatedcollection.Logo;
            dbContext.Collections.Update(item);
            dbContext.SaveChanges();
        }
    }
}
