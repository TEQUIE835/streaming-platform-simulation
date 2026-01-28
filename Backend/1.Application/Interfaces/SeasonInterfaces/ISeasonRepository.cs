using _2.Domain.Entities;

namespace _1.Application.Interfaces.SeasonInterfaces;

public interface ISeasonRepository
{
    public Task<Season?> GetSeasonById(Guid id);
    public Task<Season?> AddSeason(Season season);
    public Task<Season?> UpdateSeason(Season season);
}