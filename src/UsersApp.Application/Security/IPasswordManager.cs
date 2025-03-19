namespace UsersApp.Application.Security;

public interface IPasswordManager
{
    string HashPassword(string password);
    bool Validate(string password, string securedPassword);
}