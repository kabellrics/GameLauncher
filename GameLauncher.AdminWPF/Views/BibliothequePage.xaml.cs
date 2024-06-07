using System.Windows.Controls;

using GameLauncher.AdminWPF.ViewModels;

namespace GameLauncher.AdminWPF.Views;

public partial class BibliothequePage : Page
{
    public BibliothequePage(BibliothequeViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
