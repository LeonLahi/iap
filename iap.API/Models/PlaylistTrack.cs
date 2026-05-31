using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Models
{
    public class PlaylistTrack
    {
       public int PlaylistId { get; set; }
       public Playlist Playlist  { get; set; } = null!;
       public int TrackId { get; set; }
       public Track Track  { get; set; } = null!;   
       public int Order { get; set; }
    }
}