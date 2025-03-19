using UsersApp.Core.Exceptions;

namespace UsersApp.Application.Exceptions.User;

public sealed class EmailAlreadyInUseException : CustomException
{
    public string Email { get; }
    public EmailAlreadyInUseException(string email) : base($"Email: {email} is already in use.")
    {
        Email = email;
    }
}