using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Models;

namespace iap.API.Interfaces
{
    public interface IASyncRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id); // ? as possible that id is not found so will return null
        // Task<T> CreateAsync(T entity);
        // Task<T?> DeleteAsync(int id);
    }
}