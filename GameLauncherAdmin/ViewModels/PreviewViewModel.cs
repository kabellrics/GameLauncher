using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Models.APIObject;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Contracts.ViewModels;

namespace GameLauncherAdmin.ViewModels;

public partial class PreviewViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ICollectionProvider _collecProvider;
    public ObservableCollection<FullCollectionItem> Source { get; } = new ObservableCollection<FullCollectionItem>();
    public PreviewViewModel(INavigationService navigationService, ICollectionProvider collecProvider)
    {
        _navigationService = navigationService;
        _collecProvider = collecProvider;
    }
    public void OnNavigatedFrom()
    {
    }
    public async void OnNavigatedTo(object parameter)
    {
        await GetAllCollectionAsync();
    }

    private async Task GetAllCollectionAsync()
    {
        Source.Clear();
        var collec = await _collecProvider.GetFullCollection();
        foreach (var col in collec) { Source.Add(col); }
    }
}
