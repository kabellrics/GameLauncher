using System.Windows.Controls;

using GameLauncher.AdminWPF.Contracts.Views;
using GameLauncher.AdminWPF.ViewModels;

using MahApps.Metro.Controls;

namespace GameLauncher.AdminWPF.Views;

public partial class ShellWindow : MetroWindow, IShellWindow
{
    public ShellWindow(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => shellFrame;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();
}
