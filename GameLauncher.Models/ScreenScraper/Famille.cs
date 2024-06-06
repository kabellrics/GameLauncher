using System.Collections.Generic;

namespace GameLauncher.Models.ScreenScraper
{
    public class Famille
    {
        public string id { get; set; }
        public string nomcourt { get; set; }
        public string principale { get; set; }
        public string parentid { get; set; }
        public List<Nom> noms { get; set; }
    }
}
