using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Data;
using iap.API.Dtos;
using iap.API.Interfaces;
using iap.API.Mappers;
using iap.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PlaylistController : ControllerBase
    {
        private readonly IapDbContext _context;
        private readonly IPlaylistRepository _playlistRepo;
        private readonly IPlaylistService _playlistService;
        
        public PlaylistController(IapDbContext context, IPlaylistRepository playlistRepo, IPlaylistService playlistService)
        {
            _playlistRepo = playlistRepo;
            _playlistService = playlistService;
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var Playlists = await _playlistRepo.GetAllAsync();
            var PlaylistDto = Playlists.Select(pt => pt.ToPlaylistDto());

            return Ok(PlaylistDto);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var Playlist = await _playlistRepo.GetByIdAsync(id);

            if (Playlist == null)
            {
                return NotFound();
            }

            return Ok(Playlist.ToPlaylistDto());
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreatePlaylistRequestDto playlistDto)
        {
            var resultDto = await _playlistService.CreateAsync(playlistDto);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }
        
        // [HttpPut]
        // [Route("{id}")]

        // public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePlaylistRequestDto updateDto)
        // {
        //     var playlistModel = await _playlistService.UpdateAsync(id, updateDto);

        //     if (playlistModel == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(playlistModel);
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        // {
        //     var playlistModel = await _playlistService.DeleteAsync(id);

        //     if (playlistModel == null)
        //     {
        //         return NotFound();
        //     }

        //     return NoContent();
        // }

        // [HttpPost("iap.API/Playlist/{playlistId}/Track/{trackId}")]
        // public async Task<IActionResult> AddTrack([FromRoute] int playlistId, [FromRoute] int trackId)
        // {

        //     var Playlist = await _playlistService.AddTrackAsync(playlistId, trackId);

        //     if (Playlist == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(Playlist);
        // }

        // [HttpDelete("iap.API/Playlist/{playlistId}/Track/{trackId}")]
        // public async Task<IActionResult> DeleteTrack([FromRoute] int playlistId, [FromRoute] int trackId)
        // {
        //     var trackModel = await _playlistService.DeleteTrackAsync(playlistId, trackId);

        //     if (trackModel == null)
        //     {
        //         return NotFound();
        //     }

        //     return NoContent();
        // }

    }

}