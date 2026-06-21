using System;
using System.Data;
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
using iap.API.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using iap.API.Common;
using iap.API.Repository;

namespace iap.API.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;

        public TrackService(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task<Result<IEnumerable<TrackDto>>> GetAllAsync()
        {
            var tracks = await _trackRepository.GetAllAsync();
            var dtos = tracks.Select(t => t.ToTrackDto());
            
            return Result<IEnumerable<TrackDto>>.Success(dtos);
        }

        public async Task<Result<TrackDto>> GetByIdAsync(int id)
        {
            var track = await _trackRepository.GetByIdAsync(id);

            if (track is null)
                return Result<TrackDto>.NotFound("Track not found");

            return Result<TrackDto>.Success(track.ToTrackDto());
        }

        public async Task<Result<IEnumerable<TrackDto>>> GetAllDeletedAsync()
        {
            var tracks = await _trackRepository.GetAllDeletedAsync();
            var dtos = tracks.Select(t => t.ToTrackDto());
            
            return Result<IEnumerable<TrackDto>>.Success(dtos);
        }

        public async Task<Result<TrackDto>> GetByIdDeletedAsync(int id)
        {
            var track = await _trackRepository.GetByIdDeletedAsync(id);

            if (track is null)
                return Result<TrackDto>.NotFound("Track not found");

            return Result<TrackDto>.Success(track.ToTrackDto());
        }

        public async Task<Result<TrackDto>> CreateAsync(CreateTrackRequestDto trackDto)
        {

            // Check for duplicate track name
            var existing = await _trackRepository
                .GetByTitleAndUserAsync(trackDto.Title, trackDto.UserId);

            if (existing is not null)
                return Result<TrackDto>.Conflict("Track with this title already exists");

            // Get model columns from dto to populate for new object
            var trackModel = trackDto.ToTrackFromCreateDto();
            trackModel.UserId = 1;
            trackModel.UploadedAt = DateTime.UtcNow;
            trackModel.IsDeleted = false;

            // Call repo to create track object
            var created = await _trackRepository.CreateAsync(trackModel);

            return Result<TrackDto>.Success(created.ToTrackDto());
            
        }

        public async Task<Result<TrackDto>> UpdateAsync(int id, UpdateTrackRequestDto updateDto)
        {   
            var existingTrack = await _trackRepository.GetByIdAsync(id);

            if(existingTrack is null)
                return Result<TrackDto>.NotFound("Track not found");

            // Allow user to update custom details for track to override original details
            // If same EF Core leaves current data 
            // Original fields are populated by metadata or are null if not found
            existingTrack.Title = updateDto.Title ?? existingTrack.Title;
            existingTrack.Artist = updateDto.Artist ?? existingTrack.Artist;
            existingTrack.AlbumName = updateDto.AlbumName ?? existingTrack.AlbumName;
            existingTrack.CoverArtUrl = updateDto.CoverArtUrl ?? existingTrack.CoverArtUrl;
            existingTrack.UpdatedAt = DateTimeOffset.UtcNow;  // server sets this

            await _trackRepository.SaveAsync();

            return Result<TrackDto>.Success(existingTrack.ToTrackDto());
        }

        public async Task<Result<TrackDto>> SoftDeleteTrackAsync(int id)
        {
            // Get track from repo
            var track = await _trackRepository.GetByIdAsync(id);

            if(track is null)
                return Result<TrackDto>.NotFound("Track not found");

            track.IsDeleted = true;
            track.DeletedAt = DateTimeOffset.Now;  // server sets this

            // Call repo to update deleted properties
            await _trackRepository.SaveAsync();

            return Result<TrackDto>.Success(track.ToTrackDto());
            
        }

        public async Task<Result<TrackDto>> UndoSoftDeleteTrackAsync(int id)
        {
            // Get deleted track from repo
            var track = await _trackRepository.GetByIdDeletedAsync(id);

            if(track is null)
                return Result<TrackDto>.NotFound("Track not found");

            // Restore track so is not filtered out of track queries
            track.IsDeleted = false;
            track.DeletedAt = null;

            // Call repo to update deleted properties
            await _trackRepository.SaveAsync();

            return Result<TrackDto>.Success(track.ToTrackDto());
            
        }
    }
}