using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Models;

namespace iap.API.Dtos
{
    public class PlaylistDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } = String.Empty;  
        public string? Description { get; set; }   
        public string? CoverArtUrl { get; set; }
        public bool IsDefault { get; set; }
        public PlaylistType Type { get; set; }   
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public int? ParentId { get; set; }
        public int UserId { get; set; }
        public List<PlaylistDto>? Children { get; set; } = new List<PlaylistDto>();
        public List<TrackDto> Tracks { get; set; } = new List<TrackDto>();

    }
}