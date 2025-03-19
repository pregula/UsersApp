using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using UsersApp.Application.Queries;
using UsersApp.Application.Queries.Users;
using UsersApp.Core.Abstractions;

namespace UsersApp.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}