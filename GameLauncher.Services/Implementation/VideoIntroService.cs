using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.Services.Interface;
using Microsoft.AspNetCore.SignalR;

namespace GameLauncher.Services.Implementation;
public class VideoIntroService : BaseService, IVideoIntroService
{
    private readonly IAssetDownloader assetService;
    public VideoIntroService(GameLauncherContext dbContext, IAssetDownloader assetService) : base(dbContext)
    {
        this.assetService = assetService;
    }
    public async Task InsertIntroVideo(string sourcefile, string sourcename)
    {
        IntroVideo intro = new IntroVideo();
        intro.Name = sourcename;
        intro.IsUsed = false;
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string targetfolder = Path.Combine(documentsPath, "GameLauncher", "Assets", "Intro");
        Directory.CreateDirectory(targetfolder);
        string targetfile = Path.Combine(targetfolder, $"{sourcename}.mp4");
        intro.Path = targetfile;
        await assetService.GetIntroVideo(sourcefile, targetfile);//.ContinueWith(task =>
        //{
          //  if (task.IsCompletedSuccessfully)
            //{
                _dbContext.IntroVideos.Add(intro);
                _dbContext.SaveChanges();
                SendNotification(MsgCategory.Create, "Ajout d'un video d'intro", $"Ajout de la video d'intro : {sourcename}");
        //    }
        //});
    }
    public IntroVideo GetRandomVideoIntro()
    {
        var videos = _dbContext.IntroVideos.ToList();
        var video = videos.Where(x=>x.IsUsed).OrderBy(x=> Guid.NewGuid()).FirstOrDefault();
        if(video != null)
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
    public void DeleteIntroVideo(Guid id)
    {
        var deleteitem = _dbContext.IntroVideos.FirstOrDefault(x => x.ID == id);
        if (deleteitem != null)
        {
            _dbContext.IntroVideos.Remove(deleteitem);
            SendNotification(MsgCategory.Delete, " IntroVideos supprimé", "IntroVideos retiré de la bibliothèque");
        }
        try
        {
            File.Delete(deleteitem.Path);
        }
        catch (Exception ex)
        {
            //throw;
        }
        _dbContext.SaveChanges();
    }
    public void UpdateItem(IntroVideo updateditem)
    {
        var item = _dbContext.IntroVideos.FirstOrDefault(x => x.ID == updateditem.ID);
        if (item != null)
        {
            item.Name = updateditem.Name;
            item.Path = updateditem.Path;
            item.IsUsed = updateditem.IsUsed;
            _dbContext.IntroVideos.Update(item);
            _dbContext.SaveChanges();
        }
    }
}
