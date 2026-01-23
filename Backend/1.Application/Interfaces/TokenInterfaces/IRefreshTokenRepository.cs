using _2.Domain.Entities;

namespace _1.Application.Interfaces.TokenInterfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByRefreshToken(string refreshToken);
    Task<RefreshToken?> AddRefreshToken(RefreshToken refreshToken);
    Task DeleteRefreshToken(RefreshToken refreshToken);
}