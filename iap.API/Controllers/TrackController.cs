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

    public class TrackController : ControllerBase
    {
        private readonly IapDbContext _context;
        private readonly ITrackRepository _trackRepo;
        private readonly ITrackService _trackService;
        public TrackController(IapDbContext context, ITrackRepository trackRepo, ITrackService trackService)
        {
            _trackRepo = trackRepo;
            _trackService = trackService;
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var tracks = await _trackRepo.GetAllAsync();
            var trackDto = tracks.Select(t => t.ToTrackDto());

            return Ok(trackDto);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var track = await _trackRepo.GetByIdAsync(id);

            if (track == null)
            {
                return NotFound();
            }

            return Ok(track.ToTrackDto());
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateTrackRequestDto trackDto)
        {
            var trackModel = trackDto.ToTrackFromCreateDto();
            trackModel.UserId = 1;

            
            if (trackModel == null)
            {
                return NotFound();
            }
            
            await _trackRepo.CreateAsync(trackModel);
            return CreatedAtAction(nameof(GetById), new {id = trackModel.Id}, trackModel.ToTrackDto());
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTrackRequestDto updateDto)
        {
            var trackModel = await _trackRepo.UpdateTrackAsync(id, updateDto);

            if (trackModel == null)
            {
                return NotFound();
            }

            return Ok(trackModel.ToTrackDto());
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteTrackAsync([FromRoute] int id)
        {
            var trackModel = await _trackService.DeleteTrackAsync(id);

            if (trackModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}