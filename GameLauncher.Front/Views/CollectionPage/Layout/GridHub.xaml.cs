using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using GameLauncher.Models.APIObject;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GameLauncher.Front.Views.CollectionPage.Layout;
public sealed partial class GridHub : UserControl
{
    public GridHub()
    {
        this.InitializeComponent();
        Loaded += GridHub_Loaded;
    }

    private void GridHub_Loaded(object sender, RoutedEventArgs e)
    {
        //var collectionGroups = cvsGroups.View.CollectionGroups;
        //((ListViewBase)this.Zoom.ZoomedOutView).ItemsSource = collectionGroups;
    }

    private void SemanticZoom_ViewChangeStarted(object sender, SemanticZoomViewChangedEventArgs e)
    {
        if (e.IsSourceZoomedInView == false)
        {
            zoominbck.Visibility = Visibility.Visible;
        }
        else
        {
            zoominbck.Visibility = Visibility.Collapsed;
        }
    }

    private async void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            //await CollectionList.SmoothScrollIntoViewWithIndexAsync(ViewModel.CurrentCollectionListIndex, ScrollItemPlacement.Center, false, true);
            if (this.Visibility == Visibility.Visible && zoomoutcollection.IsLoaded == true && zoomoutcollection.Items.Any())
            {
                //CollectionList.ScrollIntoView(CollectionList.SelectedItem);
                await zoomoutcollection.SmoothScrollIntoViewWithItemAsync(zoomoutcollection.SelectedItem, ScrollItemPlacement.Left, false, true);
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

    private async void zoomincollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            //await CollectionList.SmoothScrollIntoViewWithIndexAsync(ViewModel.CurrentCollectionListIndex, ScrollItemPlacement.Center, false, true);
            if (this.Visibility == Visibility.Visible && zoomincollection.IsLoaded == true && zoomincollection.Items.Any())
            {
                //CollectionList.ScrollIntoView(CollectionList.SelectedItem);
                await zoomincollection.SmoothScrollIntoViewWithItemAsync(zoomincollection.SelectedItem, ScrollItemPlacement.Left, false, true);
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
}
