using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace iap.API.Models
{
    public class User : IdentityUser<int>
    {
       public string? DisplayName { get; set; }
       public string Role { get; set; } = "User";
       public DateTimeOffset CreatedAt { get; set; }
       public bool IsDeleted { get; set; }
       public DateTimeOffset? DeletedAt { get; set; }       
       public ICollection<Track> Tracks { get; set; } = new List<Track>();
       public ICollection<ListeningSession> ListeningSessions { get; set; } = new List<ListeningSession>();
       public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    }
}