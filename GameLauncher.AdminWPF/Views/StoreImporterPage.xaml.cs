using System.Windows.Controls;

using GameLauncher.AdminWPF.ViewModels;

namespace GameLauncher.AdminWPF.Views;

public partial class StoreImporterPage : Page
{
    public StoreImporterPage(StoreImporterViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
