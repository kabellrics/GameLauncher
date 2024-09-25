using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Models.APIObject;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Contracts.ViewModels;

namespace GameLauncherAdmin.ViewModels;

public partial class VideoIntroViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IIntroVideoProvider _introvideoProvider;
    public ObservableCollection<ObservableIntroVideo> Source { get; } = new ObservableCollection<ObservableIntroVideo>();
    private ICommand _refreshCommand;
    public ICommand RefreshCommand
    {
        get
        {
            return _refreshCommand ?? (_refreshCommand = new RelayCommand(Refresh));
        }
    }

    private async void Refresh()
    {
        await LoadData();
    }
    public VideoIntroViewModel(INavigationService navigationService, IIntroVideoProvider introvideoProvider)
    {
        _navigationService = navigationService;
        _introvideoProvider = introvideoProvider;
    }
    public void OnNavigatedFrom()
    {
        foreach (var video in Source) {
            _introvideoProvider.UpdateIntroVideo(video.introVideo);
        }
    }
    public async void OnNavigatedTo(object parameter)
    {
        await LoadData();
    }
    public async Task LoadData()
    {
        Source.Clear();
        var videos = await _introvideoProvider.GetIntroVideo();
        foreach (var video in videos) 
        {
            Source.Add(new ObservableIntroVideo(video));
        }
    }
    public async void AddVideo(string video)
    {
        var FileRequest = new FileRequest();
        FileRequest.SourceFile = video;
        FileRequest.NameFile = Path.GetFileNameWithoutExtension(video);
        await _introvideoProvider.CreateIntroVideo(FileRequest);
        Refresh();
    }
}
