using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GameLauncher.Front.Views.CollectionPage.Layout;
public sealed partial class Semantic : UserControl
{
    public Semantic()
    {
        this.InitializeComponent();
    }

    private async void CollectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            //await CollectionList.SmoothScrollIntoViewWithIndexAsync(ViewModel.CurrentCollectionListIndex, ScrollItemPlacement.Center, false, true);
            if (this.Visibility == Visibility.Visible && CollectionList.IsLoaded == true && CollectionList.Items.Any())
            {
                //CollectionList.ScrollIntoView(CollectionList.SelectedItem);
                await CollectionList.SmoothScrollIntoViewWithItemAsync(CollectionList.SelectedItem, ScrollItemPlacement.Top, false, true);
            }
        }
        catch (System.NullReferenceException ex)
        {
            //throw;
        }
        catch (Exception ex)
        {
            //throw;
        }
    }

    private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            //await ItemsList.SmoothScrollIntoViewWithIndexAsync(ViewModel.CurrentItemListIndex, ScrollItemPlacement.Center, false, true);
            if (this.Visibility == Visibility.Visible && ItemsList.IsLoaded == true && ItemsList.Items.Any())
            {
                //ItemsList.ScrollIntoView(ItemsList.SelectedItem);
                await ItemsList.SmoothScrollIntoViewWithItemAsync(ItemsList.SelectedItem, ScrollItemPlacement.Left, false, true);
            }
        }
        catch (System.NullReferenceException ex)
        {
            //throw;
        }
        catch (Exception ex)
        {
            //throw;
        }
    }

    private void CollectionList_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Right || e.Key == VirtualKey.GamepadDPadRight)
        {
            var isfocused = ItemsList.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);
        }
    }

    private void ItemsList_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (ItemsList.SelectedIndex == 0)
        {
            if (e.Key == VirtualKey.Left || e.Key == VirtualKey.GamepadDPadLeft)
            {
                var isfocused = CollectionList.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);
            } 
        }
    }
}
