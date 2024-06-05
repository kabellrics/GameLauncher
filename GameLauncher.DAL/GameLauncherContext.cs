using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLauncher.DAL
{
    public class GameLauncherContext : DbContext
    {
        public DbSet<Genre> Genres
        {
            get; set;
        }
        public DbSet<MetadataGenre> MetadataGenres
        {
            get; set;
        }
        public DbSet<LUProfile> Profiles
        {
        get; set; }
        public DbSet<LUEmulateur> Emulateurs
        {
        get; set; }
        public DbSet<LUPlatformes> Platformes
        {get; set;
        }
        public DbSet<Item> Items
        {
        get; set; }
        public DbSet<Editeur> Editeurs
        {
        get; set; }
        public DbSet<Develloppeur> Develloppeurs
        {
        get; set; }
        public DbSet<Collection> Collections
        {
            get;set;
        }
        //public string DbPath
        //{
        //    get;
        //}

        public GameLauncherContext(DbContextOptions<GameLauncherContext> options)
        : base(options)
        {
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = System.IO.Path.Join(path, "gamelauncher.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Code to seed data
            modelBuilder.Entity<LUPlatformes>().HasData(
                new LUPlatformes { ID = Guid.NewGuid(), Name = "Steam", CodeName = "Steam", Databases=string.Empty },
                new LUPlatformes { ID = Guid.NewGuid(), Name = "Epic Games Store", CodeName = "Epic", Databases = string.Empty },
                new LUPlatformes { ID = Guid.NewGuid(), Name = "EA Origin", CodeName = "EA Play", Databases = string.Empty }
                );
        }
    }
}
