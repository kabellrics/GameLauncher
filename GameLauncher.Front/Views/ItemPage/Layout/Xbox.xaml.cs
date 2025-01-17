using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using GameLauncher.Front.ViewModels;
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

namespace GameLauncher.Front.Views.ItemPage.Layout;
public sealed partial class Xbox : UserControl
{
    public Xbox()
    {
        this.InitializeComponent();
        this.DataContextChanged += Xbox_DataContextChanged;
        this.Unloaded += Xbox_Unloaded;
    }

    private void Xbox_Unloaded(object sender, RoutedEventArgs e)
    {
        videoassetplayer.MediaPlayer.Pause();
    }

    private void Xbox_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        if (this.DataContext is ItemDetailViewModel)
        {
            var vm = this.DataContext as ItemDetailViewModel;
            //rtbdescription.Blocks.Clear();
            //rtbdescription.Blocks.Add(vm.CurrentItemDescription);
        }
    }
}
