using MediatR;
using UsersApp.Application.Exceptions.User;
using UsersApp.Application.Security;
using UsersApp.Application.Services;
using UsersApp.Core.Repositories;
using UsersApp.Core.ValueObjects.User;

namespace UsersApp.Application.Commands.Users.Handlers;

public class SignInHandler : IRequestHandler<SignIn, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;
    
    public SignInHandler(IUserRepository userRepository,
        IAuthenticator authenticator,
        IPasswordManager passwordManager, 
        ITokenStorage tokenStorage)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
    }
    
    public async Task<Unit> Handle(SignIn request, CancellationToken cancellationToken)
    {
        var email = new Email(request.Email);
        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordManager.Validate(request.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        var jwt = _authenticator.CreateToken(user.Id, user.Role);
        _tokenStorage.Set(jwt);
        return Unit.Value;
    }
}