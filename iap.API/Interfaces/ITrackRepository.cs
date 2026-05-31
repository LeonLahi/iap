using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Models;

namespace iap.API.Interfaces
{
    public interface ITrackRepository : IASyncRepository<Track>
    {
        // Task<List<Track>> GetAllAsync();
        // Task<Track?> GetByIdAsync(int id); // ? as possible that id is not found so will return null
        // Task<Track> CreateAsync(Track trackModel);
        // Task<Track?> UpdateTrackAsync(int id, UpdateTrackRequestDto trackDto);
        // Task<Track?> DeleteAsync(int id);
    }
}