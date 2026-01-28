using _1.Application.DTOs.ContentDtos;
using _2.Domain.Entities;

namespace _1.Application.Interfaces.ContentInterfaces;

public interface IContentService
{
    Task<IReadOnlyCollection<ContentListItemDto>> GetAllAsync();
    Task<IReadOnlyCollection<ContentListItemDto>> GetByTypeAsync(ContentType type);
    Task<IReadOnlyCollection<ContentListItemDto>> GetByGenreAsync(Guid genreId);

    Task<ContentDetailDto?> GetByIdAsync(Guid contentId, ContentType type);

    Task<Guid> CreateMovieAsync(CreateMovieDto dto);
    Task<Guid> CreateSeriesAsync(CreateSeriesDto dto);
    Task UpdateAsync(Guid contentId, UpdateContentDto dto);
    
    Task ChangeStatusAsync(Guid contentId, ChangeContentStatusDto dto);
    Task<MoviePlaybackDto?> GetMoviePlaybackAsync(Guid contentId);
    Task<EpisodePlaybackDto?> GetEpisodePlaybackAsync(Guid episodeId);
    
}