using _2.Domain.Entities;

namespace _1.Application.Interfaces.ContentInterfaces;

public interface IContentRepository
{
    Task<IReadOnlyCollection<Content>> GetAllAsync();
    Task<IReadOnlyCollection<Content>> GetByTypeAsync(ContentType type);
    Task<IReadOnlyCollection<Content>> GetByGenreAsync(Guid genreId);
    Task<Content?> GetByIdAsync(Guid id);
    Task<Content?> GetByIdAsync(Guid id, ContentType type);
    public Task<Content?> AddContent(Content content);
    public Task<Content?> UpdateContent(Content content);
    public Task DeleteContent(Content content);
}