using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Common;
using iap.API.Models;
using iap.API.Services;

namespace iap.API.Interfaces
{
    public interface IListeningSessionService
    {
        Task<Result<IEnumerable<ListeningSessionDto>>> GetAllAsync();
        Task<Result<ListeningSessionDto>> GetByIdAsync(int id);
        Task<Result<IEnumerable<ListeningSessionDto>>> GetRecentlyPlayedAsync();
    }
}