using UsersApp.Core.Exceptions;

namespace UsersApp.Application.Exceptions.User;

public sealed class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}