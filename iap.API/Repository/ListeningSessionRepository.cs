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
            .Where(ls => ls.UserId == 1)
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
  }
}