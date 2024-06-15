using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models;
public class ItemDev
{
    public Guid ID
    {
        get; set;
    }
    public Guid DevelloppeurID
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
    public Develloppeur Develloppeur
    {
        get;set;
    }
}
