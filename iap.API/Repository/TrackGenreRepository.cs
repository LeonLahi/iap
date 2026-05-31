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
  public class TrackGenreRepository : AsyncRepository<TrackGenre>, ITrackGenreRepository
  {
    public TrackGenreRepository(IapDbContext context) : base(context)
    {
    }
  }
}