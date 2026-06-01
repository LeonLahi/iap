using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iap.API.Interfaces;
using iap.API.Models;
using iap.API.Data;
using iap.API.Dtos;
using System.Formats.Tar;
using iap.API.Mappers;

namespace iap.API.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;

        public TrackService(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task<TrackDto?> DeleteTrackAsync(int trackId)
        {
            // Get track from repo
            var track = await _trackRepository.GetByIdAsync(trackId);

            // TODO: Set global query to filter out deleted records

            // Call repo to delete track from playlist
            var updated = await _trackRepository.DeleteTrackAsync(track);
            return updated?.ToTrackDto();
            
        }
    }
}