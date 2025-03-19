using UsersApp.Core.Exceptions.User;

namespace UsersApp.Core.ValueObjects.User;

public sealed record Role
{
    private const string admin = "admin";
    private const string user = "user";
    public static IEnumerable<string> AvailableRoles { get; } = new[] { admin, user };
    public string Value { get; }

    public Role(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
        {
            throw new InvalidRoleException(value);
        }

        if (!AvailableRoles.Contains(value))
        {
            throw new InvalidRoleException(value);
        }
        
        Value = value;
    }
    
    public static Role Admin() => new(admin);

    public static Role User() => new(user);

    public static implicit operator Role(string value) => new Role(value);

    public static implicit operator string(Role value) => value?.Value;

    public override string ToString() => Value;
}