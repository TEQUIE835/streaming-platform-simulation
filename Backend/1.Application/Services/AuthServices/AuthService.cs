using _1.Application.DTOs.AuthDtos;
using _1.Application.Interfaces.TokenInterfaces;
using _1.Application.Interfaces.UserInterfaces;
using _1.Application.Services.HashingServices;
using _1.Application.Services.TokenServices;
using _2.Domain.Entities;

namespace _1.Application.Services.AuthServices;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly BCryptService _passwordHashing;
    private readonly CreateToken _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthService(IUserRepository userRepository,  BCryptService passwordHashing, CreateToken tokenService, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _passwordHashing = passwordHashing;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task Register(RegisterRequestDto request)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        if (user != null) throw new ArgumentException("Email already exists");
        var newUser = new User()
        {
            Email = request.Email,
            Username = request.Name,
            Password = _passwordHashing.HashPassword(request.Password),
        };
        await _userRepository.AddUser(newUser);
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        if (user == null) throw new ArgumentException("Incorrect email or password");
        var access = _passwordHashing.VerifyPassword(user.Password, request.Password);
        if (!access) throw new ArgumentException("Incorrect email or password");
        return await _tokenService.GenerateTokensAsync(user.Id, user.Email, user.Role);
    }

    public async Task<RefreshValidationDto?> ValidateRefresh(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByRefreshToken(_passwordHashing.HashPassword(refreshToken));
        if (token == null || token.IsExpired) throw new ArgumentException("Invalid Token");
        var user = await _userRepository.GetById(token.UserId);
        if (user == null) throw new Exception("User Not found");
        var newAccess = _tokenService.RefreshAccessToken(user.Id, user.Email, user.Role);
        var response = new RefreshValidationDto()
        {
            AccesToken = newAccess,
            RefreshToken = refreshToken,
        };
        return response;
    }

    public async Task Logout(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByRefreshToken(_passwordHashing.HashPassword(refreshToken));
        if (token == null) throw new ArgumentException("Invalid Token");
        await _refreshTokenRepository.DeleteRefreshToken(token);
    }
}