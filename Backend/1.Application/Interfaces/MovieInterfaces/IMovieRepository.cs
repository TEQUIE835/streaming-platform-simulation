using _2.Domain.Entities;

namespace _1.Application.Interfaces.MovieInterfaces;

public interface IMovieRepository
{
    public Task AddMovie(Movie movie);
    public Task UpdateMovie(Movie movie);
}