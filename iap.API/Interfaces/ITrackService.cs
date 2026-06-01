using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Models;
using iap.API.Services;

namespace iap.API.Interfaces
{
    public interface ITrackService
    {
        Task<TrackDto?> DeleteTrackAsync(int trackId);
    }
}