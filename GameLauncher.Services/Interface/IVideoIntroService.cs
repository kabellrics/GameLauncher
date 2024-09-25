using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface IVideoIntroService
{
    void DeleteIntroVideo(Guid id);
    IEnumerable<IntroVideo> GetIntroVideos();
    Task InsertIntroVideo(string sourcefile, string sourcename);
    void UpdateItem(IntroVideo updateditem);
    IntroVideo GetRandomVideoIntro();
}