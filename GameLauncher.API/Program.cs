
using System;
using GameLauncher.DAL;
using GameLauncher.Services.Implementation;
using GameLauncher.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace GameLauncher.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        // Add services to the container.

        builder.Services.AddDbContext<GameLauncherContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<ISteamGameFinderService, SteamGameFinderService>();
        builder.Services.AddScoped<IEAOriginGameFinderService, EAOriginGameFinderService>();
        builder.Services.AddScoped<IEpicGameFinderService, EpicGameFinderService>();
        builder.Services.AddScoped<ISteamGridDbService, SteamGridDbService>();
        builder.Services.AddScoped<IAssetDownloader, AssetDownloader>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameLauncher API", Version = "v1" });

        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

}
