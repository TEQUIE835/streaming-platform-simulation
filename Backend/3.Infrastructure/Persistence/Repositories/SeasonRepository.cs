using _1.Application.Interfaces.SeasonInterfaces;
using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3.Infrastructure.Persistence.Repositories;

public class SeasonRepository : ISeasonRepository
{
    private readonly AppDbContext _dbContext;

    public SeasonRepository(AppDbContext dbContext)
    {
        _dbContext  = dbContext;
    }

    public async Task<Season?> GetSeasonById(Guid id)
    {
        return await _dbContext.seasons.Include(se => se.Episodes).FirstOrDefaultAsync(se => se.Id == id);
    }

    public async Task<Season?> AddSeason(Season season)
    {
        _dbContext.seasons.Add(season);
        await _dbContext.SaveChangesAsync();
        return season;
    }

    public async Task<Season?> UpdateSeason(Season season)
    {
        _dbContext.seasons.Update(season);
        await _dbContext.SaveChangesAsync();
        return season;
    }
    
}