using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Models;

namespace iap.API.Interfaces
{
    public interface IPlaylistRepository : IASyncRepository<Playlist>
    {
        // Task<Playlist?> AddTrackAsync(int playlistId, int trackId);
        // Task<PlaylistTrack?> GetPlaylistTrackAsync(int playlistId, int trackId);
        // Task<Playlist?> DeleteTrackAsync(PlaylistTrack playlistTrack);
    }
}