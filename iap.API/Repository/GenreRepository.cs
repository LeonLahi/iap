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
  public class GenreRepository : AsyncRepository<Genre>, IGenreRepository
  {
    public GenreRepository(IapDbContext context) : base(context)
    {
    }
  }
}