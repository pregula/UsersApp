using Microsoft.EntityFrameworkCore;
using UsersApp.Core.Entities;
using UsersApp.Core.Repositories;
using UsersApp.Core.ValueObjects.User;

namespace UsersApp.Infrastructure.DAL.Repositories;

internal sealed class MsSqlUserRepository : IUserRepository
{
    private readonly UsersAppDbContext _dbContext;
    public MsSqlUserRepository(UsersAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(User user) 
        => await _dbContext.AddAsync(user);

    public Task<User> GetAsync(UserId id)
        => _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

    public Task<User> GetByEmailAsync(Email email)
        => _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);

    public Task<User> GetByUserNameAsync(UserName userName)
    {
        return _dbContext.Users
            .SingleOrDefaultAsync(u => u.UserName == userName);
    }

    public Task UpdateAsync(User user)
    {
        _dbContext.Update(user);
        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(User user)
    {
        _dbContext.Remove(user);
        return Task.CompletedTask;
    }
}