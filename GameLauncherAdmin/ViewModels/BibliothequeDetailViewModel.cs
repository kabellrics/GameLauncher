﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.UI;
using GameLauncher.AdminProvider.Interface;
using GameLauncher.Models;
using GameLauncher.ObservableObjet;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Contracts.ViewModels;
using GameLauncherAdmin.Core.Contracts.Services;
using GameLauncherAdmin.Core.Models;
using Microsoft.UI.Xaml;

namespace GameLauncherAdmin.ViewModels;

public partial class BibliothequeDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;
    private readonly IMetadataProvider _MetadataService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private bool _showMetadataSourceExpander;
    [ObservableProperty]
    private bool _showGameProposalExpander;
    [ObservableProperty]
    private bool _showDataProposalExpander;
    [ObservableProperty]
    private Visibility _visibilityMetadataSourceExpander;
    [ObservableProperty]
    private Visibility _visibilityGameProposalExpander;
    [ObservableProperty]
    private Visibility _visibilityDataProposalExpander;

    [ObservableProperty]
    private int _flipMetadataIndex;

    [ObservableProperty]
    private List<string> _metadataType = new List<string> { "IGDB", "Screenscraper" };
    [ObservableProperty]
    private string _metadataTypechoice;

    [ObservableProperty]
    private List<string> _mediaType = new List<string> { "IGDB", "Screenscraper", "SteamGridDB" };

    [ObservableProperty]
    private string _mediaTypechoice;

    [ObservableProperty]
    private string _reconcileNewName;
    [ObservableProperty]
    private string _reconcileNewDateTime;
    [ObservableProperty]
    private string _reconcileDescription;
    [ObservableProperty]
    private ObservableCollection<ObservableEditeur> _reconcileEditeur;
    [ObservableProperty]
    private ObservableCollection<ObservableDevelloppeur> _reconcileDevs;
    [ObservableProperty]
    private ObservableCollection<ObservableGenre> _reconcileGenre;

    [ObservableProperty]
    private int intgameToReconcile;

    [ObservableProperty]
    private ObservableItem? item;

    public ObservableItem GameToReconcile;

    public ObservableCollection<ObservableItem> SearchMetadata
    {
        get; private set;
    }

    private ICommand _cancelCommand;
    private ICommand _fullcancelCommand;
    private ICommand _chooseSourceCommand;
    private ICommand _chooseGameCommand;
    private ICommand _chooseReconcileCommand;

    public ICommand ChooseSourceCommand
    {
        get
        {
            return _chooseSourceCommand ?? (_chooseSourceCommand = new RelayCommand(GetMetadataChoice));
        }
    }
    public ICommand CancelMetadataCommand
    {
        get
        {
            return _cancelCommand ?? (_cancelCommand = new RelayCommand(CancelMetaChoice));
        }
    }
    public ICommand ChooseGameCommand
    {
        get
        {
            return _chooseGameCommand ?? (_chooseGameCommand = new RelayCommand(ChooseMetadataGame));
        }
    }
    public ICommand ChooseReconcileCommand
    {
        get
        {
            return _chooseReconcileCommand ?? (_chooseReconcileCommand = new RelayCommand(GetAllMetadata));
        }
    }
    public ICommand FullCancelCommand
    {
        get
        {
            return _fullcancelCommand ?? (_fullcancelCommand = new RelayCommand(GoBackToList));
        }
    }

    public BibliothequeDetailViewModel(INavigationService navigationService, ISampleDataService sampleDataService, IMetadataProvider metadataProvider)
    {
        _navigationService = navigationService;
        _sampleDataService = sampleDataService;
        _MetadataService = metadataProvider;
        IntgameToReconcile = 0;
        ShowMetadataSourceExpander = false;
        ShowGameProposalExpander = false;
        ShowDataProposalExpander = false;
        VisibilityMetadataSourceExpander = Visibility.Visible;
        VisibilityGameProposalExpander = Visibility.Collapsed;
        VisibilityDataProposalExpander = Visibility.Collapsed;
        FlipMetadataIndex = 0;
        SearchMetadata = new ObservableCollection<ObservableItem>();
        ReconcileEditeur = new ObservableCollection<ObservableEditeur>();
        ReconcileDevs = new ObservableCollection<ObservableDevelloppeur>();
        ReconcileGenre = new ObservableCollection<ObservableGenre>();
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is ObservableItem detailitem)
        {
            //var data = await _sampleDataService.GetContentGridDataAsync();
            //Item = data.First(i => i.OrderID == orderID);
            Item = detailitem;
        }
    }
    private void GetAllMetadata()
    {
        Item.Name = ReconcileNewName;
        Item.ReleaseDate = DateTime.Parse(ReconcileNewDateTime);
        Item.Description = ReconcileDescription;
        Item.Editeurs.Clear();
        Item.Develloppeurs.Clear();
        Item.Genres.Clear();
        foreach(var item in ReconcileEditeur) { Item.Editeurs.Add(item); }
        foreach (var item in ReconcileDevs) { Item.Develloppeurs.Add(item); }
        foreach (var item in ReconcileGenre) { Item.Genres.Add(item); }
        CancelMetaChoice();
    }
    private void ChooseMetadataGame()
    {
        ReconcileNewName = GameToReconcile.Name;
        ReconcileNewDateTime = GameToReconcile.ReleaseDate.ToString("dd/MM/yyyy");
        ReconcileDescription = GameToReconcile.Description;
        ReconcileEditeur = GameToReconcile.Editeurs;
        ReconcileDevs = GameToReconcile.Develloppeurs;
        ReconcileGenre = GameToReconcile.Genres;
        ShowMetadataSourceExpander = false;
        ShowGameProposalExpander = false;
        ShowDataProposalExpander = true;
        VisibilityMetadataSourceExpander = Visibility.Collapsed;
        VisibilityGameProposalExpander = Visibility.Collapsed;
        VisibilityDataProposalExpander = Visibility.Visible;
    }
    private void CancelMetaChoice()
    {
        ShowMetadataSourceExpander = false;
        ShowGameProposalExpander = false;
        ShowDataProposalExpander = false;
        VisibilityMetadataSourceExpander = Visibility.Visible;
        VisibilityGameProposalExpander = Visibility.Collapsed;
        VisibilityDataProposalExpander = Visibility.Collapsed;
        FlipMetadataIndex = 0;
        SearchMetadata.Clear();
        ReconcileNewName = string.Empty;
        ReconcileNewDateTime = string.Empty;
        ReconcileDescription = string.Empty;
        ReconcileEditeur.Clear();
        ReconcileDevs.Clear();
        ReconcileGenre.Clear();
    }
    private void GoBackToList()
    {
        _navigationService.GoBack();
    }
    public async void GetMetadataChoice()
    {
        ShowMetadataSourceExpander = false;
        ShowGameProposalExpander = true;
        ShowDataProposalExpander = false;
        VisibilityMetadataSourceExpander = Visibility.Collapsed;
        VisibilityGameProposalExpander = Visibility.Visible;
        VisibilityDataProposalExpander = Visibility.Collapsed;
        SearchMetadata.Clear();
        if (MetadataTypechoice == "IGDB")
        {
            await InitializeGameChoose(_MetadataService.GetIGDBGameByNameAsync(Item.SearchName));
        }

        if (MetadataTypechoice == "Screenscraper")
        {
            await InitializeGameChoose(_MetadataService.SearchScreenscraperGameByNameAsync(Item.SearchName));
        }
    }
    private async Task InitializeGameChoose(IAsyncEnumerable<ObservableItem> asyncitems)
    {
        var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        await Task.Run(async () =>
        {
            await foreach (var item in asyncitems)
            {
                dispatcherQueue.TryEnqueue(() =>
                {
                    SearchMetadata.Add(item);
                });
            }
        });
    }
    public void OnNavigatedFrom()
    {
    }
}
