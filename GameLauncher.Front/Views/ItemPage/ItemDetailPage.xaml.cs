using GameLauncher.Front.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.System;

namespace GameLauncher.Front.Views;

public sealed partial class ItemDetailPage : Page
{
    public ItemDetailViewModel ViewModel
    {
        get;
    }

    public ItemDetailPage()
    {
        ViewModel = App.GetService<ItemDetailViewModel>();
        InitializeComponent();
    }
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if(ViewModel.Currentdisplay == Models.APIObject.ItemDisplay.Defaut)
        {
            defaultlayout.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            artworklayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            steamlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            xboxlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }else if (ViewModel.Currentdisplay == Models.APIObject.ItemDisplay.ArtworkNoDesc)
        {
            defaultlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            artworklayout.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            steamlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            xboxlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }
        else if (ViewModel.Currentdisplay == Models.APIObject.ItemDisplay.SteamLike)
        {
            defaultlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            artworklayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            steamlayout.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            xboxlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }
        else if (ViewModel.Currentdisplay == Models.APIObject.ItemDisplay.XBox)
        {
            defaultlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            artworklayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            steamlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            xboxlayout.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        }
    }
    private void Page_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Escape || e.Key == VirtualKey.Back) { ViewModel.GoBack(); }
    }
}
