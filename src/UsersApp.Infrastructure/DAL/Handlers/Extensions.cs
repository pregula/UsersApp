using UsersApp.Application.DTO.Users;
using UsersApp.Core.Entities;

namespace UsersApp.Infrastructure.DAL.Handlers;

internal static class Extensions
{
    public static UserDto AsDto(this User entity)
    {
        return new()
        {
            Id = entity.Id,
            UserName = entity.UserName
        };
    }
    
}