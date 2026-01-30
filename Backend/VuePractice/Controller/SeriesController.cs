using _1.Application.DTOs.SeriesDtos;
using _1.Application.Interfaces.SeriesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VuePractice.Controller;

[ApiController]
[Authorize(Roles="Admin")]
[Route("api/[controller]")]
public class SeriesController : ControllerBase
{
    private readonly ISeriesService _seriesService;

    public SeriesController(ISeriesService seriesService)
    {
        _seriesService = seriesService;
    }

    [HttpPost("season/add")]
    public async Task<IActionResult> AddSeasonAsync([FromBody] Guid seriesId)
    {
        try
        {
            await _seriesService.AddSeasonAsync(seriesId);
            return Ok("Season Added");
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [HttpPost("{seasonId}/episode/add")]
    public async Task<IActionResult> AddEpisodeAsync(Guid seasonId,[FromBody] CreateEpisodeDto req)
    {
        try
        {
            await _seriesService.AddEpisodeAsync(seasonId, req);
            return Ok("Episode Added");
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [HttpPatch("episode/{id}/update")]
    public async Task<IActionResult> UpdateEpisodeAsync(Guid id, [FromBody] UpdateEpisodeDto req)
    {
        try
        {
            await _seriesService.UpdateEpisodeAsync(id, req);
            return Ok("Episode Updated");
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
}