using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class DevelloppeurPage : Page
{
    public DevelloppeurViewModel ViewModel
    {
        get;
    }

    public DevelloppeurPage()
    {
        ViewModel = App.GetService<DevelloppeurViewModel>();
        InitializeComponent();
    }
}
