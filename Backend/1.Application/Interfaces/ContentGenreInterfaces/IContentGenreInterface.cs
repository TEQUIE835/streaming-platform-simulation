using _2.Domain.Entities;

namespace _1.Application.Interfaces.ContentGenreInterfaces;

public interface IContentGenreInterface
{
    public Task AddContentGenre(ContentGenre genre);
    public Task  DeleteContentGenre(ContentGenre genre);
}