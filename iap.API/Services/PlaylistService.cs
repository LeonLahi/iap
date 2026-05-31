using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iap.API.Interfaces;
using iap.API.Models;
using iap.API.Data;
using iap.API.Dtos;
using System.Formats.Tar;
using iap.API.Mappers;

namespace iap.API.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ITrackRepository _trackRepository;

        public PlaylistService(IPlaylistRepository playlistRepository, ITrackRepository trackRepository)
        {
            _playlistRepository = playlistRepository;
            _trackRepository = trackRepository;
        }

        // public async Task<PlaylistDto?> AddTrackAsync(int playlistId, int trackId)
        // {

        //     // Get playlist
        //     var playlist = await _playlistRepository.GetByIdAsync(playlistId);
        //     if (playlist == null)
        //     {
        //         throw new InvalidOperationException("Cannot find playlist.");
        //     }

        //     // Folder cannot have track
        //     if (playlist.Type == PlaylistType.Folder)
        //     {
        //         throw new InvalidOperationException("Cannot add track to a folder playlist.");
        //     }

        //     // Is track already in playlist
        //     if (playlist.PlaylistTracks.Any(pt => pt.TrackId == trackId))
        //     {
        //         throw new InvalidOperationException("Track is already in the playlist.");
        //     }

        //     // Call repo to add track to playlist
        //     var updated = await _playlistRepository.AddTrackAsync(playlistId, trackId);
        //     return updated?.ToPlaylistDto();
            
        // }

        // public async Task<PlaylistDto?> DeleteTrackAsync(int playlistId, int trackId)
        // {
        //     // Call repo to get the track from the playlist
        //     var playlistTrack = await _playlistRepository.GetPlaylistTrackAsync(playlistId, trackId);
        //     // Call repo to delete track from playlist
        //     var updated = await _playlistRepository.DeleteTrackAsync(playlistTrack);
        //     return updated?.ToPlaylistDto();
            
        // }
    }
}