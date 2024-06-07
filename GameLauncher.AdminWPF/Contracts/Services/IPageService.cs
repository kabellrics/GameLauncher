using System.Windows.Controls;

namespace GameLauncher.AdminWPF.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);

    Page GetPage(string key);
}
