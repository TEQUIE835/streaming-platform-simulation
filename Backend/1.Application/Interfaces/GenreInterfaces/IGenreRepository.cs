using _2.Domain.Entities;

namespace _1.Application.Interfaces.GenreInterfaces;

public interface IGenreRepository
{
    public Task<IReadOnlyCollection<Genre>?> GetAllGenres();
    public Task<Genre?> GetGenreById(Guid id);
    public Task<Genre?> GetGenreByName(string name);
    public Task<Genre?>  AddGenre(Genre genre);
    public Task<Genre?> UpdateGenre(Genre genre);
    public Task DeleteGenre(Genre genre);
}