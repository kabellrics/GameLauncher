using System.Windows.Controls;

namespace GameLauncher.AdminWPF.Contracts.Views;

public interface IShellWindow
{
    Frame GetNavigationFrame();

    void ShowWindow();

    void CloseWindow();
}
