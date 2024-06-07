using System.Windows.Controls;

using GameLauncher.AdminWPF.ViewModels;

namespace GameLauncher.AdminWPF.Views;

public partial class EditeurPage : Page
{
    public EditeurPage(EditeurViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
