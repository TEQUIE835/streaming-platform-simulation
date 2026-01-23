using _1.Application.Interfaces.TokenInterfaces;
using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _dbContext;

    public RefreshTokenRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RefreshToken?> GetByRefreshToken(string refreshToken)
    {
        var token = await _dbContext.refreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
        return token;
    }

    public async Task<RefreshToken?> AddRefreshToken(RefreshToken refreshToken)
    {
        _dbContext.refreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync();
        return refreshToken;
    }

    public async Task DeleteRefreshToken(RefreshToken refreshToken)
    {
        _dbContext.refreshTokens.Remove(refreshToken);
        await _dbContext.SaveChangesAsync();
    }
}