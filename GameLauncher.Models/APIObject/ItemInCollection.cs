using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.APIObject;
public class ItemInCollection
{
    public Item Item { get; set; }
    public CollectionItem CollectionItem { get; set; }
}public class TrueItemInCollection
{
    public ItemCompleteInfo Item { get; set; }
    public CollectionItem CollectionItem { get; set; }
}
