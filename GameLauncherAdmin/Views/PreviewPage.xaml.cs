using GameLauncherAdmin.ViewModels;
using Windows.Media.Core;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.Media.Playback;
using System.Timers;

namespace GameLauncherAdmin.Views;

public sealed partial class PreviewPage : Page
{
    public PreviewViewModel ViewModel
    {
        get;
    }
    private System.Timers.Timer _timer;
    private MediaSource _currentSource;
    private Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;
    public PreviewPage()
    {
        ViewModel = App.GetService<PreviewViewModel>();
        InitializeComponent();
        MyMediaPlayer.MediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
        MyMediaPlayer.MediaPlayer.MediaFailed += MediaPlayer_MediaFailed;
        MyMediaPlayer.MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
    }
    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        base.OnNavigatingFrom(e);
        _timer?.Stop();
        MyMediaPlayer.MediaPlayer.Pause();
        MyMediaPlayer.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
    }
    private void MediaPlayer_MediaEnded(MediaPlayer sender, object args)
    {
        DispatcherQueue.TryEnqueue(() =>
        {
            MyMediaPlayer.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        });
    }

    private void MediaPlayer_MediaOpened(MediaPlayer sender, object args)
    {
        dispatcherQueue.TryEnqueue(() =>
        {
            MyMediaPlayer.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        });
        // Start the timer when the media is opened
        StartVisibilityTimer();
    }

    private void MediaPlayer_MediaFailed(MediaPlayer sender, MediaPlayerFailedEventArgs args)
    {
        // Handle media loading failure (optional)
        dispatcherQueue.TryEnqueue(() =>
        {
            MyMediaPlayer.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        });
    }
    private void StartVisibilityTimer()
    {
        // Reset the timer if it already exists
        _timer?.Stop();
        _timer = new System.Timers.Timer(5000); // 5 seconds delay
        _timer.Elapsed += Timer_Elapsed;
        _timer.AutoReset = false;
        _timer.Start();
    }
    private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        dispatcherQueue.TryEnqueue(() =>
        {
            // Make MediaPlayerElement visible and start the video
            MyMediaPlayer.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            MyMediaPlayer.MediaPlayer.Play();
        });
    }
    protected override void OnNavigatedTo(NavigationEventArgs e){
        base.OnNavigatedTo(e);
        try
        {
            CollectionList.Focus(Microsoft.UI.Xaml.FocusState.Keyboard);
            CollectionList.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            //throw;
        }
    }

    private void CollectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {

            ItemList.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            //throw;
        }
    }

    private void ItemList_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        try
        {
            if (e.Key == Windows.System.VirtualKey.Up || e.Key == Windows.System.VirtualKey.GamepadDPadUp)
            {
                CollectionList.Focus(Microsoft.UI.Xaml.FocusState.Keyboard); 
            } } catch { }
    }

    private void CollectionList_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        try
        {
            if (e.Key == Windows.System.VirtualKey.Down || e.Key == Windows.System.VirtualKey.GamepadDPadDown)
            {
                ItemList.Focus(Microsoft.UI.Xaml.FocusState.Keyboard); 
            } } catch { }
    }
}
