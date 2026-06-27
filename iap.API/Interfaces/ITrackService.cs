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
    public interface ITrackService
    {
        Task<Result<IEnumerable<TrackDto>>> GetAllAsync();
        Task<Result<TrackDto>> GetByIdAsync(int id);
        Task<Result<IEnumerable<TrackDto>>> GetAllDeletedAsync();
        Task<Result<TrackDto>> GetByIdDeletedAsync(int id);
        Task<Result<TrackDto>> CreateAsync(CreateTrackRequestDto trackDto, int userId);
        Task<Result<TrackDto>> UpdateAsync(int id, UpdateTrackRequestDto updateDto);
        Task<Result<TrackDto>> SoftDeleteTrackAsync(int id);
        Task<Result<TrackDto>> UndoSoftDeleteTrackAsync(int id);
    }
}