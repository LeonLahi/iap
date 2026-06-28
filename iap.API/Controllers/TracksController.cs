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

    public class TracksController : ControllerBase
    {
        private readonly IapDbContext _context;
        private readonly ITrackRepository _trackRepo;
        private readonly ITrackService _trackService;
        public TracksController(IapDbContext context, ITrackRepository trackRepo, ITrackService trackService)
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
    
        [HttpGet("deleted")]

        public async Task<IActionResult> GetAllDeleted()
        {
            var result = await _trackService.GetAllDeletedAsync();

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpGet("{id}/deleted")]

        public async Task<IActionResult> GetByIdDeleted([FromRoute] int id)
        {
            var result = await _trackService.GetByIdDeletedAsync(id);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateTrackRequestDto trackDto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _trackService.CreateAsync(trackDto, userId);

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteAsync([FromRoute] int id)
        {
            var result = await _trackService.SoftDeleteTrackAsync(id);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }

        [HttpPost("{id}/restore")]
        public async Task<IActionResult> UndoSoftDeleteAsync([FromRoute] int id)
        {
            var result = await _trackService.UndoSoftDeleteTrackAsync(id);

            if (!result.IsSuccess)
                return result.ToActionResult(this);

            return Ok(result.Value);
        }
    }

}