using System;
using System.Collections.Generic;
using System.Text;

namespace GameLauncher.Models.ScreenScraper
{
    public class Response
    {
        public Serveurs serveurs { get; set; }
        public Ssuser ssuser { get; set; }
        public List<Jeux> jeux { get; set; }
        public Jeux jeu { get; set; }
    }
}
