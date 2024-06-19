using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.APIObject;
public class UpdateEditeurMessage
{
    public Item Item
    {
        get; set;
    }
    public List<Editeur> newEditeurs
    {
        get; set;
    }
}
