using GameLauncher.Admin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncher.Admin.Views;

public sealed partial class SteamPage : Page
{
    public SteamViewModel ViewModel
    {
        get;
    }

    public SteamPage()
    {
        ViewModel = App.GetService<SteamViewModel>();
        InitializeComponent();
    }
}
