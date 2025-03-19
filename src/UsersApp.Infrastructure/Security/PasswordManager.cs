using Microsoft.AspNetCore.Identity;
using UsersApp.Application.Security;
using UsersApp.Core.Entities;

namespace UsersApp.Infrastructure.Security;

internal sealed class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordManager(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }
    
    public string HashPassword(string password) 
        => _passwordHasher.HashPassword(default, password);

    public bool Validate(string password, string securedPassword) 
        => _passwordHasher.VerifyHashedPassword(default, securedPassword, password) is PasswordVerificationResult.Success;
}