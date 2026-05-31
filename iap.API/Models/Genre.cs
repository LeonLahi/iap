using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public ICollection<TrackGenre> TrackGenres { get; set; } = new List<TrackGenre>();
    }
}