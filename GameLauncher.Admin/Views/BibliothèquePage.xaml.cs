using GameLauncher.Admin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncher.Admin.Views;

public sealed partial class BibliothèquePage : Page
{
    public BibliothèqueViewModel ViewModel
    {
        get;
    }

    public BibliothèquePage()
    {
        ViewModel = App.GetService<BibliothèqueViewModel>();
        InitializeComponent();
    }
}
