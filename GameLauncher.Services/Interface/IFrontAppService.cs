using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface IFrontAppService
{
    IEnumerable<FrontApp> GetAll();
    FrontApp GetDefault();
    void Update(FrontApp updatedfrontapp);
}