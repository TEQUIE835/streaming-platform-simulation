using _2.Domain.Entities;

namespace _1.Application.Interfaces.SeriesInterfaces;

public interface ISeriesRepository
{
    public Task<Series?> AddSeries(Series series);
    Task<Series?> GetByContentIdAsync(Guid contentId);
    Task<IReadOnlyCollection<Season>> GetSeasonsAsync(Guid seriesId);

    Task<IReadOnlyCollection<Episode>> GetEpisodesBySeasonAsync(Guid seasonId);

    Task<Episode?> GetEpisodeAsync(Guid seriesId, int seasonNumber, int episodeNumber);
}