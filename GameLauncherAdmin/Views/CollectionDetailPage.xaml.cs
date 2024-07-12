using GameLauncher.ObservableObjet;
using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Windows.Storage.Pickers;

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

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ViewModel.SaveChanges();
    }

    private void Button_Click_1(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var items = proposalItem.SelectedItems.Select(x=> x as ObservableItem);
        ViewModel.AddToCollec(items);
    }

    private void Button_Click_2(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var items = collectioncontent.SelectedItems.Select(x => x as ObservableItemInCollection);
        ViewModel.RemoveToCollec(items);
    }

    private async void Button_ChangeLogo(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var logoPicker = new FileOpenPicker();
        var window = App.MainWindow;
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WinRT.Interop.InitializeWithWindow.Initialize(logoPicker, hWnd);
        logoPicker.ViewMode = PickerViewMode.Thumbnail;
        logoPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        logoPicker.FileTypeFilter.Add(".png");
        var file = await logoPicker.PickSingleFileAsync();
        if (file != null)
            ViewModel.SetNewLogoPath(file.Path);

    }

    private async void Button_ChangeFanart(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

        var fanartPicker = new FileOpenPicker();
        var window = App.MainWindow;
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WinRT.Interop.InitializeWithWindow.Initialize(fanartPicker, hWnd);
        fanartPicker.ViewMode = PickerViewMode.Thumbnail;
        fanartPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        fanartPicker.FileTypeFilter.Add(".png");
        fanartPicker.FileTypeFilter.Add(".jpg");
        fanartPicker.FileTypeFilter.Add(".jpeg");
        var file = await fanartPicker.PickSingleFileAsync();
        if (file != null)
            ViewModel.SetNewFanartPath(file.Path);
    }
}
