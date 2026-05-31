using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Models
{
    public class TrackGenre
    {
       public int TrackId { get; set; }
       public Track Track  { get; set; } = null!; 

       public int GenreId { get; set; }
       public Genre Genre  { get; set; } = null!; 
    }
}