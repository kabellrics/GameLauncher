using GameLauncher.AdminProvider.Interface;
using GameLauncher.AdminProvider;
using GameLauncherAdmin.Activation;
using GameLauncherAdmin.Contracts.Services;
using GameLauncherAdmin.Core.Contracts.Services;
using GameLauncherAdmin.Core.Services;
using GameLauncherAdmin.Helpers;
using GameLauncherAdmin.Models;
using GameLauncherAdmin.Services;
using GameLauncherAdmin.ViewModels;
using GameLauncherAdmin.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

namespace GameLauncherAdmin;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static WindowEx MainWindow { get; } = new MainWindow();

    public static UIElement? AppTitlebar { get; set; }

    public App()
    {
        InitializeComponent();
        //this.AddOtherProvider(new Microsoft.UI.Xaml.XamlTypeInfo.XamlControlsXamlMetaDataProvider());
        //this.AddOtherProvider(new AClassLibrary1.AClassLibrary1_XamlTypeInfo.XamlMetaDataProvider());

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers

            // Services
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Core Services
            services.AddSingleton<ISampleDataService, SampleDataService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IItemProvider, ItemProvider>();
            services.AddSingleton<IMetadataProvider, MetadataProvider>();

            // Views and ViewModels
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<DevelloppeurViewModel>();
            services.AddTransient<DevelloppeurPage>();
            services.AddTransient<EditeurViewModel>();
            services.AddTransient<EditeurPage>();
            services.AddTransient<GenreViewModel>();
            services.AddTransient<GenrePage>();
            services.AddTransient<CollectionViewModel>();
            services.AddTransient<CollectionPage>();
            services.AddTransient<RomImporterViewModel>();
            services.AddTransient<RomImporterPage>();
            services.AddTransient<EmulateurImporterViewModel>();
            services.AddTransient<EmulateurImporterPage>();
            services.AddTransient<StoreImporterViewModel>();
            services.AddTransient<StoreImporterPage>();
            services.AddTransient<BibliothequeDetailViewModel>();
            services.AddTransient<BibliothequeDetailPage>();
            services.AddTransient<BibliothequeViewModel>();
            services.AddTransient<BibliothequePage>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();

            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();

        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        await App.GetService<IActivationService>().ActivateAsync(args);
    }
}
