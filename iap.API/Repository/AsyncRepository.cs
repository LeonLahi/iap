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
  public class AsyncRepository<T> : IASyncRepository<T> where T : class
  {
    protected readonly IapDbContext _context;
    public AsyncRepository(IapDbContext context)
    {
        _context = context;
    }

    public async Task<T> CreateAsync(T entity)
    {
      await _context.Set<T>().AddAsync(entity);
      await _context.SaveChangesAsync();
      return entity;

    }

    public async Task<T?> DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);

        if(entity == null)
        {
            return null;
        }

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
      // TODO: Set global query to filter out deleted records
      return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
      // TODO: Set global query to filter out deleted records
      return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T?> UpdateAsync(int id, T entity)
    {
        var existing = await _context.Set<T>().FindAsync(id);

        if(existing == null)
        {
            return null;
        }

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();

        return existing;
    }

    public async Task SaveAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}