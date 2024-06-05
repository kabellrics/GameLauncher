using GameLauncher.Admin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncher.Admin.Views;

public sealed partial class ImportRomGamesPage : Page
{
    public ImportRomGamesViewModel ViewModel
    {
        get;
    }

    public ImportRomGamesPage()
    {
        ViewModel = App.GetService<ImportRomGamesViewModel>();
        InitializeComponent();
    }
}
