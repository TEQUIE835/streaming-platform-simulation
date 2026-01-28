using System.Globalization;
using _1.Application.DTOs.SeriesDtos;
using _1.Application.Interfaces.CloudinaryInterfaces;
using _1.Application.Interfaces.EpisodeInterfaces;
using _1.Application.Interfaces.SeasonInterfaces;
using _1.Application.Interfaces.SeriesInterfaces;
using _2.Domain.Entities;

namespace _1.Application.Services;

public class SeriesService : ISeriesService
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly ISeasonRepository _seasonRepository;
    private readonly IEpisodeRepository _episodeRepository;
    private readonly ICloudinaryService _cloudinary;

    public SeriesService(
        ISeriesRepository seriesRepository,
        ISeasonRepository seasonRepository,
        IEpisodeRepository episodeRepository,
        ICloudinaryService cloudinary)
    {
        _seriesRepository = seriesRepository;
        _seasonRepository = seasonRepository;
        _episodeRepository = episodeRepository;
        _cloudinary = cloudinary;
    }

    public async Task AddSeasonAsync(Guid seriesId)
    {
        var series = await _seriesRepository.GetByContentIdAsync(seriesId);
        if (series == null)
            throw new ArgumentException("Series not found");
        var season = new Season
        {
            SeriesId = series.ContentId,
            SeasonNumber = series.Seasons.Count + 1
        };
        await _seasonRepository.AddSeason(season);
    }

    public async Task AddEpisodeAsync(Guid seasonId, CreateEpisodeDto dto)
    {
        var season = await _seasonRepository.GetSeasonById(seasonId);
        if (season == null) throw new ArgumentException("Season not found");
        var upload = await _cloudinary.UploadVideoAsync(dto.VideoFile);
        var episode = new Episode()
        {
            Title = dto.Title,
            Description = dto.Description,
            SeasonId = season.Id,
            DurationInSeconds = upload.DurationInSeconds,
            VideoPublicId = upload.PublicId,
            VideoUrl = upload.Url,
            EpisodeNumber = season.Episodes.Count + 1
        };
        await _episodeRepository.AddEpisode(episode);
    }

    public async Task UpdateEpisodeAsync(Guid episodeId, UpdateEpisodeDto dto)
    {
        var episode = await _episodeRepository.GetById(episodeId);
        if (episode == null) throw new ArgumentException("Episode not found");
        episode.Title = dto.Title;
        episode.Description = dto.Description;
        if (dto.VideoFile != null)
        {
            var upload = await _cloudinary.UploadVideoAsync(dto.VideoFile);
            await _cloudinary.DeleteAsync(episode.VideoPublicId);
            episode.DurationInSeconds = upload.DurationInSeconds;
            episode.VideoUrl = upload.Url;
            episode.VideoPublicId = upload.PublicId;
        }

        ;

        await _episodeRepository.UpdateEpisode(episode);
    }
}