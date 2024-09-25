using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.Front.Contracts.Services;
using GameLauncher.Front.Contracts.ViewModels;
using GameLauncher.Services.Interface.Front;
using Windows.Media.Core;
using Windows.Services.Maps;

namespace GameLauncher.Front.ViewModels;

public partial class SplashScreenViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IVideoIntroService _videoIntroService;
    private readonly ICollectionService _collectionService;
    [ObservableProperty]
    private MediaSource _splashascreen;
    private Task LoadItems;
    public SplashScreenViewModel(INavigationService navigationService, IVideoIntroService videoIntroService, ICollectionService collectionService)
    {
        _navigationService = navigationService;
        _videoIntroService = videoIntroService;
        _collectionService = collectionService;
    }
    public void OnNavigatedFrom()
    {
    }
    public void OnNavigatedTo(object parameter)
    {
        LoadVideo();
        LoadItems = _collectionService.GetAllFull();
    }
    public async void LoadVideo()
    {
        var introvideo = _videoIntroService.GetRandomVideoIntro();
        if (introvideo != null && File.Exists(introvideo.Path))
        {
            Uri uri = new Uri(introvideo.Path, UriKind.RelativeOrAbsolute);
            Splashascreen = MediaSource.CreateFromUri(uri);
        }
        else
        {
            GoToList();
        }
    }
    public void GoToList()
    {
        _navigationService.NavigateTo(typeof(ListCollectionViewModel).FullName!, LoadItems);
    }
}
