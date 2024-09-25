using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLauncher.Models.APIObject;

namespace GameLauncher.Models;
public class FrontApp
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
    public string Path
    {
        get; set;
    }
    public bool IsUsed
    {
        get; set;
    }
    public CollectionDisplay CollectionDisplay
    {
        get;set;
    }
    public ItemDisplay ItemDisplay
    {
        get;set;
    }
}
