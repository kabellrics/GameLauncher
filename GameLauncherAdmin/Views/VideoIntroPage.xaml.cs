using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class VideoIntroPage : Page
{
    public VideoIntroViewModel ViewModel
    {
        get;
    }

    public VideoIntroPage()
    {
        ViewModel = App.GetService<VideoIntroViewModel>();
        InitializeComponent();
    }
}
