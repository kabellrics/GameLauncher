using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.Services.Implementation;
public class DevService : IDevService
{
    private readonly GameLauncherContext dbContext;
    public DevService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<Develloppeur> GetAll()
    {
        return dbContext.Develloppeurs.Include(x=>x.Items);
    }
    public IEnumerable<Develloppeur> GetAllForItem(Guid id)
    {
        return dbContext.Develloppeurs.Where(x => x.Items.Any(item => item.ID == id));
    }
    public ItemDev AddDevToItem(string editeurname, Item item)
    {
        var dbgenre = dbContext.Develloppeurs.FirstOrDefault(x => x.Name == editeurname);
        if (dbgenre == null)
        {
            dbgenre = new Develloppeur();
            dbgenre.Name = editeurname;
            dbgenre.Items = new List<ItemDev>();
            dbContext.Develloppeurs.Add(dbgenre);
        }
        var itemgenre = new ItemDev()
        {
            ID = Guid.NewGuid(),
            DevelloppeurID = dbgenre.ID,
            ItemID = item.ID
        };
        dbContext.DevdItems.Add(itemgenre);
        if (!dbgenre.Items.Contains(itemgenre))
        {
            dbgenre.Items.Add(itemgenre);
        }
        dbContext.SaveChanges();
        return itemgenre;
    }
}
