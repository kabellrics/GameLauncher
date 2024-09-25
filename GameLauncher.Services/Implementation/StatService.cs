using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;

namespace GameLauncher.Services.Implementation;
public class StatService : BaseService, IStatService
{
    public StatService(GameLauncherContext dbContext) : base(dbContext)
    {
    }

    public StatsObject GetStatistiques()
    {
        var stats = new StatsObject();
        stats.CollectionsWithoutArtwork = _dbContext.Collections.Where(x => string.IsNullOrWhiteSpace(x.Fanart)).ToList();
        stats.CollectionsWithoutLogo = _dbContext.Collections.Where(x => string.IsNullOrWhiteSpace(x.Logo)).ToList();
        stats.ItemsWithoutBanner = _dbContext.Items.Where(x => string.IsNullOrWhiteSpace(x.Banner)).ToList();
        stats.ItemsWithoutVideo = _dbContext.Items.Where(x => string.IsNullOrWhiteSpace(x.Video) && x.LUPlatformesId != "emulator").ToList();
        stats.ItemsWithoutArtwork = _dbContext.Items.Where(x => string.IsNullOrWhiteSpace(x.Artwork) && x.LUPlatformesId != "emulator").ToList();
        stats.ItemsWithoutCover = _dbContext.Items.Where(x => string.IsNullOrWhiteSpace(x.Cover)).ToList();
        stats.ItemsWithoutDescription = _dbContext.Items.Where(x => string.IsNullOrWhiteSpace(x.Description) && x.LUPlatformesId != "emulator").ToList();
        stats.ItemsWithoutReleaseDate = _dbContext.Items.Where(x => x.ReleaseDate == DateTime.MinValue && x.LUPlatformesId != "emulator").ToList();
        stats.ItemsWithoutGenres = _dbContext.Items.Where(x => !x.Genres.Any() && x.LUPlatformesId != "emulator").ToList();
        stats.ItemsWithoutDevelloppeurs = _dbContext.Items.Where(x => !x.Develloppeurs.Any() && x.LUPlatformesId != "emulator").ToList();
        stats.ItemsWithoutEditeurs = _dbContext.Items.Where(x => !x.Editeurs.Any() && x.LUPlatformesId != "emulator").ToList();
        return stats;
    }
}
