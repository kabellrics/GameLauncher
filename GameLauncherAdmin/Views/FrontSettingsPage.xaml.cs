using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class FrontSettingsPage : Page
{
    public FrontSettingsViewModel ViewModel
    {
        get;
    }

    public FrontSettingsPage()
    {
        ViewModel = App.GetService<FrontSettingsViewModel>();
        InitializeComponent();
    }
}
