using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class StoreImporterPage : Page
{
    public StoreImporterViewModel ViewModel
    {
        get;
    }

    public StoreImporterPage()
    {
        ViewModel = App.GetService<StoreImporterViewModel>();
        InitializeComponent();
    }
}
