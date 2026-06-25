using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Dtos
{
    public class MostPlayedDto
    {
       public TrackDto Track { get; set; } = null!;
       public int PlayCount { get; set; }
       public int TotalSecondsListened { get; set; }
    }
}