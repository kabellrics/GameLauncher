using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models;
public class Collection
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
    public string CodeName
    {
        get; set;
    }
    public string Fanart
    {
        get; set;
    }
    public string Logo
    {
        get; set;
    }
    public int Order
    {
        get; set;
    }
    public List<CollectionItem>? Items
    {
        get; set;
    }
}
