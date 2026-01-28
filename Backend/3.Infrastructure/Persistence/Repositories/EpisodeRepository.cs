using _1.Application.Interfaces.EpisodeInterfaces;
using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3.Infrastructure.Persistence.Repositories;

public class EpisodeRepository : IEpisodeRepository
{
    private readonly AppDbContext _dbContext;
    public EpisodeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Episode?> GetById(Guid id)
    {
        return await _dbContext.episodes.FindAsync(id);
    }

    public async Task AddEpisode(Episode episode)
    {
        _dbContext.episodes.Add(episode);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateEpisode(Episode episode)
    {
        _dbContext.episodes.Update(episode);
        await _dbContext.SaveChangesAsync();
    }
    
}