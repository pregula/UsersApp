using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersApp.Core.Repositories;
using UsersApp.Infrastructure.DAL.Decorators;
using UsersApp.Infrastructure.DAL.Repositories;

namespace UsersApp.Infrastructure.DAL;

internal static class Extensions
{
    private const string _sectionName = "mssql";
    
    public static IServiceCollection AddMsSql(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(_sectionName);
        services.Configure<MsSqlOptions>(section);
        var options = configuration.GetOptions<MsSqlOptions>(_sectionName);
        services.AddDbContext<UsersAppDbContext>(x => x.UseSqlServer(options.ConnectionString));
        services.AddScoped<IUserRepository, MsSqlUserRepository>();
        services.AddScoped<IUnitOfWork, MsSqlUnitOfWork>();
        services.TryDecorate(typeof(IRequestHandler<,>), typeof(UnitOfWorkHandlerDecorator<,>));
        services.AddHostedService<DatabaseInitializer>();
        return services;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);
        
        return options;
    }
}