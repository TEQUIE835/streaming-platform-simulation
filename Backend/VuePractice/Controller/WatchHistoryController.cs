using _1.Application.DTOs.WatchHistoryDtos;
using _1.Application.Interfaces.WatchHistoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VuePractice.Controller;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class WatchHistoryController : ControllerBase
{
    private readonly IWatchHistoryService _historyService;

    public WatchHistoryController(IWatchHistoryService historyService)
    {
        _historyService = historyService;
    }

    [HttpPost("watch/{userId}/update")]
    public async Task<IActionResult> UpdateProgressAsync(Guid userId, UpdateWatchProgressDto req)
    {
        try
        {
            await _historyService.UpdateProgressAsync(userId, req);
            return Ok("Progress Updated");
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [HttpGet("watchhistory/{userId}")]
    public async Task<IActionResult> GetByUserAsync(Guid userId)
    {
        try
        {
            var res = await _historyService.GetByUserAsync(userId);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [HttpPut("watchhistory/{historyId}/complete")]
    public async Task<IActionResult> CompleteWatchHistoryAsync(Guid historyId)
    {
        try
        {
            await _historyService.MarkCompletedAsync(historyId);
            return Ok("Completed");
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
}