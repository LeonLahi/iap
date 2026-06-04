using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iap.API.Interfaces;
using iap.API.Models;
using iap.API.Data;
using iap.API.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Formats.Tar;

namespace iap.API.Repository
{
  public class PlaylistRepository : AsyncRepository<Playlist>, IPlaylistRepository
  {
    public PlaylistRepository(IapDbContext context) : base(context)
    {
    }

    public override async Task<List<Playlist>> GetAllAsync()
    {
      return await _context.Playlists.Include(pt => pt.PlaylistTracks).ThenInclude(pt => pt.Track).ToListAsync();
    }

    public override async Task<Playlist?> GetByIdAsync(int id)
    {
      return await _context.Playlists.Include(pt => pt.PlaylistTracks).ThenInclude(pt => pt.Track).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> GetByNameAsync(string name)
    {
      return await _context.Playlists.AnyAsync(pt => pt.Name == name);
    }


    // public async Task<Playlist?> UpdateAsync(Playlist playlist)
    // {
    //     _context.Playlists.Update(playlist);
    //     await _context.SaveChangesAsync();

    //     return playlist;
    // }

    // public async Task<Playlist?> DeleteAsync(Playlist playlist)
    // {
    //     playlist.IsDeleted = true;
    //     playlist.DeletedAt = DateTimeOffset.UtcNow;  // server sets this

    //     await _context.SaveChangesAsync();

    //     return playlist;
    // }

    // public async Task<PlaylistTrack?> GetPlaylistTrackAsync (int playlistId, int trackId)
    // {
    //     // Get from join table
    //     return await _context.PlaylistTracks.FirstOrDefaultAsync(x => x.PlaylistId == playlistId && x.TrackId == trackId);
    // }

    // public async Task<Playlist?> AddTrackAsync (int playlistId, int trackId)
    // {
    //   var maxOrder = await _context.PlaylistTracks
    //       .Where(pt => pt.PlaylistId == playlistId)
    //       .MaxAsync(pt => (int?)pt.Order) ?? 0;
      
    //   _context.PlaylistTracks.Add(new PlaylistTrack
    //   {
    //       PlaylistId = playlistId, 
    //       TrackId = trackId,
    //       Order = maxOrder + 1
    //   });

    //   await _context.SaveChangesAsync();

    //   return await GetByIdAsync(playlistId);
    // }

    // public async Task<Playlist?> DeleteTrackAsync (PlaylistTrack playlistTrack)
    // {
    //     int playlistId = playlistTrack.PlaylistId;
    //     _context.PlaylistTracks.Remove(playlistTrack);
    //     await _context.SaveChangesAsync();

    //   return await GetByIdAsync(playlistId);
    // }


  }
}