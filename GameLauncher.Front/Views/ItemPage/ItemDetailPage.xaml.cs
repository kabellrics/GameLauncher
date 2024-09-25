using GameLauncher.Front.ViewModels;

using Microsoft.UI.Xaml.Controls;
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
    private void Page_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Escape || e.Key == VirtualKey.Back) { ViewModel.GoBack(); }
    }
}
