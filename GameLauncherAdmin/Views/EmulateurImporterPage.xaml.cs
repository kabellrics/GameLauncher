using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class EmulateurImporterPage : Page
{
    public EmulateurImporterViewModel ViewModel
    {
        get;
    }

    public EmulateurImporterPage()
    {
        ViewModel = App.GetService<EmulateurImporterViewModel>();
        InitializeComponent();
    }
}
