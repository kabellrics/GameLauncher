using System.Windows.Controls;

using GameLauncher.AdminWPF.ViewModels;

namespace GameLauncher.AdminWPF.Views;

public partial class EmulatorImporterPage : Page
{
    public EmulatorImporterPage(EmulatorImporterViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
