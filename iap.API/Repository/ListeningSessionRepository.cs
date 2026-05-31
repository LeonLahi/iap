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
      return await _context.ListeningSessions.Include(ls => ls.Track).Include(ls => ls.User).ToListAsync();
    }

    public override async Task<ListeningSession?> GetByIdAsync(int id)
    {
      return await _context.ListeningSessions.Include(ls => ls.Track).FirstOrDefaultAsync(t => t.Id == id);
    }
  }
}