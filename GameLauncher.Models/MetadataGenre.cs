using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models
{
    public class MetadataGenre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID
        {
            get; set;
        }
        public String Name
        {
            get; set;
        }
        public List<Item> Items
        {
            get; set;
        }
        public Guid? GenreId
        {
            get; set;
        }
        public Genre? Genre
        {
            get; set;
        }
    }
}
