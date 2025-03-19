using MediatR;
using UsersApp.Application.Exceptions.User;
using UsersApp.Application.Security;
using UsersApp.Core.Abstractions;
using UsersApp.Core.Entities;
using UsersApp.Core.Repositories;
using UsersApp.Core.ValueObjects;
using UsersApp.Core.ValueObjects.User;

namespace UsersApp.Application.Commands.Users.Handlers;

public class SignUpHandler : IRequestHandler<SignUp, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public SignUpHandler(IUserRepository userRepository, IPasswordManager passwordManager, IClock clock)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _clock = clock;
    }
    
    public async Task<Unit> Handle(SignUp request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var email = new Email(request.Email);
        var userName = new UserName(request.UserName);
        var password = new Password(request.Password);
        
        if (await _userRepository.GetByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }

        if (await _userRepository.GetByUserNameAsync(userName) is not null)
        {
            throw new UserNameAlreadyInUseException(userName);
        }

        var hashedPassword = _passwordManager.HashPassword(password);
        var user = new User(userId, email, userName, hashedPassword, Role.User(), new Date(_clock.Current()));
        await _userRepository.AddAsync(user);
        
        return Unit.Value;
    }
}