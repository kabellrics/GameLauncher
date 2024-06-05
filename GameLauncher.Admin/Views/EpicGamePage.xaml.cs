using GameLauncher.Admin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncher.Admin.Views;

public sealed partial class EpicGamePage : Page
{
    public EpicGameViewModel ViewModel
    {
        get;
    }

    public EpicGamePage()
    {
        ViewModel = App.GetService<EpicGameViewModel>();
        InitializeComponent();
    }
}
