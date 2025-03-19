using UsersApp.Core.Entities;
using UsersApp.Core.ValueObjects.User;

namespace UsersApp.Core.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> GetAsync(UserId id);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByUserNameAsync(UserName userName);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}