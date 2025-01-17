using GameLauncher.Front.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Windows.System;
using Microsoft.UI.Xaml.Navigation;

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
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (ViewModel.Currentdisplay == Models.APIObject.CollectionDisplay.Defaut)
        {
            defaultlayout.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            semanticlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            gridhublayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }
        else if (ViewModel.Currentdisplay == Models.APIObject.CollectionDisplay.SemanticFlip)
        {
            defaultlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            semanticlayout.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            gridhublayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }
        else if (ViewModel.Currentdisplay == Models.APIObject.CollectionDisplay.GridHub)
        {
            defaultlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            semanticlayout.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            gridhublayout.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        }
    }
}
