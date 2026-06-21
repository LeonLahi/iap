using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Common;
using iap.API.Models;
using iap.API.Services;

namespace iap.API.Interfaces
{
    public interface IPlaylistService
    {
        // Task<PlaylistDto?> DeleteTrackAsync(int playlistId, int trackId);
        Task<Result<IEnumerable<PlaylistDto>>> GetAllAsync();
        Task<Result<PlaylistDto>> GetByIdAsync(int id);
        Task<Result<IEnumerable<PlaylistDto>>> GetAllDeletedAsync();
        Task<Result<PlaylistDto>> GetByIdDeletedAsync(int id);
        Task<Result<PlaylistDto>> CreateAsync(CreatePlaylistRequestDto playlistDto);
        Task<Result<PlaylistDto>> UpdateAsync(int id, UpdatePlaylistRequestDto playlistDto);
        Task<Result<PlaylistDto>> SoftDeletePlaylistAsync(int id);
        Task<Result<PlaylistDto>> UndoSoftDeletePlaylistAsync(int id);
        Task<Result<PlaylistDeleteImpactDto>> GetDeleteImpactAsync(int id);
        Task<Result<PlaylistDto>> AddTrackAsync(int playlistId, int trackId);
        Task<Result<PlaylistDto>> AddPlaylistToFolderAsync(int folderId, int playlistId);
    }
}