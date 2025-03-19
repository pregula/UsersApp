using UsersApp.Core.Exceptions;

namespace UsersApp.Application.Exceptions.User;

public sealed class UserNameAlreadyInUseException : CustomException
{
    public string UserName { get; }
    public UserNameAlreadyInUseException(string userName) : base($"User name: {userName} is already in use.")
    {
        UserName = userName;
    }
}