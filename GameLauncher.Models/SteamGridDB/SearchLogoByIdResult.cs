﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameLauncher.Models.SteamGridDB
{

    public class SearchLogoByIdResult
    {
        public bool success { get; set; }
        public List<ImgResult> data { get; set; }
    }
}
