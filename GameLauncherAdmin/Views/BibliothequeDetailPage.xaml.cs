using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.UI.Animations;

using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.Storage.Pickers;

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
        this.Unloaded += BibliothequeDetailPage_Unloaded;
        InitializeComponent();
    }
    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
        try
        {
            var player = this.videoassetplayer;
            player.MediaPlayer.Pause();
            player.MediaPlayer.Dispose();
        }
        catch (Exception ex)
        {
            //throw;
        }
    }
    private void BibliothequeDetailPage_Unloaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        try
        {
            var player = this.videoassetplayer;
            player.MediaPlayer.Pause();
            player.MediaPlayer.Dispose();
        }
        catch (Exception ex)
        {
            //throw;
        }
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

    private void videoassetplayer_LostFocus(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        try
        {
            var player = sender as MediaPlayerElement;
            player.MediaPlayer.Pause();
            player.MediaPlayer.Dispose();
        }
        catch (Exception ex)
        {
            //throw;
        }
    }

    private async void PickLocalCover(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var Picker = new FileOpenPicker();
        var window = App.MainWindow;
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WinRT.Interop.InitializeWithWindow.Initialize(Picker, hWnd);
        Picker.ViewMode = PickerViewMode.Thumbnail;
        Picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        Picker.FileTypeFilter.Add(".png");
        Picker.FileTypeFilter.Add(".jpg");
        Picker.FileTypeFilter.Add(".jpeg");
        var file = await Picker.PickSingleFileAsync();
        if (file != null)
            ViewModel.Item.Cover = file.Path;
    }

    private async void PickLocalLogo(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

        var Picker = new FileOpenPicker();
        var window = App.MainWindow;
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WinRT.Interop.InitializeWithWindow.Initialize(Picker, hWnd);
        Picker.ViewMode = PickerViewMode.Thumbnail;
        Picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        Picker.FileTypeFilter.Add(".png");
        var file = await Picker.PickSingleFileAsync();
        if (file != null)
            ViewModel.Item.Logo = file.Path;
    }

    private async void PickLocalArtwork(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

        var Picker = new FileOpenPicker();
        var window = App.MainWindow;
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WinRT.Interop.InitializeWithWindow.Initialize(Picker, hWnd);
        Picker.ViewMode = PickerViewMode.Thumbnail;
        Picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        Picker.FileTypeFilter.Add(".png");
        Picker.FileTypeFilter.Add(".jpg");
        Picker.FileTypeFilter.Add(".jpeg");
        var file = await Picker.PickSingleFileAsync();
        if (file != null)
            ViewModel.Item.Artwork = file.Path;
    }

    private async void PickLocalVideo(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

        var Picker = new FileOpenPicker();
        var window = App.MainWindow;
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WinRT.Interop.InitializeWithWindow.Initialize(Picker, hWnd);
        Picker.ViewMode = PickerViewMode.Thumbnail;
        Picker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
        Picker.FileTypeFilter.Add(".mp4");
        var file = await Picker.PickSingleFileAsync();
        if (file != null)
            ViewModel.Item.Video = file.Path;
    }

    private async void PickLocalBanner(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var Picker = new FileOpenPicker();
        var window = App.MainWindow;
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WinRT.Interop.InitializeWithWindow.Initialize(Picker, hWnd);
        Picker.ViewMode = PickerViewMode.Thumbnail;
        Picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        Picker.FileTypeFilter.Add(".png");
        Picker.FileTypeFilter.Add(".jpg");
        Picker.FileTypeFilter.Add(".jpeg");
        var file = await Picker.PickSingleFileAsync();
        if (file != null)
            ViewModel.Item.Banner = file.Path;
    }
}
