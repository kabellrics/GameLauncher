using GameLauncher.Admin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncher.Admin.Views;

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
