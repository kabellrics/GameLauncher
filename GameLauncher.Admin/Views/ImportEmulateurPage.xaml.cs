using GameLauncher.Admin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncher.Admin.Views;

public sealed partial class ImportEmulateurPage : Page
{
    public ImportEmulateurViewModel ViewModel
    {
        get;
    }

    public ImportEmulateurPage()
    {
        ViewModel = App.GetService<ImportEmulateurViewModel>();
        InitializeComponent();
    }
}
