using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models;
public class ItemEditeur
{
    public Guid ID
    {
        get; set;
    }
    public Guid EditeurID
    {
        get; set;
    }
    public Guid ItemID
    {
        get; set;
    }
    public Item Item
    {
        get; set;
    }
    public Editeur Editeur
    {
        get;set;
    }
}
