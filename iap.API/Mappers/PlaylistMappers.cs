using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Models;

namespace iap.API.Mappers
{
    public static class PlaylistMapper
    {
        public static PlaylistDto ToPlaylistDto(this Playlist playlist)
        {
            return new PlaylistDto
            {   
                Id = playlist.Id,
                Name = playlist.Name,
                Description = playlist.Description, 
                CoverArtUrl = playlist.CoverArtUrl,
                IsDefault = playlist.IsDefault,
                Type = playlist.Type,
                CreatedAt = playlist.CreatedAt,  
                ParentId = playlist.ParentId,
                UserId = playlist.UserId,
                Tracks = playlist.PlaylistTracks.Select(pt => pt.Track.ToTrackDto()).ToList()
            };
        }

        public static Playlist ToPlaylistFromCreateDto(this CreatePlaylistRequestDto dto)
        {
            return new Playlist
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                CoverArtUrl = dto.CoverArtUrl,
                IsDefault = false,
                CreatedAt = DateTimeOffset.UtcNow,  // server sets this
                // UserId set in controller from JWT token
            };
        }
    }
}