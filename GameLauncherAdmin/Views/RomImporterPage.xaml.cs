using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class RomImporterPage : Page
{
    public RomImporterViewModel ViewModel
    {
        get;
    }

    public RomImporterPage()
    {
        ViewModel = App.GetService<RomImporterViewModel>();
        InitializeComponent();
    }
}
