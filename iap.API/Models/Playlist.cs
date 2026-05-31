using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Models
{
    public class Playlist
    {
        public int Id { get; set; } 
        public string Name { get; set; } = String.Empty;  
        public string? Description { get; set; }
        public PlaylistType Type { get; set; }      
        public string? CoverArtUrl { get; set; }
        public bool IsDefault { get; set; }  
        public DateTimeOffset CreatedAt { get; set; }   
        public int? ParentId { get; set; }
        public Playlist? Parent  { get; set; } = null!;
        public ICollection<Playlist> Children { get; set; } = new List<Playlist>();
        public int UserId { get; set; }
        public User User  { get; set; } = null!;
        public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();
       
    }
}