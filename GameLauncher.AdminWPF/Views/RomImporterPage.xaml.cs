using System.Windows.Controls;

using GameLauncher.AdminWPF.ViewModels;

namespace GameLauncher.AdminWPF.Views;

public partial class RomImporterPage : Page
{
    public RomImporterPage(RomImporterViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
