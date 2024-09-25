using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface.Front;
using Microsoft.AspNetCore.SignalR;

namespace GameLauncher.Services.Implementation.Front;
public class VideoIntroService : IVideoIntroService
{
    protected readonly GameLauncherContext _dbContext;
    public VideoIntroService(GameLauncherContext dbContext)
    {
        this._dbContext = dbContext;
    }
    public IntroVideo GetRandomVideoIntro()
    {
        var videos = _dbContext.IntroVideos.ToList();
        var video = videos.Where(x => x.IsUsed).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        if (video != null)
        {
            return video;
        }
        var defaultvideo = videos.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        if (defaultvideo != null)
            return defaultvideo;
        return null;
    }
    public IEnumerable<IntroVideo> GetIntroVideos()
    {
        return _dbContext.IntroVideos;
    }
}
