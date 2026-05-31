using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Models
{
    public class Chapter
    {
       public int Id { get; set; } 
       public string Title { get; set; } = String.Empty;  
       public int TimestampSeconds { get; set; }  
       public int TrackId { get; set; }
       public Track Track  { get; set; } = null!;
    }
}