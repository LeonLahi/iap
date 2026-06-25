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
    public interface IListeningSessionRepository : IASyncRepository<ListeningSession>
    {
        Task<List<ListeningSession>> GetRecentlyPlayedAsync(int limit = 20);
        Task<List<MostPlayedDto>> GetMostPlayedAsync(int limit = 20);
    }
}