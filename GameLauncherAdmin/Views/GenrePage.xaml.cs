using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class GenrePage : Page
{
    public GenreViewModel ViewModel
    {
        get;
    }

    public GenrePage()
    {
        ViewModel = App.GetService<GenreViewModel>();
        InitializeComponent();
    }
}
