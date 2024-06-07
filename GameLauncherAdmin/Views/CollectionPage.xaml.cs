using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class CollectionPage : Page
{
    public CollectionViewModel ViewModel
    {
        get;
    }

    public CollectionPage()
    {
        ViewModel = App.GetService<CollectionViewModel>();
        InitializeComponent();
    }
}
