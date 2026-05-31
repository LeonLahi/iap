using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Models
{
    public class ListeningSession
    {
       public int Id { get; set; } 
       public DateTimeOffset PlayedAt { get; set; }
       public int SecondsListened { get; set; }  
       public int UserId { get; set; }
       public User User  { get; set; } = null!;
       public int TrackId { get; set; }
       public Track Track  { get; set; } = null!;        
    }
}