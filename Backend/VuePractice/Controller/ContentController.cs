using _1.Application.DTOs.ContentDtos;
using _1.Application.Interfaces.ContentInterfaces;
using _2.Domain.Entities;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VuePractice.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContentController : ControllerBase
{
    private readonly IContentService _contentService;
    public ContentController(IContentService contentService)
    {
        _contentService =  contentService;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var res = await _contentService.GetAllAsync();
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [HttpGet("{type}/type")]
    public async Task<IActionResult> GetByTypeAsync(string type)
    {
        try
        {
            if (!Enum.TryParse<ContentType>(type, true, out var parsedType))
                return BadRequest("Invalid content type");

            var contents = await _contentService.GetByTypeAsync(parsedType);
            return Ok(contents);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [HttpGet("{genreId}/bygenre")]
    public async Task<IActionResult> GetByGenreAsyn(Guid genreId)
    {
        try
        {

            var contents = await _contentService.GetByGenreAsync(genreId);
            return Ok(contents);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [HttpGet("{id}/getbyid")]
    public async Task<IActionResult> GetByIdAsync(Guid id, [FromQuery] string? type)
    {
        try
        {
            if (!Enum.TryParse<ContentType>(type, true, out var parsedType))
                return BadRequest("Invalid content type");

            var result = await _contentService.GetByIdAsync(id, parsedType);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles="Admin")]
    [HttpPost("createmovie")]
    public async Task<IActionResult> CreateMovieAsync([FromBody] CreateMovieDto req)
    {
        try
        {
            var res = await _contentService.CreateMovieAsync(req);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles="Admin")]
    [HttpPost("createSeries")]
    public async Task<IActionResult> CreateSeriesAsync([FromBody] CreateSeriesDto req)
    {
        try
        {
            var res = await _contentService.CreateSeriesAsync(req);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles="Admin")]
    [HttpPatch("{id}/update")]
    public async Task<IActionResult> UpdateContentAsync(Guid id, [FromBody] UpdateContentDto req)
    {
        try
        {
            await _contentService.UpdateAsync(id, req);
            return Ok("Content Update Successful");
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles="Admin")]
    [HttpPut("{id}/changestatus")]
    public async Task<IActionResult> ChangeStatusAsync(Guid id, [FromBody] string? status)
    {
        try
        {
            if (!Enum.TryParse<ContentStatus>(status, true, out var parsedStatus))
                return BadRequest("Invalid content type");
            await _contentService.ChangeStatusAsync(id, new ChangeContentStatusDto
            {
                Status = parsedStatus
            });
            return Ok("Status Change Successful");
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [HttpGet("{id}/movieplayback")]
    public async Task<IActionResult> MoviePlayback(Guid id)
    {
        try
        {
            var res = await _contentService.GetMoviePlaybackAsync(id);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
    [HttpGet("{id}/episodeplayback")]
    public async Task<IActionResult> EpisodePlayback(Guid id)
    {
        try
        {
            var res = await _contentService.GetEpisodePlaybackAsync(id);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
    
}