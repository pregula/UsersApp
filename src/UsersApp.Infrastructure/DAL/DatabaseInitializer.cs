using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UsersApp.Core.Abstractions;
using UsersApp.Core.Entities;
using UsersApp.Core.ValueObjects;
using UsersApp.Core.ValueObjects.User;

namespace UsersApp.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    
    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<UsersAppDbContext>();
            dbContext.Database.Migrate();
            var users = dbContext.Users.ToList();
            if (!users.Any())
            {
                var clock = scope.ServiceProvider.GetRequiredService<IClock>();
                users = new List<User>
                {
                    new User(Guid.Parse("00000000-0000-0000-0000-000000000001"), "john.doe@example.com", "John Doe", "password123", Role.Admin(), new Date(clock.Current()))
                };
                
                dbContext.AddRange(users);
            }

            dbContext.SaveChanges();
        }
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}