using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Dtos
{
    public class UpdatePlaylistRequestDto
    {
        public string? Name { get; set; } = String.Empty;
        public string? Description { get; set; }   
        public string? CoverArtUrl { get; set; }
        // public DateTimeOffset? UpdatedAt { get; set; }
    }
}