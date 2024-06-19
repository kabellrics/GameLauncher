using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Models.APIObject;
public class UpdateGenreMessage
{
    public Item Item
    {
        get;set;
    }
    public List<Genre> newGenres
    {
        get;set;
    }
}
