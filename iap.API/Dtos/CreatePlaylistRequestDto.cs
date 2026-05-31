using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Dtos
{
    public class CreatePlaylistRequestDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }   
        public string? CoverArtUrl { get; set; }  
        public bool IsDefault { get; set; }  
        public DateTimeOffset CreatedAt { get; set; } 
    }
}