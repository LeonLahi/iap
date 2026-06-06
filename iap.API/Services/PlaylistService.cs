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
using iap.API.Validators;
using FluentValidation;
using iap.API.Exceptions;

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

        public async Task<PlaylistDto?> CreateAsync(CreatePlaylistRequestDto playlistDto)
        {
            // Set default name if not entered
            if(string.IsNullOrWhiteSpace(playlistDto.Name))
            {
                // Find latest default playlist name, add one to number in name to make unique
                playlistDto.Name = await _playlistRepository.GetUniqueDefaultNameAsync();
            }
            else
            {
                // Check name unique
                if(await _playlistRepository.GetByNameAsync(playlistDto.Name))
                {
                    throw new ConflictException("A playlist with this name already exists");
                }
                
            }
            

            // Get model columns from dto to populate for new object
            var playlistModel = playlistDto.ToPlaylistFromCreateDto();
            playlistModel.UserId = 1;
            playlistModel.CreatedAt = DateTime.UtcNow;
            playlistModel.IsDefault = false;
            playlistModel.IsDeleted = false;

            if(playlistModel == null)
            {
                return null;
            }

            // TODO: Set global query to filter out deleted records

            // Call repo to create playlist object
            await _playlistRepository.CreateAsync(playlistModel);


            return playlistModel.ToPlaylistDto();
            
        }

        public async Task<PlaylistDto?> UpdateAsync(int id, UpdatePlaylistRequestDto updateDto)
        {

            var existingPlaylist = await _playlistRepository.GetByIdAsync(id);

            if(existingPlaylist == null)
            {
                return null;
            }

            // Allow user to update custom details for track to override original details
            // If same EF Core leaves current data 
            // Original fields are populated by metadata or are null if not found
            existingPlaylist.Name = updateDto.Name ?? existingPlaylist.Name;
            existingPlaylist.Description = updateDto.Description;
            existingPlaylist.CoverArtUrl = updateDto.CoverArtUrl;
            existingPlaylist.UpdatedAt = DateTimeOffset.UtcNow;  // server sets this

            var updated = await _playlistRepository.UpdateAsync(existingPlaylist.Id, existingPlaylist);

            return updated?.ToPlaylistDto();
        }

        // public async Task<PlaylistDto?> DeleteAsync(int id)
        // {
        //     // Get playlist from repo
        //     var playlist = await _playlistRepository.GetByIdAsync(id);

        //     if(playlist == null)
        //     {
        //         return null;
        //     }

        //     // TODO: Set global query to filter out deleted records

        //     // Call repo to delete playlist
        //     var updated = await _playlistRepository.DeleteAsync(playlist);
        //     return updated?.ToPlaylistDto();
            
        // }

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