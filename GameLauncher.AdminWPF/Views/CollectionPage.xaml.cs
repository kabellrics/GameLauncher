using System.Windows.Controls;

using GameLauncher.AdminWPF.ViewModels;

namespace GameLauncher.AdminWPF.Views;

public partial class CollectionPage : Page
{
    public CollectionPage(CollectionViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
