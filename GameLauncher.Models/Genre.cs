using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameLauncher.Models;
public class Genre
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    [JsonIgnore]
    public List<ItemGenre>? Items
    {
        get; set;
    }
    public override string ToString() => Name;
}
