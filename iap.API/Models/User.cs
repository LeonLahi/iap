using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Models
{
    public class User
    {
       public int Id { get; set; } 
       public string Username { get; set; } = String.Empty;
       public string? DisplayName { get; set; }
       public string Email { get; set; } = String.Empty;
       public string PasswordHash { get; set; } = String.Empty;
       public DateTimeOffset CreatedAt { get; set; }
       public ICollection<Track> Tracks { get; set; } = new List<Track>();
       public ICollection<ListeningSession> ListeningSessions { get; set; } = new List<ListeningSession>();
       public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    }
}