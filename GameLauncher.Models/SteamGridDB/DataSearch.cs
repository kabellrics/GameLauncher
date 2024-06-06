using System;
using System.Collections.Generic;
using System.Text;

namespace GameLauncher.Models.SteamGridDB
{
    public class DataSearch
    {
        public int id
        {
            get; set;
        }
        public string name
        {
            get; set;
        }
        public int release_date { get; set; }
        public List<string> types { get; set; }
        public bool verified { get; set; }

        public override string ToString()
        {
            try
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(release_date);
                DateTime date = dateTimeOffset.DateTime;
                return $"{name} - ({date.Year})";
            }
            catch (Exception ex)
            {
                return $"{name}";
            }
        }
    }
}
