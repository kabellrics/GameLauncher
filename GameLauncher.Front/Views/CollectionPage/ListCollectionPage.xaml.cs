using GameLauncher.Front.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Windows.System;
using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.UI;

namespace GameLauncher.Front.Views;

public sealed partial class ListCollectionPage : Page
{
    public ListCollectionViewModel ViewModel
    {
        get;
    }

    public ListCollectionPage()
    {
        ViewModel = App.GetService<ListCollectionViewModel>();
        InitializeComponent();
    }
    private void CollectionList_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var isfocused = CollectionList.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);
    }

    private void CollectionList_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Down || e.Key == VirtualKey.GamepadDPadDown)
        {
            var isfocused = ItemsList.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);
            ItemsList.SelectedIndex = ViewModel.CurrentItemListIndex;
        }
    }

    private void ItemsList_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Up || e.Key == VirtualKey.GamepadDPadUp)
        {
            var isfocused = CollectionList.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);
            CollectionList.SelectedIndex = ViewModel.CurrentCollectionListIndex;
        }
    }

    private async void CollectionList_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
    {
        try
        {
            await CollectionList.SmoothScrollIntoViewWithIndexAsync(CollectionList.SelectedIndex, ScrollItemPlacement.Center, false, true);
        }
        catch (NullReferenceException ex)
        {
            //throw;
        }
        catch (Exception ex)
        {
            //throw;
        }
    }

    private async void ItemsList_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
    {
        try
        {
            await ItemsList.SmoothScrollIntoViewWithIndexAsync(ItemsList.SelectedIndex, ScrollItemPlacement.Center, false, true);
        }
        catch (NullReferenceException ex)
        {
            //throw;
        }
        catch (Exception ex)
        {
            //throw;
        }

    }
}
