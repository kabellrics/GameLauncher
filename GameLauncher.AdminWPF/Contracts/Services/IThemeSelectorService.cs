using GameLauncher.AdminWPF.Models;

namespace GameLauncher.AdminWPF.Contracts.Services;

public interface IThemeSelectorService
{
    void InitializeTheme();

    void SetTheme(AppTheme theme);

    AppTheme GetCurrentTheme();
}
