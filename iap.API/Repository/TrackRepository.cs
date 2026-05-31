using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iap.API.Interfaces;
using iap.API.Models;
using iap.API.Data;
using iap.API.Dtos;

namespace iap.API.Repository
{
  public class TrackRepository : AsyncRepository<Track>, ITrackRepository
  {
    public TrackRepository(IapDbContext context) : base(context)
    {
    }

    public override async Task<List<Track>> GetAllAsync()
    {
      return await _context.Tracks.Include(t => t.TrackGenres).ThenInclude(tg => tg.Genre)
      .Include(c => c.Chapters)
      .ToListAsync();
    }

    public override async Task<Track?> GetByIdAsync(int id)
    {
      return await _context.Tracks.Include(t => t.TrackGenres).ThenInclude(tg => tg.Genre).FirstOrDefaultAsync(t => t.Id == id);
    }

    // public async Task<Track?> UpdateTrackAsync(int id, UpdateTrackRequestDto trackDto)
    // {
    //     var existingTrack = await _context.Tracks.FirstOrDefaultAsync(x => x.Id == id);

    //     if(existingTrack == null)
    //     {
    //         return null;
    //     }

    //     existingTrack.Title = trackDto.Title ?? existingTrack.Title;
    //     existingTrack.Artist = trackDto.Artist ?? existingTrack.Artist;
    //     existingTrack.AlbumName = trackDto.AlbumName ?? existingTrack.AlbumName;
    //     existingTrack.CoverArtUrl = trackDto.CoverArtUrl ?? existingTrack.CoverArtUrl;
    //     existingTrack.UpdatedAt = DateTimeOffset.UtcNow;  // server sets this

    //     await _context.SaveChangesAsync();

    //     return existingTrack;
    // }
  }
}