using _2.Domain.Entities;

namespace _1.Application.Interfaces.UserInterfaces;

public interface IUserRepository
{
    public Task<ICollection<User>?> GetUsers();
    public Task<User?> GetByEmail(string email);
    public Task<User?> GetById(Guid userId);
    public Task<User?> AddUser(User user);
}