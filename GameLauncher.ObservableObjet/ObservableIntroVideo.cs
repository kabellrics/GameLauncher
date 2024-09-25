using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Models;
using GameLauncher.Models.ScreenScraper;

namespace GameLauncher.ObservableObjet;
public class ObservableIntroVideo : ObservableObject
{
    public IntroVideo introVideo;
    public ObservableIntroVideo(IntroVideo item)
    {
        introVideo = item;
    }
    public Guid Id
    {
        get => introVideo.ID;
    }
    public string Name
    {
        get => introVideo.Name;
        set
        {
            SetProperty(introVideo.Name, value, introVideo, (syteme, item) => introVideo.Name = item);
        }
    }
    public string Path
    {
        get => introVideo.Path;
        set
        {
            SetProperty(introVideo.Path, value, introVideo, (syteme, item) => introVideo.Path = item);
        }
    }
    public bool IsUsed
    {
        get => introVideo.IsUsed;
        set
        {
            SetProperty(introVideo.IsUsed, value, introVideo, (syteme, item) => introVideo.IsUsed = item);
        }
    }
}
