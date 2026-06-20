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
            var result = await _trackService.GetAllAsync();

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _trackService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateTrackRequestDto trackDto)
        {
            var result = await _trackService.CreateAsync(trackDto);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTrackRequestDto updateDto)
        {
            var result = await _trackService.UpdateAsync(id, updateDto);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
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