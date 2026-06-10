using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Dtos
{
    public class UpdatePlaylistRequestDto
    {
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;   
        public string? CoverArtUrl { get; set; } = null;
    }
}