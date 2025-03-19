using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UsersApp.Application.Security;
using UsersApp.Core.Entities;

namespace UsersApp.Infrastructure.Security;

public static class Extension
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>();
        return services;
    }
}