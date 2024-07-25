using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.APIObject;
public class ScanProfile
{
    public string FolderPath{get;set;}
    public LUProfile Profile
    {
        get; set;
    }
    public LUPlatformes Platforms
    {
        get; set;
    }
    public MetadataProvider MetaProvider
    {
        get; set;
    }
    public LogoProvider LogoProvider
    {
        get; set;
    }
    public CoverProvider CoverProvider
    {
        get; set;
    }
    public FanartProvider FanartProvider
    {
        get; set;
    }
    public VideoProvider VideoProvider
    {
        get; set;
    }
}
