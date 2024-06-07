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
public class EditeurService : IEditeurService
{
    private readonly GameLauncherContext dbContext;
    public EditeurService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<Editeur> GetAll()
    {
        return dbContext.Editeurs.Include(x => x.Items);
    }
    public IEnumerable<Editeur> GetAllForItem(Guid id)
    {
        return dbContext.Editeurs.Where(x => x.Items.Any(item => item.ID == id));
    }
}
