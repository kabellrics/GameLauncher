using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class PreviewItemPage : Page
{
    public PreviewItemViewModel ViewModel
    {
        get;
    }

    public PreviewItemPage()
    {
        ViewModel = App.GetService<PreviewItemViewModel>();
        InitializeComponent();
    }
}
