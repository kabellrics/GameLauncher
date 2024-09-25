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
        public DbSet<IntroVideo> IntroVideos
        {
            get;set;
        }
        public DbSet<FrontApp> FrontEnds
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
            modelBuilder.Entity<LUPlatformes>(entity =>
            {
                entity.HasKey(e=>e.Id);
            });
            modelBuilder.Entity<LUEmulateur>(entity =>
            {
                entity.HasKey(e=>e.Id);
            });
            modelBuilder.Entity<LUProfile>(entity =>
            {
                entity.HasKey(e=>e.Id);
            });
        }
    }
}
