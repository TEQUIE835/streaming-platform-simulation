using _2.Domain.Entities;

namespace _1.Application.Interfaces.GenreInterfaces;

public interface IGenreService
{
    Task<IReadOnlyCollection<Genre>> GetAllAsync();
    Task<Genre?> CreateAsync(string name);
    Task UpdateAsync(Guid id, string name);
    Task DeleteAsync(Guid id);
}