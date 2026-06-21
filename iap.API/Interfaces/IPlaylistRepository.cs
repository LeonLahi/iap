using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Models;

namespace iap.API.Interfaces
{
    public interface IPlaylistRepository : IASyncRepository<Playlist>
    {
        Task<bool> PlaylistExistsAsync(int id);
        Task<Playlist?> AddTrackAsync(int playlistId, int trackId);
        Task<PlaylistTrack?> GetPlaylistTrackAsync(int playlistId, int trackId);
        // Task<Playlist?> DeleteTrackAsync(PlaylistTrack playlistTrack);
        // Task<Playlist?> UpdateAsync(Playlist playlist);
        // Task<Playlist?> DeleteAsync(Playlist playlist);
        Task<bool> GetByNameAsync(string name);
        // Task<bool> GetByLastDefaultNameAsync();
        Task<string> GetUniqueDefaultNameAsync();
        Task<Playlist?> SoftDeletePlaylistAsync(Playlist playlist);
        Task<List<Playlist>> GetAllChildPlaylists(int ParentId);
        Task<int> GetActiveChildCount(int id);
        Task<List<Playlist>> GetAllDeletedAsync();
        Task<Playlist?> GetByIdDeletedAsync(int id);
    }
}