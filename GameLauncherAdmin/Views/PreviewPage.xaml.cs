using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class PreviewPage : Page
{
    public PreviewViewModel ViewModel
    {
        get;
    }

    public PreviewPage()
    {
        ViewModel = App.GetService<PreviewViewModel>();
        InitializeComponent();
    }
}
