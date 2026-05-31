using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Models;

namespace iap.API.Mappers
{
    public static class TrackMapper
    {
        public static TrackDto ToTrackDto(this Track track)
        {
            return new TrackDto
            {
                Id = track.Id,
                Title = track.Title,
                Artist = track.Artist,
                AlbumName = track.AlbumName,
                DurationSeconds = track.DurationSeconds,
                BlobUrl = track.BlobUrl,
                CoverArtUrl = track.CoverArtUrl,
                UploadedAt = track.UploadedAt,
                UpdatedAt = track.UpdatedAt,
                OriginalTitle = track.OriginalTitle,
                OriginalArtist = track.OriginalArtist,
                OriginalAlbumName = track.OriginalAlbumName,
                OriginalCoverArtUrl = track.OriginalCoverArtUrl,
                UserId = track.UserId,
                Genres = track.TrackGenres.Select(tg => tg.Genre.ToGenreDto()).ToList(),
                Chapters = track.Chapters.Select(c => c.ToChapterDto()).ToList()
            };
        }

        public static Track ToTrackFromCreateDto(this CreateTrackRequestDto dto)
        {
            return new Track
            {
                Title = dto.Title,
                Artist = dto.Artist,
                AlbumName = dto.AlbumName,
                DurationSeconds = dto.DurationSeconds,
                BlobUrl = dto.BlobUrl,
                CoverArtUrl = dto.CoverArtUrl,
                UploadedAt = DateTimeOffset.UtcNow,  // server sets this
                OriginalTitle = dto.Title,            // server copies to original on create
                OriginalArtist = dto.Artist,
                OriginalAlbumName = dto.AlbumName,
                OriginalCoverArtUrl = dto.CoverArtUrl
                // UserId set in controller from JWT token
            };
        }
    }
}