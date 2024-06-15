using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFinder.Common;
using GameFinder.RegistryUtils;
using GameFinder.StoreHandlers.EGS;
using GameLauncher.DAL;
using GameLauncher.Models;
using GameLauncher.Services.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NexusMods.Paths;
using RestSharp;

namespace GameLauncher.Services.Implementation;
public class EpicGameFinderService : IEpicGameFinderService
{
    private readonly GameLauncherContext dbContext;
    private readonly IAssetDownloader assetDownloader;
    private readonly ISteamGridDbService steangriddbService;
    private readonly IDevService devService;
    private readonly IEditeurService editService;
    public EpicGameFinderService(GameLauncherContext dbContext, IAssetDownloader assetDownloader, ISteamGridDbService steangriddbService, IDevService devService, IEditeurService editService)
    {
        this.dbContext = dbContext;
        this.assetDownloader = assetDownloader;
        this.steangriddbService = steangriddbService;
        this.devService = devService;
        this.editService = editService;
    }
    public async Task GetGameAsync()
    {
        var resultlist = new List<Item>();
        var handler = new EGSHandler(WindowsRegistry.Shared, FileSystem.Shared);
        var results = handler.FindAllGames();
        foreach (var result in results)
        {
            if (!dbContext.Items.Any(x => x.StoreId == result.AsT0.CatalogItemId.Value))
            {
                Item item = new Item();
                item.Name = result.AsT0.DisplayName;
                item.SearchName = item.Name;
                item.StoreId = result.AsT0.CatalogItemId.Value;
                item.Platformes = dbContext.Platformes.First(x => x.Name == "Epic Games Store");
                item.LUPlatformesId = item.Platformes.ID;
                item.Path = $"com.epicgames.launcher://apps/{item.StoreId}?action=launch&silent=true";
                item.Logo = string.Empty;
                item.Cover = string.Empty;
                item.Banner = string.Empty;
                item.Artwork = string.Empty;
                item.Video = string.Empty;
                item.Description = string.Empty;
                item.ReleaseDate = DateTime.MinValue;
                item.Develloppeurs = new List<ItemDev>();
                item.Editeurs = new List<ItemEditeur>();
                item.Genres = new List<ItemGenre>();
                dbContext.Items.Add(item);
                await GetEpicData(item);
                dbContext.SaveChanges();
            }
        }
        dbContext.SaveChanges();
    }

    private async Task GetEpicData(Item item)
    {
        string _endpoint = "https://graphql.epicgames.com/graphql";
        RestClient _client;
        _client = new RestClient();
        var query = @"
query searchStoreQuery($allowCountries: String, $category: String, $country: String!, $keywords: String, $locale: String, $sortBy: String, $sortDir: String) {
  Catalog {
    searchStore(
      allowCountries: $allowCountries
      category: $category
      country: $country
      keywords: $keywords
      locale: $locale
      sortBy: $sortBy
      sortDir: $sortDir
    ) {
      elements {
        title
        id
        namespace
        description
        effectiveDate
        keyImages {
          type
          url
        }
        productSlug
        urlSlug
        url
        items {
          id
          namespace
        }
        customAttributes {
          key
          value
        }
        categories {
          path
        }        
      }
    }
  }
}";
        var request = new RestRequest(_endpoint, Method.Post);
        request.AddJsonBody(new
        {
            query,
            variables = new
            {
                keywords = item.SearchName,
                locale = "fr-FR",
                allowCountries = "FR",
                country = "FR",
                sortBy = "title",
                sortDir = "ASC",
                category = "games"
            }
        });

        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            var epicdata = JsonConvert.DeserializeObject<GameLauncher.Models.EpicGame.Response>(response.Content);
            var epicdatagame = epicdata.Data.Catalog.SearchStore.Elements.FirstOrDefault();
            if (epicdatagame != null)
            {
                item.Description = epicdatagame.Description;
                item.ReleaseDate = epicdatagame.EffectiveDate;

                var editor = epicdatagame.CustomAttributes.FirstOrDefault(x => x.Key == "publisherName");
                if (editor != null)
                    item.Editeurs.Add(editService.AddEditeurToItem(editor.Value, item));
                    //item.Editeurs.Add(ExtractEditor(editor, item));
                var dev = epicdatagame.CustomAttributes.FirstOrDefault(x => x.Key == "developerName");
                if (dev != null)
                    item.Develloppeurs.Add(devService.AddDevToItem(dev.Value, item));
                //    item.Develloppeurs.Add(ExtractDev(dev, item));

                var assetfolder = assetDownloader.CreateItemAssetFolder(item.ID);
                var firstlogo = epicdatagame.KeyImages.FirstOrDefault(x => x.Type == "ProductLogo");
                var firstboxart = epicdatagame.KeyImages.FirstOrDefault(x => x.Type == "OfferImageTall");
                var firstartwork = epicdatagame.KeyImages.FirstOrDefault(x => x.Type == "OfferImageWide");
                if (firstlogo != null)
                {
                    assetDownloader.DownloadFile(firstlogo.Url, Path.Combine(assetfolder, "logo.png"));
                    item.Logo = Path.Combine(assetfolder, "logo.png");
                }
                if (firstboxart != null)
                {
                    assetDownloader.DownloadFile(firstboxart.Url, Path.Combine(assetfolder, "cover.jpg"));
                    item.Cover = Path.Combine(assetfolder, "cover.jpg");
                }
                if (firstartwork != null)
                {
                    assetDownloader.DownloadFile(firstartwork.Url, Path.Combine(assetfolder, "artwork.jpg"));
                    item.Artwork = Path.Combine(assetfolder, "artwork.jpg");
                }
            }
        }
        var listgames = steangriddbService.SearchByName(item.Name);
        var firstgame = listgames.FirstOrDefault();
        if (firstgame != null)
        {
            var assetfolder = assetDownloader.CreateItemAssetFolder(item.ID);

            if (string.IsNullOrEmpty(item.Logo))
            {
                var listlogo = steangriddbService.GetLogoForId(firstgame.id);
                var firstlogo = listlogo.FirstOrDefault();
                if (firstlogo != null)
                {
                    assetDownloader.DownloadFile(firstlogo.url, Path.Combine(assetfolder, "logo.png"));
                    item.Logo = Path.Combine(assetfolder, "logo.png");
                }
            }
            if (string.IsNullOrEmpty(item.Cover))
            {
                var listboxart = steangriddbService.GetGridBoxartForId(firstgame.id);
                var firstboxart = listboxart.FirstOrDefault();
                if (firstboxart != null)
                {
                    assetDownloader.DownloadFile(firstboxart.url, Path.Combine(assetfolder, "cover.jpg"));
                    item.Cover = Path.Combine(assetfolder, "cover.jpg");
                }
            }
            if (string.IsNullOrEmpty(item.Banner))
            {
                var listhero = steangriddbService.GetHeroesForId(firstgame.id);
                var firsthero = listhero.FirstOrDefault();
                if (firsthero != null)
                {
                    assetDownloader.DownloadFile(firsthero.url, Path.Combine(assetfolder, "banner.jpg"));
                    item.Banner = Path.Combine(assetfolder, "banner.jpg");
                }
            }
        }
    }
    //private ItemEditeur ExtractEditor(GameLauncher.Models.EpicGame.CustomAttribute data, Item game)
    //{
    //    if (!dbContext.Editeurs.Any(x => x.Name == data.Value))
    //    {
    //        var dbdev = new Editeur { ID = Guid.NewGuid(), Name = data.Value, Items = new List<ItemEditeur>() };
    //        dbContext.Editeurs.Add(dbdev);
    //        var ItemEdit = new ItemEditeur();
    //        ItemEdit.ID = Guid.NewGuid();
    //        ItemEdit.Editeur = dbdev;
    //        ItemEdit.Item = game;
    //        ItemEdit.EditeurID = dbdev.ID;
    //        ItemEdit.ItemID = game.ID;
    //        dbdev.Items.Add(ItemEdit);
    //        dbContext.EditeurdItems.Add(ItemEdit);
    //        return ItemEdit;
    //    }
    //    else
    //    {
    //        var dbdev = dbContext.Editeurs.First(x => x.Name == data.Value);
    //        var ItemEdit = new ItemEditeur();
    //        ItemEdit.ID = Guid.NewGuid();
    //        ItemEdit.Editeur = dbdev;
    //        ItemEdit.Item = game;
    //        ItemEdit.EditeurID = dbdev.ID;
    //        ItemEdit.ItemID = game.ID;
    //        dbdev.Items.Add(ItemEdit);
    //        dbContext.EditeurdItems.Add(ItemEdit);
    //        return ItemEdit;
    //    }
    //}
    //private ItemDev ExtractDev(GameLauncher.Models.EpicGame.CustomAttribute data, Item game)
    //{
    //    if (!dbContext.Editeurs.Any(x => x.Name == data.Value))
    //    {
    //        var dbdev = new Develloppeur { ID = Guid.NewGuid(), Name = data.Value, Items = new List<ItemDev>() };
    //        dbContext.Develloppeurs.Add(dbdev);
    //        var ItemEdit = new ItemDev();
    //        ItemEdit.ID = Guid.NewGuid();
    //        ItemEdit.Develloppeur = dbdev;
    //        ItemEdit.Item = game;
    //        ItemEdit.DevID = dbdev.ID;
    //        ItemEdit.ItemID = game.ID;
    //        dbdev.Items.Add(ItemEdit);
    //        dbContext.DevdItems.Add(ItemEdit);
    //        return ItemEdit;
    //    }
    //    else
    //    {
    //        var dbdev = dbContext.Develloppeurs.First(x => x.Name == data.Value);
    //        var ItemEdit = new ItemDev();
    //        ItemEdit.ID = Guid.NewGuid();
    //        ItemEdit.Develloppeur = dbdev;
    //        ItemEdit.Item = game;
    //        ItemEdit.DevID = dbdev.ID;
    //        ItemEdit.ItemID = game.ID;
    //        dbdev.Items.Add(ItemEdit);
    //        dbContext.DevdItems.Add(ItemEdit);
    //        return ItemEdit;
    //    }
    //}
}
