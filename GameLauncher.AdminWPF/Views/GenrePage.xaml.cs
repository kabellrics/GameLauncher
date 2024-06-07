using System.Windows.Controls;

using GameLauncher.AdminWPF.ViewModels;

namespace GameLauncher.AdminWPF.Views;

public partial class GenrePage : Page
{
    public GenrePage(GenreViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
