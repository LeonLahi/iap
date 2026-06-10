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
using System.Globalization;

namespace iap.API.Repository
{
  public class PlaylistRepository : AsyncRepository<Playlist>, IPlaylistRepository
  {
    public PlaylistRepository(IapDbContext context) : base(context)
    {
    }

    public override async Task<List<Playlist>> GetAllAsync()
    {
      return await _context.Playlists.Include(pt => pt.PlaylistTracks).ThenInclude(pt => pt.Track)
                                      // Fetch children playlist and each track
                                     .Include(c => c.Children).ThenInclude(c => c.PlaylistTracks).ThenInclude(ct => ct.Track)
                                     .ToListAsync();
    }

    public override async Task<Playlist?> GetByIdAsync(int id)
    {
      return await _context.Playlists.Include(pt => pt.PlaylistTracks).ThenInclude(pt => pt.Track)
                                      // Fetch children playlist and each track
                                     .Include(c => c.Children).ThenInclude(c => c.PlaylistTracks).ThenInclude(ct => ct.Track)
                                     .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Playlist>> GetAllDeletedAsync()
    {
      return await _context.Playlists.IgnoreQueryFilters()
                                     .Include(pt => pt.PlaylistTracks).ThenInclude(pt => pt.Track)
                                      // Fetch children playlist and each track
                                     .Include(c => c.Children).ThenInclude(c => c.PlaylistTracks).ThenInclude(ct => ct.Track)
                                     .Where(p => p.IsDeleted)
                                     .ToListAsync();
    }

    public async Task<Playlist?> GetByIdDeletedAsync(int id)
    {
      return await _context.Playlists.IgnoreQueryFilters()
                                     .Include(pt => pt.PlaylistTracks).ThenInclude(pt => pt.Track)
                                      // Fetch children playlist and each track
                                     .Include(c => c.Children).ThenInclude(c => c.PlaylistTracks).ThenInclude(ct => ct.Track)
                                     .Where(p => p.IsDeleted)
                                     .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Playlist>> GetAllChildPlaylists(int ParentId)
    {
      // Select all playlists that contain parent id 
      return await _context.Playlists.Where(p => p.ParentId == ParentId && !p.IsDeleted)
                                                              .ToListAsync();
    }

    public async Task<int> GetActiveChildCount(int id)
    {
      // Get quantity of playlists that are in a specific playlist folder
      return await _context.Playlists.Where(p => p.ParentId == id && !p.IsDeleted)
                                      .CountAsync();
                                    
    }

    public async Task<bool> GetByNameAsync(string name)
    {
      return await _context.Playlists.AnyAsync(pt => pt.Name == name);
    }

    public async Task<string> GetUniqueDefaultNameAsync()
    {
      // TODO: Implement user login functionality and remove hardcoded user id
      var tempUserId = 1;

      var defaultNames = await _context.Playlists
                                .Where(p => p.UserId == tempUserId && p.Name.StartsWith("New Playlist"))
                                .Select(p => p.Name)
                                .ToListAsync();

      // Trim to get numbers
      var numbers = defaultNames.Select(n => int.Parse(n.Split("#")[1]))
                                          .ToList();

      // Find largest number to get next default name
      int maxNumber = numbers.Any() ? numbers.Max() : 0; 
      int nextNumber = maxNumber + 1;

      return $"New Playlist #{nextNumber}";
    }



    // public async Task<Playlist?> UpdateAsync(Playlist playlist)
    // {
    //     _context.Playlists.Update(playlist);
    //     await _context.SaveChangesAsync();

    //     return playlist;
    // }

    public async Task<Playlist?> SoftDeletePlaylistAsync(Playlist playlist)
    {
        await _context.SaveChangesAsync();

        return playlist;
    }

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