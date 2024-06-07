using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GameLauncher.AdminWPF.Contracts.Services;
using GameLauncher.AdminWPF.Properties;

using MahApps.Metro.Controls;

namespace GameLauncher.AdminWPF.ViewModels;

public class ShellViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private HamburgerMenuItem _selectedMenuItem;
    private RelayCommand _goBackCommand;
    private ICommand _menuItemInvokedCommand;
    private ICommand _loadedCommand;
    private ICommand _unloadedCommand;

    public HamburgerMenuItem SelectedMenuItem
    {
        get { return _selectedMenuItem; }
        set { SetProperty(ref _selectedMenuItem, value); }
    }

    // TODO: Change the icons and titles for all HamburgerMenuItems here.
    public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
    {
        new HamburgerMenuGlyphItem() { Label = Resources.ShellBibliothequePage, Glyph = "\uE8A5", TargetPageType = typeof(BibliothequeViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.ShellStoreImporterPage, Glyph = "\uE8A5", TargetPageType = typeof(StoreImporterViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.ShellEmulatorImporterPage, Glyph = "\uE8A5", TargetPageType = typeof(EmulatorImporterViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.ShellRomImporterPage, Glyph = "\uE8A5", TargetPageType = typeof(RomImporterViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.ShellCollectionPage, Glyph = "\uE8A5", TargetPageType = typeof(CollectionViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.ShellGenrePage, Glyph = "\uE8A5", TargetPageType = typeof(GenreViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.ShellEditeurPage, Glyph = "\uE8A5", TargetPageType = typeof(EditeurViewModel) },
        new HamburgerMenuGlyphItem() { Label = Resources.ShellDevellopeurPage, Glyph = "\uE8A5", TargetPageType = typeof(DevellopeurViewModel) },
    };

    public RelayCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack, CanGoBack));

    public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ?? (_menuItemInvokedCommand = new RelayCommand(OnMenuItemInvoked));

    public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

    public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new RelayCommand(OnUnloaded));

    public ShellViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    private void OnLoaded()
    {
        _navigationService.Navigated += OnNavigated;
    }

    private void OnUnloaded()
    {
        _navigationService.Navigated -= OnNavigated;
    }

    private bool CanGoBack()
        => _navigationService.CanGoBack;

    private void OnGoBack()
        => _navigationService.GoBack();

    private void OnMenuItemInvoked()
        => NavigateTo(SelectedMenuItem.TargetPageType);

    private void NavigateTo(Type targetViewModel)
    {
        if (targetViewModel != null)
        {
            _navigationService.NavigateTo(targetViewModel.FullName);
        }
    }

    private void OnNavigated(object sender, string viewModelName)
    {
        var item = MenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
        if (item != null)
        {
            SelectedMenuItem = item;
        }

        GoBackCommand.NotifyCanExecuteChanged();
    }
}
