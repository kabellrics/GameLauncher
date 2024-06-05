using System;
using System.Collections.Generic;
using System.Text;

namespace GameLauncher.Services.Utilitaire.SteamGridDB
{

    public class SearchLogoByIdResult
    {
        public bool success { get; set; }
        public List<ImgResult> data { get; set; }
    }
}
