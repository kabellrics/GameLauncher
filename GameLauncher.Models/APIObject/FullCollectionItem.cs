using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.APIObject;
public class FullCollectionItem
{
    public Collection Collection
    {
    get; set;}
    public List<ItemInCollection> Items { get; set;}
}
