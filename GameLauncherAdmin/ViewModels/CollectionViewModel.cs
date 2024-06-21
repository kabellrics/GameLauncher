using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GameLauncher.AdminProvider;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Contracts.ViewModels;

namespace GameLauncherAdmin.ViewModels;

public partial class CollectionViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ICollectionProvider _collecProvider;
    public ObservableCollection<ObsCollection> Source { get; } = new ObservableCollection<ObsCollection>();
    public CollectionViewModel(INavigationService navigationService, ICollectionProvider collecProvider)
    {
        _navigationService = navigationService;
        _collecProvider = collecProvider;
    }
    public void OnNavigatedFrom()
    {
    }
    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();
        var collecs = await _collecProvider.GetCollectionsAsync();
        foreach (var item in collecs)
        {
            Source.Add(item);
        }
    }
}
