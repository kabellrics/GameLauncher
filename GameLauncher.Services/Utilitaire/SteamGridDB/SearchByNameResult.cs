using System;
using System.Collections.Generic;
using System.Text;

namespace GameLauncher.Services.Utilitaire.SteamGridDB
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class SearchByNameResult
    {
        public bool success { get; set; }
        public List<DataSearch> data { get; set; }
    }
}
