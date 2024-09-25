using GameLauncher.DAL;
using GameLauncher.Front.Activation;
using GameLauncher.Front.Contracts.Services;
using GameLauncher.Front.Helpers;
using GameLauncher.Front.Services;
using GameLauncher.Front.ViewModels;
using GameLauncher.Front.Views;
using GameLauncher.Services.Implementation.Front;
using GameLauncher.Services.Interface.Front;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using SharpDX;

namespace GameLauncher.Front;

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

        var dbfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameLauncher");
        Directory.CreateDirectory(dbfolder);
        var strcon = Path.Combine(dbfolder, "gamelauncher.db");
        var constr = $"Data Source={strcon}";

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            services.AddDbContext<GameLauncherContext>(options =>
                options.UseSqlite(constr));

            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Services
            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ICollectionService, CollectionService>();
            services.AddSingleton<IStartingService, StartingService>();
            services.AddSingleton<IVideoIntroService, VideoIntroService>();
            services.AddSingleton<IItemsService, ItemsService>();

            // Views and ViewModels
            services.AddSingleton<ItemDetailViewModel>();
            services.AddSingleton<ItemDetailPage>();
            services.AddSingleton<ListCollectionViewModel>();
            services.AddSingleton<ListCollectionPage>();
            services.AddSingleton<SplashScreenViewModel>();
            services.AddSingleton<SplashScreenPage>();

            // Configuration
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
