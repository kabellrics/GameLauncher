using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models;
public class CollectionItem
{
    public Guid ID
    {
        get; set;
    }
    public Guid CollectionID
    {
        get; set;
    }
    public Guid ItemID
    {
        get; set;
    }
    public int Order
    {
        get;set;
    }
    public Collection Collection { get; set; }
    public Item Item { get; set; }
}
