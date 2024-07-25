using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.APIObject;
public class FolderRequest
{
    public string Folder
    {
        get; set;
    }
}
public enum MetadataProvider
{
    IGDB,
    Screenscraper
}
public enum LogoProvider
{
    SteamGridDB,
    Screenscraper
}
public enum CoverProvider
{
    SteamGridDB,
    Screenscraper,
    IGDB
}
public enum FanartProvider
{
    IGDB,
    Screenscraper
}
public enum VideoProvider
{
    IGDB,
    Screenscraper
}
