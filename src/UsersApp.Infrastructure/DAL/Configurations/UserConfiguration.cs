using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersApp.Core.Entities;
using UsersApp.Core.ValueObjects;
using UsersApp.Core.ValueObjects.User;

namespace UsersApp.Infrastructure.DAL.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasConversion(u => u.Value, u => new UserId(u));
        builder.HasIndex(u => u.Email)
            .IsUnique();
        builder.Property(u => u.Email)
            .HasConversion(u => u.Value, u => new Email(u))
            .HasMaxLength(100);
        builder.HasIndex(u => u.UserName)
            .IsUnique();
        builder.Property(u => u.UserName)
            .HasConversion(u => u.Value, u => new UserName(u))
            .HasMaxLength(40);
        builder.Property(u => u.Password)
            .HasConversion(u => u.Value, u => new Password(u))
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(u => u.Role)
            .HasConversion(u => u.Value, u => new Role(u))
            .IsRequired()
            .HasMaxLength(30);
        builder.Property(u => u.CreatedAt)
            .HasConversion(u => u.Value, u => new Date(u))
            .IsRequired();
    }
}