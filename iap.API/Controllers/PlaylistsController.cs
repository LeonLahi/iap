using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Data;
using iap.API.Dtos;
using iap.API.Interfaces;
using iap.API.Mappers;
using iap.API.Models;
using iap.API.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace iap.API.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class PlaylistsController : ControllerBase
    {
        private readonly IapDbContext _context;
        private readonly IPlaylistRepository _playlistRepo;
        private readonly IPlaylistService _playlistService;
        
        public PlaylistsController(IapDbContext context, IPlaylistRepository playlistRepo, IPlaylistService playlistService)
        {
            _playlistRepo = playlistRepo;
            _playlistService = playlistService;
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var result = await _playlistService.GetAllAsync();
            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _playlistService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpGet("deleted")]

        public async Task<IActionResult> GetAllDeleted()
        {
            var result = await _playlistService.GetAllDeletedAsync();

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpGet("{id}/deleted")]

        public async Task<IActionResult> GetByIdDeleted([FromRoute] int id)
        {
            var result = await _playlistService.GetByIdDeletedAsync(id);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreatePlaylistRequestDto playlistDto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _playlistService.CreateAsync(playlistDto, userId);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }
        
        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePlaylistRequestDto updateDto)
        {
            var result = await _playlistService.UpdateAsync(id, updateDto);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteAsync([FromRoute] int id)
        {
            var result = await _playlistService.SoftDeletePlaylistAsync(id);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpGet("{id}/delete-impact")]
        public async Task<IActionResult> GetDeleteImpactAsync([FromRoute] int id)
        {
            var result = await _playlistService.GetDeleteImpactAsync(id);
            
            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpPost("{id}/restore")]
        public async Task<IActionResult> UndoSoftDeleteAsync([FromRoute] int id)
        {
            var result = await _playlistService.UndoSoftDeletePlaylistAsync(id);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpPost("iap.API/playlists/{playlistId}/tracks/{trackId}/track-to-playlist")]
        public async Task<IActionResult> AddTrack([FromRoute] int playlistId, [FromRoute] int trackId)
        {

            var result = await _playlistService.AddTrackAsync(playlistId, trackId);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpDelete("iap.API/Playlist/{playlistId}/Track/{trackId}/track-to-playlist-delete")]
        public async Task<IActionResult> DeleteTrackFromPlaylist([FromRoute] int playlistId, [FromRoute] int trackId)
        {
            var result = await _playlistService.DeleteTrackFromPlaylistAsync(playlistId, trackId);
            
            return result.ToActionResult(this);
        }

        [HttpPost("iap.API/playlists/{folderId}/playlists/{playlistId}/playlist-to-folder")]
        public async Task<IActionResult> AddPlaylistToFolder([FromRoute] int folderId, [FromRoute] int playlistId)
        {

            var result = await _playlistService.AddPlaylistToFolderAsync(folderId, playlistId);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpDelete("iap.API/playlists/{folderId}/playlists/{playlistId}/playlist-to-folder-delete")]
        public async Task<IActionResult> DeletePlaylistFromFolder([FromRoute] int folderId, [FromRoute] int playlistId)
        {
            var result = await _playlistService.DeletePlaylistFromFolderAsync(folderId, playlistId);
            
            return result.ToActionResult(this);
        }

    }

}