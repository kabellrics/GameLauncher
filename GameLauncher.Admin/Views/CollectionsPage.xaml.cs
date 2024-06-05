using GameLauncher.Admin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncher.Admin.Views;

public sealed partial class CollectionsPage : Page
{
    public CollectionsViewModel ViewModel
    {
        get;
    }

    public CollectionsPage()
    {
        ViewModel = App.GetService<CollectionsViewModel>();
        InitializeComponent();
    }
}
