using UsersApp.Core.Abstractions;
using UsersApp.Core.Entities;
using UsersApp.Core.Repositories;
using UsersApp.Core.ValueObjects;
using UsersApp.Core.ValueObjects.User;

namespace UsersApp.Infrastructure.DAL.Repositories;

internal class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users;
    private readonly IClock _clock;
    
    public InMemoryUserRepository(IClock clock)
    {
        _clock = clock;
        _users = new()
        {
            new(Guid.Parse("00000000-0000-0000-0000-000000000001"), "johndoe@usersapp.com", "JohnDoe", "passwordxyz",
                Role.User(), new Date(_clock.Current()))
        };
    }

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User> GetAsync(UserId id) 
        => Task.FromResult(_users.SingleOrDefault(u => u.Id == id));

    public Task<User> GetByEmailAsync(Email email)
    {
        return Task.FromResult(_users.SingleOrDefault(u => u.Email == email));
    }

    public Task<User> GetByUserNameAsync(UserName userName)
    {
        return Task.FromResult(_users.SingleOrDefault(u => u.UserName == userName));
    }

    public Task UpdateAsync(User user)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(User user)
    {
        _users.Remove(user);
        return Task.CompletedTask;
    }
}