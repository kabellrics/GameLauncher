using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Connector;
using GameLauncher.Models;
using GameLauncher.Models.APIObject;
using GameLauncher.ObservableObjet;
using GameLauncher.Services.Interface;

namespace GameLauncher.AdminProvider;
public class IntroVideoProvider : IIntroVideoProvider
{
    //private readonly VideoIntroConnector videointroconnector;
    private readonly IVideoIntroService videointroconnector;
    public IntroVideoProvider(IVideoIntroService video)
    {
        videointroconnector = video;
        //videointroconnector = new VideoIntroConnector("https://localhost:7197");
    }
    public async Task<IEnumerable<IntroVideo>> GetIntroVideo()
    {
        return videointroconnector.GetIntroVideos();
    }
    public async Task DeleteIntroVideo(Guid id)
    {
        videointroconnector.DeleteIntroVideo(id);
    }
    public async Task CreateIntroVideo(FileRequest item)
    {
        await videointroconnector.InsertIntroVideo(item.SourceFile,item.NameFile);
    }
    public async Task UpdateIntroVideo(IntroVideo item)
    {
        videointroconnector.UpdateItem(item);
    }
}
