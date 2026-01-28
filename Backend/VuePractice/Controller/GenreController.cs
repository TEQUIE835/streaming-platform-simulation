using _1.Application.Interfaces.GenreInterfaces;
using _2.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VuePractice.Controller;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;
    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }


    [HttpGet("getall")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var genres = await _genreService.GetAllAsync();
            return Ok(genres);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles="Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateGenreAsync([FromBody] string name)
    {
        try
        {
            var res = await _genreService.CreateAsync(name);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles="Admin")]
    [HttpPatch("{id}/update")]
    public async Task<IActionResult> UpdateGenreAsync(Guid id, [FromBody] string name)
    {
        try
        {
            await _genreService.UpdateAsync(id, name);
            return Ok("Update Succesfull");
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles="Admin")]
    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await _genreService.DeleteAsync(id);
            return Ok("Delete Succesfull");
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
}