using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.APIObject;
public class StatsObject
{
    public StatsObject() 
    {
        ItemsWithoutArtwork = new();
        ItemsWithoutBanner = new();
        ItemsWithoutCover = new();
        ItemsWithoutDescription = new();
        ItemsWithoutDevelloppeurs = new();
        ItemsWithoutEditeurs = new();
        ItemsWithoutGenres = new();
        ItemsWithoutLogo = new();
        ItemsWithoutReleaseDate= new();
        ItemsWithoutVideo = new();
        CollectionsWithoutArtwork = new();
        CollectionsWithoutLogo = new();
    }
    public List<Item> ItemsWithoutDescription
    {
        get;set;
    }
    public List<Item> ItemsWithoutReleaseDate
    {
        get;set;
    }
    public List<Item> ItemsWithoutDevelloppeurs
    {
        get;set;
    }
    public List<Item> ItemsWithoutEditeurs
    {
        get;set;
    }
    public List<Item> ItemsWithoutGenres
    {
        get;set;
    }
    public List<Item> ItemsWithoutCover
    {
        get;set;
    }
    public List<Item> ItemsWithoutLogo
    {
        get;set;
    }
    public List<Item> ItemsWithoutBanner
    {
        get;set;
    }
    public List<Item> ItemsWithoutArtwork
    {
        get;set;
    }
    public List<Item> ItemsWithoutVideo
    {
        get;set;
    }
    public List<Collection> CollectionsWithoutLogo
    {
        get;set;
    }
    public List<Collection> CollectionsWithoutArtwork
    {
        get;set;
    }
}
