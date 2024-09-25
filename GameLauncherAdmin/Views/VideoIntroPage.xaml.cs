using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Windows.Storage.Pickers;

namespace GameLauncherAdmin.Views;

public sealed partial class VideoIntroPage : Page
{
    public VideoIntroViewModel ViewModel
    {
        get;
    }

    public VideoIntroPage()
    {
        ViewModel = App.GetService<VideoIntroViewModel>();
        InitializeComponent();
    }

    private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
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
            ViewModel.AddVideo(file.Path);
    }
}
