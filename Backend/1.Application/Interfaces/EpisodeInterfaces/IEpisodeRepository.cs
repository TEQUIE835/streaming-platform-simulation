using _2.Domain.Entities;

namespace _1.Application.Interfaces.EpisodeInterfaces;

public interface IEpisodeRepository
{
    public Task<Episode?> GetById(Guid id);
    public Task AddEpisode(Episode episode);
    public Task UpdateEpisode(Episode episode);
}