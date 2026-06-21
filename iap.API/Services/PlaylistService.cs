using System;
using System.Data;
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
using Microsoft.AspNetCore.Http.HttpResults;
using iap.API.Common;
using iap.API.Repository;

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

        public async Task<Result<IEnumerable<PlaylistDto>>> GetAllAsync()
        {
            var playlists = await _playlistRepository.GetAllAsync();
            var dtos = playlists.Select(p => p.ToPlaylistDto());
            
            return Result<IEnumerable<PlaylistDto>>.Success(dtos);
        }

        public async Task<Result<PlaylistDto>> GetByIdAsync(int id)
        {
            var playlist = await _playlistRepository.GetByIdAsync(id);

            if (playlist is null)
                return Result<PlaylistDto>.NotFound("Playlist not found");

            return Result<PlaylistDto>.Success(playlist.ToPlaylistDto());
        }

        public async Task<Result<IEnumerable<PlaylistDto>>> GetAllDeletedAsync()
        {
            var playlists = await _playlistRepository.GetAllDeletedAsync();
            var dtos = playlists.Select(p => p.ToPlaylistDto());
            
            return Result<IEnumerable<PlaylistDto>>.Success(dtos);
        }

        public async Task<Result<PlaylistDto>> GetByIdDeletedAsync(int id)
        {
            var playlist = await _playlistRepository.GetByIdDeletedAsync(id);

            if (playlist is null)
                return Result<PlaylistDto>.NotFound("Playlist not found");

            return Result<PlaylistDto>.Success(playlist.ToPlaylistDto());
        }

        public async Task<Result<PlaylistDto>> CreateAsync(CreatePlaylistRequestDto playlistDto)
        {
            // Validate parent id
            if (playlistDto.ParentId.HasValue)
            {
                // Check if can be added to a folder
                if(playlistDto.Type == PlaylistType.Folder)
                    return Result<PlaylistDto>.ValidationError("Cannot place a folder inside another folder.");

                var playlistFolder = await _playlistRepository.GetByIdAsync((int)playlistDto.ParentId);

                if(playlistFolder is null)
                    return Result<PlaylistDto>.NotFound("Folder not found");

                // Check if a folder
                if(playlistFolder.Type != PlaylistType.Folder)
                    return Result<PlaylistDto>.ValidationError("Parent playlist must be of type Folder.");
            }

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
                    return Result<PlaylistDto>.Conflict("A playlist with this name already exists");
                
            }
            

            // Get model columns from dto to populate for new object
            var playlistModel = playlistDto.ToPlaylistFromCreateDto();
            playlistModel.UserId = 1;
            playlistModel.CreatedAt = DateTime.UtcNow;
            playlistModel.IsDefault = false;
            playlistModel.IsDeleted = false;

            // Call repo to create playlist object
            var created = await _playlistRepository.CreateAsync(playlistModel);

            return Result<PlaylistDto>.Success(created.ToPlaylistDto());
            
        }

        public async Task<Result<PlaylistDto>> UpdateAsync(int id, UpdatePlaylistRequestDto updateDto)
        {

            var existingPlaylist = await _playlistRepository.GetByIdAsync(id);

            if(existingPlaylist is null)
                return Result<PlaylistDto>.NotFound("Playlist not found");

            // Allow user to update custom details for track to override original details
            // If same EF Core leaves current data 
            // Original fields are populated by metadata or are null if not found
            existingPlaylist.Name = updateDto.Name ?? existingPlaylist.Name;
            existingPlaylist.Description = updateDto.Description ?? existingPlaylist.Description;
            existingPlaylist.CoverArtUrl = updateDto.CoverArtUrl ?? existingPlaylist.CoverArtUrl;
            existingPlaylist.UpdatedAt = DateTimeOffset.UtcNow;  // server sets this

            await _playlistRepository.SaveAsync();

            return Result<PlaylistDto>.Success(existingPlaylist.ToPlaylistDto());
        }

        public async Task<Result<PlaylistDto>> SoftDeletePlaylistAsync(int id)
        {
            // Get playlist from repo
            var playlist = await _playlistRepository.GetByIdAsync(id);

            if(playlist is null)
                return Result<PlaylistDto>.NotFound("Playlist not found");

            playlist.IsDeleted = true;
            playlist.DeletedAt = DateTimeOffset.UtcNow;  // server sets this

            // Delete children playlists if present
            if(playlist.Type.Equals("Folder") || playlist.Children is not null)
            {

                foreach (var childPlaylist in playlist.Children)
                {
                    childPlaylist.IsDeleted = true;
                    childPlaylist.DeletedAt = playlist.DeletedAt; // Set deleted at to same as parent playlist
                }
            }

            // Call repo to update deleted properties
            await _playlistRepository.SaveAsync();

            return Result<PlaylistDto>.Success(playlist.ToPlaylistDto());
            
        }

        public async Task<Result<PlaylistDeleteImpactDto>> GetDeleteImpactAsync(int id)
        {
            var playlistExists = await _playlistRepository.PlaylistExistsAsync(id);

            if (!playlistExists)
                return Result<PlaylistDeleteImpactDto>.NotFound("Playlist not found");


            int childCount = await _playlistRepository.GetActiveChildCount(id);

            var dto = new PlaylistDeleteImpactDto
            {
                ChildCount = childCount,
                HasChildren = childCount > 0,
                WarningMessage = childCount > 0 
                    ? $"Deleting this folder will automatically delete the {childCount} sub-playlists inside it."
                    : null // Return null if 0 sub-playlists
            };

            return Result<PlaylistDeleteImpactDto>.Success(dto);

        }

        public async Task<Result<PlaylistDto>> UndoSoftDeletePlaylistAsync(int id)
        {
            // Get deleted playlist from repo
            var playlist = await _playlistRepository.GetByIdDeletedAsync(id);

            if(playlist is null)
                return Result<PlaylistDto>.NotFound("Playlist not found");

            // Restore playlist so is not filtered out of playlist queries
            playlist.IsDeleted = false;
            playlist.DeletedAt = null;

            // Restore children playlists if present
            if(playlist.Type == PlaylistType.Folder || playlist.Children is not null)
            {

                foreach (var childPlaylist in playlist.Children)
                {
                    childPlaylist.IsDeleted = false;
                    childPlaylist.DeletedAt = null; // Set deleted at to same as parent playlist
                }
            }

            // Call repo to update deleted properties
            await _playlistRepository.SaveAsync();

            return Result<PlaylistDto>.Success(playlist.ToPlaylistDto());
            
        }

        public async Task<Result<PlaylistDto>> AddTrackAsync(int playlistId, int trackId)
        {

            // Get playlist
            var playlist = await _playlistRepository.GetByIdAsync(playlistId);
            if(playlist is null)
                return Result<PlaylistDto>.NotFound("Playlist not found");

            // Folder cannot have track
            if(playlist.Type == PlaylistType.Folder)
                return Result<PlaylistDto>.ValidationError("Cannot add tracks to a playlist folder.");

            // Check track exists
            if(!await _trackRepository.TrackExistsAsync(trackId))
                return Result<PlaylistDto>.NotFound("Track not found");

            // Is track already in playlist
            if(playlist.PlaylistTracks.Any(pt => pt.TrackId == trackId))
                return Result<PlaylistDto>.Conflict("Track is already in the playlist.");

            // Call repo to add track to playlist
            var updated = await _playlistRepository.AddTrackAsync(playlistId, trackId);
            return Result<PlaylistDto>.Success(updated!.ToPlaylistDto());
            
        }

        public async Task<Result<PlaylistDto>> AddPlaylistToFolderAsync(int folderId, int playlistId)
        {
            // Get folder
            var playlistFolder = await _playlistRepository.GetByIdAsync(folderId);
            if(playlistFolder is null)
                return Result<PlaylistDto>.NotFound("Folder not found");

            // Check if a folder
            if(playlistFolder.Type != PlaylistType.Folder)
                return Result<PlaylistDto>.ValidationError("Parent playlist must be of type Folder.");

            // Get playlist to add
            var playlistToAdd = await _playlistRepository.GetByIdAsync(playlistId);
            if(playlistToAdd is null)
                return Result<PlaylistDto>.NotFound("Playlist not found");

            // Check if can be added to a folder
            if(playlistToAdd.Type == PlaylistType.Folder)
                return Result<PlaylistDto>.ValidationError("Cannot place a folder inside another folder.");

            // Is playlist already in folder
            if(playlistToAdd.ParentId == folderId)
                return Result<PlaylistDto>.Conflict("Playlist is already in the folder.");

            // Assign folderId to parentId of playlist to link
            playlistToAdd.ParentId = folderId;

            // Apply update to parentId
            await _playlistRepository.UpdateAsync(playlistToAdd.Id, playlistToAdd);

            // Display updated folder with new playlist included
            return Result<PlaylistDto>.Success(playlistFolder.ToPlaylistDto());
            
        }

        public async Task<Result<PlaylistDto>> DeleteTrackFromPlaylistAsync(int playlistId, int trackId)
        {
            // Call repo to get the track from the playlist
            var playlistTrack = await _playlistRepository.GetPlaylistTrackAsync(playlistId, trackId);
            if (playlistTrack is null)
                return Result<PlaylistDto>.NotFound("Track not found in playlist.");

            // Call repo to delete track from playlist
            await _playlistRepository.DeleteTrackFromPlaylistAsync(playlistId, trackId);

            // Fetch updated playlist to return
            var updatedPlaylist = await _playlistRepository.GetByIdAsync(playlistId);
            if (updatedPlaylist is null)
                return Result<PlaylistDto>.NotFound("Playlist not found.");

            return Result<PlaylistDto>.Success(updatedPlaylist.ToPlaylistDto());
            
        }
    }
}