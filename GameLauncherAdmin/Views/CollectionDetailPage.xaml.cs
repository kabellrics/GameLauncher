using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Views;

public sealed partial class CollectionDetailPage : Page
{
    public CollectionDetailViewModel ViewModel
    {
        get;
    }

    public CollectionDetailPage()
    {
        ViewModel = App.GetService<CollectionDetailViewModel>();
        InitializeComponent();
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var sort = e.AddedItems.First().ToString();
        ViewModel.SortObs(sort);
    }

    private void ListView_DropCompleted(Microsoft.UI.Xaml.UIElement sender, Microsoft.UI.Xaml.DropCompletedEventArgs args)
    {
        ViewModel.ReInitOrder();
    }
}
