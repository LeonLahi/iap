using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Dtos
{
    public class CreateTrackGenreRequestDto
    {
       public int TrackId { get; set; } 
       public int GenreId { get; set; } 
    }
}