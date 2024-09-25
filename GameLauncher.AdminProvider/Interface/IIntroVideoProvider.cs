using GameLauncher.Models;
using GameLauncher.Models.APIObject;

namespace GameLauncher.AdminProvider.Interface;
public interface IIntroVideoProvider
{
    Task CreateIntroVideo(FileRequest item);
    Task DeleteIntroVideo(Guid id);
    Task<IEnumerable<IntroVideo>> GetIntroVideo();
    Task UpdateIntroVideo(IntroVideo item);
}