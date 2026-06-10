using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Models;

namespace iap.API.Dtos
{
    public class CreatePlaylistRequestDto
    {
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }   
        public string? CoverArtUrl { get; set; }  
        public PlaylistType Type {get; set;}
    }
}