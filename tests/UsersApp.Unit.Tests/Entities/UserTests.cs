using Shouldly;
using UsersApp.Core.Abstractions;
using UsersApp.Core.Entities;
using UsersApp.Core.Exceptions.User;
using UsersApp.Core.Repositories;
using UsersApp.Core.ValueObjects;
using UsersApp.Core.ValueObjects.User;
using UsersApp.Infrastructure.DAL.Repositories;
using UsersApp.Unit.Tests.Shared;

namespace UsersApp.Unit.Tests.Entities;

public class UserTests
{
    [Fact]
    public void given_creating_user_should_success()
    {
        var user = new User(_userId, _email, _userName, _password, _role, _createdAt);
        user.ShouldNotBeNull();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("short")]
    public void given_creating_too_short_or_empty_password_should_fail(string password)
    {
        var exception = Record.Exception(()=> new Password(password));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidPasswordException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData("janedoe@usersapp")]
    public void given_empty_or_invalid_email_should_fail(string email)
    {
        var exception = Record.Exception(() => new Email(email));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidEmailException>();
    }
    
    [Fact]
    public void given_uncorrect_role_should_fail()
    {
        var exception = Record.Exception(() => new Role("badRole"));
        
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidRoleException>();
    }
    
    private readonly IClock _clock;
    private readonly IUserRepository _userRepository;
    private UserId _userId;
    private Email _email;
    private UserName _userName;
    private Password _password;
    private Role _role;
    private Date _createdAt;
    
    public UserTests()
    {
        _clock = new TestClock();
        _userRepository = new InMemoryUserRepository(_clock);
        _userId = UserId.Create();
        _email = new Email("janedoe@usersapp.com");
        _userName = new UserName("Jane Doe");
        _password = new Password("secretpassword");
        _role = Role.User();
        _createdAt = new Date(_clock.Current());
    }
}