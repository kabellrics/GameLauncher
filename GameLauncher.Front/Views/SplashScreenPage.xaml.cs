using GameLauncher.Front.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Windows.Media.Playback;

namespace GameLauncher.Front.Views;

public sealed partial class SplashScreenPage : Page
{
    private Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;
    public SplashScreenViewModel ViewModel
    {
        get;
    }

    public SplashScreenPage()
    {
        ViewModel = App.GetService<SplashScreenViewModel>();
        InitializeComponent();
        dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        MyMediaPlayer.MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        MyMediaPlayer.MediaPlayer.MediaFailed += MediaPlayer_MediaFailed;
        this.Loaded += SplashScreenPage_Loaded;
    }
    private void SplashScreenPage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var isfocused = BTSplash.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);
    }

    private void MediaPlayer_MediaFailed(MediaPlayer sender, MediaPlayerFailedEventArgs args)
    {
        dispatcherQueue.TryEnqueue(() =>
        {
            ViewModel.GoToList();
        });
    }

    private void MediaPlayer_MediaEnded(MediaPlayer sender, object args)
    {
        dispatcherQueue.TryEnqueue(() =>
        {
            ViewModel.GoToList();
        });
    }
    private void Page_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        MyMediaPlayer.MediaPlayer.Pause();
        ViewModel.GoToList();
    }
    private void ContentArea_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        MyMediaPlayer.MediaPlayer.Pause();
        ViewModel.GoToList();
    }
    private void MyMediaPlayer_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        MyMediaPlayer.MediaPlayer.Pause();
        ViewModel.GoToList();
    }

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        MyMediaPlayer.MediaPlayer.Pause();
        ViewModel.GoToList();
    }

    private void BTSplash_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        MyMediaPlayer.MediaPlayer.Pause();
        ViewModel.GoToList();
    }
}
