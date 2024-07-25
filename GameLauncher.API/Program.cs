
using GameLauncher.DAL;
using GameLauncher.Services.Implementation;
using GameLauncher.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace GameLauncher.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        // Add services to the container.
        builder.Services.AddSignalR();

        builder.Services.AddDbContext<GameLauncherContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<ISteamGameFinderService, SteamGameFinderService>();
        builder.Services.AddScoped<IEAOriginGameFinderService, EAOriginGameFinderService>();
        builder.Services.AddScoped<IEpicGameFinderService, EpicGameFinderService>();
        builder.Services.AddScoped<ISteamGridDbService, SteamGridDbService>();
        builder.Services.AddScoped<IScreenscraperService, ScreenscraperService>();
        builder.Services.AddScoped<IIGDBService, IGDBService>();
        builder.Services.AddScoped<IItemsService, ItemsService>();
        builder.Services.AddScoped<IPlateformeService, PlateformeService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<IEditeurService, EditeurService>();
        builder.Services.AddScoped<IDevService, DevService>();
        builder.Services.AddScoped<ICollectionService, CollectionService>();
        builder.Services.AddScoped<IAssetDownloader, AssetDownloader>();
        //builder.Services.AddScoped<INotificationService, NotificationService>();
        //builder.Services.AddScoped<INotificationService, SignalRNotificationHub>();
        builder.Services.AddScoped<IStartingService, StartingService>();
        builder.Services.AddScoped<IEmulateurService, EmulateurService>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameLauncher API", Version = "v1" });

        });
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<GameLauncherContext>();
            GameLauncherDBInitializer.Iniatialize(context);
        }
        app.UseCors("CorsPolicy");
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();

        app.UseAuthorization();
        //app.MapHub<NotificationService>("/notif");
        app.MapHub<SignalRNotificationHub>("/notif");

        app.MapControllers();
        //app.UseEndpoints(endpoints => {
        //    endpoints.MapControllers();
        //    endpoints.MapHub<SignalRNotificationHub>("/notif");
        //});
        app.Run();
    }

}
