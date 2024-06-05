using GameLauncher.Admin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncher.Admin.Views;

public sealed partial class GenresPage : Page
{
    public GenresViewModel ViewModel
    {
        get;
    }

    public GenresPage()
    {
        ViewModel = App.GetService<GenresViewModel>();
        InitializeComponent();
    }
}
