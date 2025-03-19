using UsersApp.Core.ValueObjects;
using UsersApp.Core.ValueObjects.User;

namespace UsersApp.Core.Entities;

public sealed class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public UserName UserName { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public Date CreatedAt { get; private set; }
    
    public User(UserId id, Email email, UserName userName, Password password, Role role, Date createdAt)
    {
        Id = id;
        Email = email;
        UserName = userName;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
    }

    public void ChangeUserName(UserName userName)
    => UserName = userName;
}