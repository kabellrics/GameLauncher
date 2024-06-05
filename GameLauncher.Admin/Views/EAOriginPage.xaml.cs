using GameLauncher.Admin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncher.Admin.Views;

public sealed partial class EAOriginPage : Page
{
    public EAOriginViewModel ViewModel
    {
        get;
    }

    public EAOriginPage()
    {
        ViewModel = App.GetService<EAOriginViewModel>();
        InitializeComponent();
    }
}
