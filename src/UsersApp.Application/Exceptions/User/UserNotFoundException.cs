using UsersApp.Core.Exceptions;

namespace UsersApp.Application.Exceptions.User;

public sealed class UserNotFoundException : CustomException
{
    public Guid Id { get; }
    public UserNotFoundException(Guid id) : base($"User with ID: {id} was not found.")
    {
        Id = id;
    }
}