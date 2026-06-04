using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Models;
using iap.API.Services;

namespace iap.API.Interfaces
{
    public interface IPlaylistService
    {
        // Task<PlaylistDto?> AddTrackAsync(int playlistId, int trackId);
        // Task<PlaylistDto?> DeleteTrackAsync(int playlistId, int trackId);
        Task<PlaylistDto?> CreateAsync(CreatePlaylistRequestDto playlistDto);
        // Task<PlaylistDto?> UpdateAsync(int id, UpdatePlaylistRequestDto playlistDto);
        // Task<PlaylistDto?> DeleteAsync(int id);
    }
}