using _1.Application.DTOs.AuthDtos;
using _1.Application.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace VuePractice.Controller;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        try
        {
            await _authService.Register(request);
            return Ok(request);
        }
        catch (Exception e)
        {
            return BadRequest($"Error: {e.Message}");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var response =  await _authService.Login(request);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest($"Error: {e.Message}");
        }
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        try
        {
            var response = await _authService.ValidateRefresh(refreshToken);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest($"Error: {e.Message}");
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] string refreshToken)
    {
        try
        {
            await _authService.Logout(refreshToken);
            return Ok("logout succesful");
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
}