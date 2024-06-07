using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class BibliothequePage : Page
{
    public BibliothequeViewModel ViewModel
    {
        get;
    }

    public BibliothequePage()
    {
        ViewModel = App.GetService<BibliothequeViewModel>();
        InitializeComponent();
    }
}
