namespace UsersApp.Core.Exceptions.User;

public sealed class InvalidPasswordException : CustomException
{
    public InvalidPasswordException() : base("Invalid password")
    {
    }
}