using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.UI.Animations;

using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace GameLauncherAdmin.Views;

public sealed partial class BibliothequeDetailPage : Page
{
    public BibliothequeDetailViewModel ViewModel
    {
        get;
    }

    public BibliothequeDetailPage()
    {
        ViewModel = App.GetService<BibliothequeDetailViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        this.RegisterElementForConnectedAnimation("animationKeyContentGrid", itemHero);
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        base.OnNavigatingFrom(e);
        if (e.NavigationMode == NavigationMode.Back)
        {
            var navigationService = App.GetService<INavigationService>();

            if (ViewModel.Item != null)
            {
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }

    private void PointerWheelIgnore(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        e.Handled = true;
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var items = e.AddedItems;
        ViewModel.GameToReconcile = (GameLauncher.ObservableObjet.ObservableItem)items.FirstOrDefault();
    }

    private void ReconcileGameMediaList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var items = e.AddedItems;
        ViewModel.MediaGameToReconcile = (GameLauncher.ObservableObjet.ObservableMediaItem)items.FirstOrDefault();
    }

    private void ListViewLogo_DoubleTapped(object sender, Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
    {
      var list = (ListView)sender;
        var url = list.SelectedItem.ToString();
        ViewModel.Item.Logo = url;
    }
    private void ListViewCover_DoubleTapped(object sender, Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
    {
        var list = (ListView)sender;
        var url = list.SelectedItem.ToString();
        ViewModel.Item.Cover = url;
    }
    private void ListViewArtwork_DoubleTapped(object sender, Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
    {
        var list = (ListView)sender;
        var url = list.SelectedItem.ToString();
        ViewModel.Item.Artwork = url;
    }
    private void ListViewBanner_DoubleTapped(object sender, Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
    {
        var list = (ListView)sender;
        var url = list.SelectedItem.ToString();
        ViewModel.Item.Banner = url;
    }
}
