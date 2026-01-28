using _1.Application.Interfaces.GenreInterfaces;
using _2.Domain.Entities;

namespace _1.Application.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository  genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<IReadOnlyCollection<Genre>> GetAllAsync()
    {
        var genres = await _genreRepository.GetAllGenres();
        if (genres == null || genres.Count == 0) throw new Exception("Theres no genres");
        return genres;
    }

    public async Task<Genre?> CreateAsync(string name)
    {
        var exists = await _genreRepository.GetGenreByName(name);
        if (exists != null) throw new Exception("Genre already exists");
        var genre = await _genreRepository.AddGenre(new Genre{Name = name});
        return genre;
    }

    public async Task UpdateAsync(Guid id, string name)
    {
        var genre = await _genreRepository.GetGenreById(id);
        if (genre == null) throw new ArgumentException("Genre Not Found");
        var exists = await _genreRepository.GetGenreByName(name);
        if (exists != null) throw new ArgumentException("Genre name is already in use");
        genre.Name = name;
        await _genreRepository.UpdateGenre(genre);
    }

    public async Task DeleteAsync(Guid id)
    {
        var genre = await _genreRepository.GetGenreById(id);
        if (genre == null) throw new ArgumentException("Genre Not Found");
        await _genreRepository.DeleteGenre(genre);
    }
}