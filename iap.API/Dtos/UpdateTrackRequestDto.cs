using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Dtos
{
    public class UpdateTrackRequestDto
    {
       public string Title { get; set; } = String.Empty;  
       public string? Artist { get; set; }    
       public string? AlbumName { get; set; }
       public string? CoverArtUrl { get; set; }     
    }
}