using GameLauncher.Models;

namespace GameLauncher.Services.Interface.Front;
public interface IVideoIntroService
{
    IEnumerable<IntroVideo> GetIntroVideos();
    IntroVideo GetRandomVideoIntro();
}