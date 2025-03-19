using MediatR;
using UsersApp.Application.Exceptions.User;
using UsersApp.Core.Repositories;
using UsersApp.Core.ValueObjects.User;

namespace UsersApp.Application.Commands.Users.Handlers;

public class ChangeUserNameHandler : IRequestHandler<ChangeUserName, Unit>
{
    private readonly IUserRepository _userRepository;

    public ChangeUserNameHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ChangeUserName request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var newUserName = new UserName(request.UserName);
        
        var user = await  _userRepository.GetAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException(userId);
        }

        user.ChangeUserName(newUserName);
        await _userRepository.UpdateAsync(user);
        return Unit.Value;
    }
}