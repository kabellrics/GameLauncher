using CommunityToolkit.Mvvm.ComponentModel;

using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.ViewModels;
using GameLauncherAdmin.Views;

using Microsoft.UI.Xaml.Controls;

namespace GameLauncherAdmin.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    public PageService()
    {
        Configure<MainViewModel, MainPage>();
        Configure<BibliothequeViewModel, BibliothequePage>();
        Configure<BibliothequeDetailViewModel, BibliothequeDetailPage>();
        Configure<StoreImporterViewModel, StoreImporterPage>();
        Configure<EmulateurImporterViewModel, EmulateurImporterPage>();
        Configure<RomImporterViewModel, RomImporterPage>();
        Configure<CollectionViewModel, CollectionPage>();
        Configure<GenreViewModel, GenrePage>();
        Configure<EditeurViewModel, EditeurPage>();
        Configure<DevelloppeurViewModel, DevelloppeurPage>();
        Configure<SettingsViewModel, SettingsPage>();
        Configure<CollectionDetailViewModel, CollectionDetailPage>();
        Configure<VideoIntroViewModel, VideoIntroPage>();
    }

    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Page
    {
        lock (_pages)
        {
            var key = typeof(VM).FullName!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(V);
            if (_pages.ContainsValue(type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}
