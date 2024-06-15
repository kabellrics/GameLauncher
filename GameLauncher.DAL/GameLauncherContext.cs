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
        public DbSet<ItemEditeur> EditeurdItems
        {
            get;set;
        }
        public DbSet<ItemDev> DevdItems
        {
            get;set;
        }
        public DbSet<ItemGenre> GenredItems
        {
            get;set;
        }
        public DbSet<CollectionItem> CollectiondItems
        {
            get;set;
        }

        public GameLauncherContext(DbContextOptions<GameLauncherContext> options)
        : base(options)
        {
        }

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
