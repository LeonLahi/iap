using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iap.API.Interfaces;
using iap.API.Models;
using iap.API.Data;
using iap.API.Dtos;
using iap.API.Mappers;

namespace iap.API.Repository
{
  public class ListeningSessionRepository : AsyncRepository<ListeningSession>, IListeningSessionRepository
  {
    public ListeningSessionRepository(IapDbContext context) : base(context)
    {
    }

    public override async Task<List<ListeningSession>> GetAllAsync()
    {
      return await _context.ListeningSessions.Include(ls => ls.Track).ToListAsync();
    }

    public override async Task<ListeningSession?> GetByIdAsync(int id)
    {
      return await _context.ListeningSessions
          .Include(ls => ls.Track)
          .FirstOrDefaultAsync(ls => ls.Id == id);
    }

    public async Task<List<ListeningSession>> GetRecentlyPlayedAsync(int limit = 20)
    {
        var sessions = await _context.ListeningSessions
            .Where(ls => ls.UserId == 1) // TODO: Change to get userId from JWT token
            .Include(ls => ls.Track)
            .OrderByDescending(ls => ls.PlayedAt)
            .ToListAsync();

        // Deduplicate in memory to keep only most recent
        // play per track by using GroupBy on TrackId
        return sessions
            .GroupBy(ls => ls.TrackId)
            .Select(g => g.First()) // First() is most recent since ordered by PlayedAt desc
            .OrderByDescending(ls => ls.PlayedAt)
            .Take(limit)
            .ToList();
    }

    public async Task<List<MostPlayedDto>> GetMostPlayedAsync(int limit = 20)
    {
        // Get sessions grouped by track
        var sessions = await _context.ListeningSessions
            .GroupBy(ls => ls.TrackId)
            .OrderByDescending(g => g.Count())
            .Take(limit)
            .Select(g => new 
            {
                // Get track that is being grouped
                TrackEntity = g.Select(ls => ls.Track).FirstOrDefault(),
                PlayCount = g.Count(), // How many of the same track is being grouped
                TotalSecondsListened = g.Sum(ls => ls.SecondsListened) // Sum SecondsListened of each track entity
            })
            .ToListAsync();

        // Map to dtos to return to user
        var dtos = sessions
            .Where(x => x.TrackEntity is not null)
            .Select(x => new MostPlayedDto
            {
                Track = x.TrackEntity!.ToTrackDto(),
                PlayCount = x.PlayCount,
                TotalSecondsListened = x.TotalSecondsListened
            });

        return dtos.ToList();
    }
  }
}