using System.Windows.Controls;

using GameLauncher.AdminWPF.ViewModels;

namespace GameLauncher.AdminWPF.Views;

public partial class DevellopeurPage : Page
{
    public DevellopeurPage(DevellopeurViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
