using _1.Application.Interfaces.SeriesInterfaces;
using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3.Infrastructure.Persistence.Repositories;

public class SeriesRepository : ISeriesRepository
{
    private readonly AppDbContext _dbContext;
    public SeriesRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Series?> AddSeries(Series series)
    {
        _dbContext.series.Add(series);
        await _dbContext.SaveChangesAsync();
        return series;
    }

    public async Task<Series?> GetByContentIdAsync(Guid contentId)
    {
        return await _dbContext.series
            .Include(s => s.Seasons.OrderBy(se => se.SeasonNumber))
            .ThenInclude(se => se.Episodes.OrderBy(e => e.EpisodeNumber))
            .FirstOrDefaultAsync(s => s.ContentId == contentId);
    }

    public async Task<IReadOnlyCollection<Season>> GetSeasonsAsync(Guid seriesId)
    {
        return await _dbContext.seasons
            .Where(s => s.SeriesId == seriesId)
            .OrderBy(s => s.SeasonNumber)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Episode>> GetEpisodesBySeasonAsync(Guid seasonId)
    {
        return await _dbContext.episodes
            .Where(e => e.SeasonId == seasonId)
            .OrderBy(e => e.EpisodeNumber)
            .ToListAsync();
    }

    public async Task<Episode?> GetEpisodeAsync(Guid seriesId, int seasonNumber, int episodeNumber)
    {
        return await _dbContext.episodes
            .Include(e => e.Season)
            .ThenInclude(s => s.Series)
            .Where(e =>
                e.Season.SeriesId == seriesId &&
                e.Season.SeasonNumber == seasonNumber &&
                e.EpisodeNumber == episodeNumber)
            .FirstOrDefaultAsync();
    }
}