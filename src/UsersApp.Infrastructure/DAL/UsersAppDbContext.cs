using Microsoft.EntityFrameworkCore;
using UsersApp.Core.Entities;

namespace UsersApp.Infrastructure.DAL;

internal sealed class UsersAppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public UsersAppDbContext(DbContextOptions<UsersAppDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}