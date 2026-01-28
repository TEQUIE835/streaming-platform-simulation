using _1.Application.DTOs.ContentDtos;
using _1.Application.Interfaces.ContentInterfaces;
using _1.Application.Interfaces.EpisodeInterfaces;
using _1.Application.Interfaces.GenreInterfaces;
using _1.Application.Interfaces.CloudinaryInterfaces;
using _1.Application.Interfaces.MovieInterfaces;
using _1.Application.Interfaces.SeasonInterfaces;
using _1.Application.Interfaces.SeriesInterfaces;
using _2.Domain.Entities;

namespace _1.Application.Services;

public class ContentService : IContentService
{
    private readonly IContentRepository _contentRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly ISeriesRepository _seriesRepository;
    private readonly ISeasonRepository _seasonRepository;
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly ICloudinaryService _cloudinary;

    public ContentService(
        IContentRepository contentRepository,
        IMovieRepository movieRepository,
        ISeriesRepository seriesRepository,
        ISeasonRepository seasonRepository,
        IEpisodeRepository episodeRepository,
        IGenreRepository genreRepository,
        ICloudinaryService cloudinary)
    {
        _contentRepository = contentRepository;
        _movieRepository = movieRepository;
        _seriesRepository = seriesRepository;
        _seasonRepository = seasonRepository;
        _episodeRepository = episodeRepository;
        _genreRepository = genreRepository;
        _cloudinary = cloudinary;
    }
    public async Task<Guid> CreateMovieAsync(
        CreateMovieDto movieDto)
    {
        var genres = new List<Genre>();
        foreach (var id in movieDto.Content.GenreIds.Distinct())
        {
            var genre = await _genreRepository.GetGenreById(id);
            if (genre != null)
            {
                genres.Add(genre);
            }
        }
        
        if (genres.Count != movieDto.Content.GenreIds.Count)
            throw new ArgumentException("One or more genres not found");

        var thumbnail = await _cloudinary.UploadImageAsync(movieDto.Content.Thumbnail);
        var content = new Content
        {
            Title = movieDto.Content.Title,
            Description = movieDto.Content.Description,
            ThumbnailUrl = thumbnail.Url,
            ThumbnailPublicId = thumbnail.PublicId,
            Type = ContentType.Movie,
            Status = ContentStatus.Draft,
            Genres = genres.Select(g => new ContentGenre
            {
                GenreId = g.Id
            }).ToList()
        };

        await _contentRepository.AddContent(content);

        var upload = await _cloudinary.UploadVideoAsync(movieDto.VideoFile);

        var movie = new Movie
        {
            ContentId = content.Id,
            DurationInSeconds = upload.DurationInSeconds,
            VideoUrl = upload.Url,
            VideoPublicId = upload.PublicId
        };
        if (movie.DurationInSeconds == null) throw new Exception("Invalid video duration send by cloudinary");
        await _movieRepository.AddMovie(movie);

        return content.Id;
    }
    public async Task<Guid> CreateSeriesAsync(CreateSeriesDto seriesDto)
    {
        var genres = new List<Genre>();

        foreach (var id in seriesDto.Content.GenreIds.Distinct())
        {
            var genre = await _genreRepository.GetGenreById(id);
            if (genre == null)
                throw new ArgumentException($"Genre not found: {id}");

            genres.Add(genre);
        }

        var thumbnail = await _cloudinary.UploadImageAsync(seriesDto.Content.Thumbnail);

        var content = new Content
        {
            Title = seriesDto.Content.Title,
            Description = seriesDto.Content.Description,
            ThumbnailUrl = thumbnail.Url,
            ThumbnailPublicId = thumbnail.PublicId,
            Type = ContentType.Series,
            Status = ContentStatus.Draft,
            Genres = genres.Select(g => new ContentGenre
            {
                GenreId = g.Id
            }).ToList()
        };

        await _contentRepository.AddContent(content);

        var series = new Series
        {
            ContentId = content.Id
        };

        await _seriesRepository.AddSeries(series);
        
        var season = new Season
        {
            SeriesId = series.ContentId,
            SeasonNumber = 1
        };

        await _seasonRepository.AddSeason(season);
        
        var upload = await _cloudinary.UploadVideoAsync(seriesDto.FirstEpisodeVideo);

        if (upload.DurationInSeconds == null )
            throw new Exception("Invalid episode video duration");

        var episode = new Episode
        {
            SeasonId = season.Id,
            EpisodeNumber = 1,
            Title = seriesDto.FirstEpisodeTitle,
            Description = seriesDto.FirstEpisodeDescription,
            DurationInSeconds = upload.DurationInSeconds,
            VideoUrl = upload.Url,
            VideoPublicId = upload.PublicId
        };

        await _episodeRepository.AddEpisode(episode);

        return content.Id;
    }
    public async Task UpdateAsync(Guid contentId, UpdateContentDto dto)
    {
        var content = await _contentRepository.GetByIdAsync(contentId);
        if (content == null)
            throw new ArgumentException("Content not found");

        content.Title = dto.Title;
        content.Description = dto.Description;
        content.UpdatedAt = DateTime.UtcNow;

        await _contentRepository.UpdateContent(content);
    }
    public async Task ChangeStatusAsync(
        Guid contentId,
        ChangeContentStatusDto dto)
    {
        var content = await _contentRepository.GetByIdAsync(contentId);
        if (content == null)
            throw new ArgumentException("Content not found");

        content.Status = dto.Status;
        content.UpdatedAt = DateTime.UtcNow;

        await _contentRepository.UpdateContent(content);
    }
    
    public async Task<IReadOnlyCollection<ContentListItemDto>> GetAllAsync()
    {
        var contents = await _contentRepository.GetAllAsync();

        return contents
            .Where(c => c.Status == ContentStatus.Published)
            .Select(c => new ContentListItemDto
            {
                Id = c.Id,
                Title = c.Title,
                ThumbnailUrl = c.ThumbnailUrl,
                Type = c.Type
            })
            .ToList();
    }

    public async Task<IReadOnlyCollection<ContentListItemDto>> GetByTypeAsync(ContentType type)
    {
        var contents = await _contentRepository.GetByTypeAsync(type);

        return contents
            .Where(c => c.Status == ContentStatus.Published)
            .Select(c => new ContentListItemDto
            {
                Id = c.Id,
                Title = c.Title,
                ThumbnailUrl = c.ThumbnailUrl,
                Type = c.Type
            })
            .ToList();
    }

    public async Task<IReadOnlyCollection<ContentListItemDto>> GetByGenreAsync(Guid genreId)
    {
        var contents = await _contentRepository.GetByGenreAsync(genreId);

        return contents
            .Where(c => c.Status == ContentStatus.Published)
            .Select(c => new ContentListItemDto
            {
                Id = c.Id,
                Title = c.Title,
                ThumbnailUrl = c.ThumbnailUrl,
                Type = c.Type
            })
            .ToList();
    }

    public async Task<ContentDetailDto?> GetByIdAsync(Guid contentId, ContentType type)
    {
        var content = await _contentRepository.GetByIdAsync(contentId, type);
        if (content == null) return null;

        return content.Type switch
        {
            ContentType.Movie => MapToMovieDetail(content),
            ContentType.Series => MapToSeriesDetail(content),
            _ => null
        };
    }
    public async Task<MoviePlaybackDto?> GetMoviePlaybackAsync(Guid contentId)
    {
        var movie = await _contentRepository.GetByIdAsync(contentId, ContentType.Movie);
        if (movie == null) return null;

        return new MoviePlaybackDto
        {
            ContentId = contentId,
            VideoUrl = movie.Movie.VideoUrl,
            DurationInSeconds = movie.Movie.DurationInSeconds
        };
    }
    public async Task<EpisodePlaybackDto?> GetEpisodePlaybackAsync(Guid episodeId)
    {
        var episode = await _episodeRepository.GetById(episodeId);
        if (episode == null) return null;

        return new EpisodePlaybackDto
        {
            EpisodeId = episode.Id,
            VideoUrl = episode.VideoUrl,
            DurationInSeconds = episode.DurationInSeconds
        };
    }

    private MovieDetailDto MapToMovieDetail(Content content)
    {
        if (content.Movie == null)
            throw new InvalidOperationException("Movie data not loaded");

        return new MovieDetailDto
        {
            Id = content.Id,
            Title = content.Title,
            Description = content.Description,
            ThumbnailUrl = content.ThumbnailUrl,
            Type = content.Type,
            Genres = content.Genres
                .Select(g => g.Genre.Name)
                .ToList(),

            DurationInSeconds = content.Movie.DurationInSeconds
        };
    }

    private SeriesDetailDto MapToSeriesDetail(Content content)
    {
        if (content.Series == null)
            throw new InvalidOperationException("Series data not loaded");

        return new SeriesDetailDto
        {
            Id = content.Id,
            Title = content.Title,
            Description = content.Description,
            ThumbnailUrl = content.ThumbnailUrl,
            Type = content.Type,
            Genres = content.Genres
                .Select(g => g.Genre.Name)
                .ToList(),

            Seasons = content.Series.Seasons
                .OrderBy(s => s.SeasonNumber)
                .Select(s => new SeasonDto
                {
                    SeasonNumber = s.SeasonNumber,
                    Episodes = s.Episodes
                        .OrderBy(e => e.EpisodeNumber)
                        .Select(e => new EpisodeDto
                        {
                            Id = e.Id,
                            ContentId = content.Id,
                            EpisodeNumber = e.EpisodeNumber,
                            Title = e.Title,
                            Description = e.Description,
                            DurationInSeconds = e.DurationInSeconds
                        })
                        .ToList()
                })
                .ToList()
        };
    }

}