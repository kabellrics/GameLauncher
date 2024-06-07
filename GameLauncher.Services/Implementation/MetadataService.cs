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
public class MetadataService : IMetadataService
{
    private readonly GameLauncherContext dbContext;
    public MetadataService(GameLauncherContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IEnumerable<MetadataGenre> GetAll()
    {
        return dbContext.MetadataGenres.Include(x => x.Items);
    }
    public IEnumerable<MetadataGenre> GetAllForItem(Guid id)
    {
        return dbContext.MetadataGenres.Where(x => x.Items.Any(item => item.ID == id));
    }
}
