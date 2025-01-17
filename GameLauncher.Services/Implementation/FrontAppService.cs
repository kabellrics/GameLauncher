using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface;

namespace GameLauncher.Services.Implementation;
public class FrontAppService : BaseService, IFrontAppService
{
    public FrontAppService(GameLauncherContext dbContext) : base(dbContext)
    {
    }
    public IEnumerable<FrontApp> GetAll()
    {
        return _dbContext.FrontEnds;
    }
    public FrontApp GetDefault()
    {
        return _dbContext.FrontEnds.FirstOrDefault(); ;
    }
    public void Update(FrontApp updatedfrontapp)
    {
        var item = _dbContext.FrontEnds.FirstOrDefault(x => x.ID == updatedfrontapp.ID);
        if (item != null)
        {
            item.Name = updatedfrontapp.Name;
            item.ItemDisplay = updatedfrontapp.ItemDisplay;
            item.CollectionDisplay = updatedfrontapp.CollectionDisplay;
            item.Path = updatedfrontapp.Path;
            item.IsUsed = updatedfrontapp.IsUsed;
            _dbContext.FrontEnds.Update(item);
            _dbContext.SaveChanges();
            SendNotification(MsgCategory.Update, $"FrontApp {item.Name} MAJ", $"FrontApp {item.Name} mis à jour");
        }
    }
}
