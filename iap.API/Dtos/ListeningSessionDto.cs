using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Dtos
{
    public class ListeningSessionDto
    {
       public int Id { get; set; } 
       public DateTimeOffset PlayedAt { get; set; }
       public int SecondsListened { get; set; }
       public int UserId { get; set; }
       public int TrackId { get; set; } 
       public TrackDto Track { get; set; } = null!;
    }
}