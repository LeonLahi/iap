using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Dtos
{
    public class TrackDto
    {
       public int Id { get; set; } 
       public string Title { get; set; } = String.Empty;  
       public string? Artist { get; set; }    
       public string? AlbumName { get; set; }
       public int DurationSeconds { get; set; }
       public string BlobUrl { get; set; } = String.Empty;
       public string? CoverArtUrl { get; set; }
       public DateTimeOffset UploadedAt { get; set; }
       public DateTimeOffset? UpdatedAt { get; set; }
       public string? OriginalTitle { get; set; }
       public string? OriginalArtist { get; set; }
       public string? OriginalAlbumName { get; set; }
       public string? OriginalCoverArtUrl { get; set; }
       public int UserId { get; set; }
       public List<GenreDto> Genres { get; set; } = new List<GenreDto>();
       public List<ChapterDto>? Chapters { get; set; } = new List<ChapterDto>();
    }
}