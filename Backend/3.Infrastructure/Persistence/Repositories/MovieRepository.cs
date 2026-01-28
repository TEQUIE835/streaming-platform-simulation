using _1.Application.Interfaces.MovieInterfaces;
using _2.Domain.Entities;

namespace _3.Infrastructure.Persistence.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _dbContext;

    public MovieRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddMovie(Movie movie)
    {
        _dbContext.movies.Add(movie);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateMovie(Movie movie)
    {
        _dbContext.movies.Update(movie);
        await _dbContext.SaveChangesAsync();
    }
}