using GameLauncherAdmin.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Windows.Storage.AccessCache;
using Windows.Storage;
using Windows.Storage.Pickers;
using GameLauncher.ObservableObjet;

namespace GameLauncherAdmin.Views;

public sealed partial class EmulateurImporterPage : Page
{
    public EmulateurImporterViewModel ViewModel
    {
        get;
    }

    public EmulateurImporterPage()
    {
        ViewModel = App.GetService<EmulateurImporterViewModel>();
        InitializeComponent();
    }

    private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        FolderPicker openPicker = new Windows.Storage.Pickers.FolderPicker();
        var window = App.MainWindow;
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);
        openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
        openPicker.FileTypeFilter.Add("*");
        StorageFolder folder = await openPicker.PickSingleFolderAsync();
        if (folder != null)
        {
            ViewModel.ScanEmulator(folder.Path);
        }
    }

    private async void Button_ChooseEmu(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var selectedItem = listEmuItem.SelectedItem as ObservableEmulateur;
        await ViewModel.GetProfileForEmulateurAsync(selectedItem);
    }

    private async void Button_ChooseProfile(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var selectedItem = listEmuProfileItem.SelectedItem as ObservableProfile;
        await ViewModel.GetPlateformeForProfileAsync(selectedItem);
    }

    private async void Button_ChoosePlateforme(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var selectedItem = listEmuProfilePlatformItem.SelectedItem as ObservablePlateforme;
        await ViewModel.PrepareScanningProfileAsync(selectedItem);
    }

    private async void Button_Click_1(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        FolderPicker openPicker = new Windows.Storage.Pickers.FolderPicker();
        var window = App.MainWindow;
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);
        openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
        openPicker.FileTypeFilter.Add("*");
        StorageFolder folder = await openPicker.PickSingleFolderAsync();
        if (folder != null)
        {
            ViewModel.ScanningProfile.FolderPath = folder.Path;
        }
    }
}
