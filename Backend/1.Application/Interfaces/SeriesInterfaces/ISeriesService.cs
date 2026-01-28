using _1.Application.DTOs.SeriesDtos;

namespace _1.Application.Interfaces.SeriesInterfaces;

public interface ISeriesService
{
    Task AddSeasonAsync(Guid seriesId);
    Task AddEpisodeAsync(Guid seasonId, CreateEpisodeDto dto);
    Task UpdateEpisodeAsync(Guid episodeId, UpdateEpisodeDto dto);
}