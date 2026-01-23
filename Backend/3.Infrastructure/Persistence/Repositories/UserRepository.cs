using _1.Application.Interfaces.UserInterfaces;
using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<User>?> GetUsers()
    {
        return await _dbContext.users.ToListAsync();
    }

    public async Task<User?> GetByEmail(string email)
    {
        var user = await _dbContext.users.FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }

    public async Task<User?> GetById(Guid userId)
    {
        var user = await _dbContext.users.FindAsync(userId);
        return user;
    }

    public async Task<User?> AddUser(User user)
    {
        _dbContext.users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
}