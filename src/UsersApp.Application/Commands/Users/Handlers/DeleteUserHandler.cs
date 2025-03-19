using MediatR;
using UsersApp.Application.Exceptions.User;
using UsersApp.Core.Repositories;

namespace UsersApp.Application.Commands.Users.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUser, Unit>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await  _userRepository.GetAsync(request.UserId);
        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        await _userRepository.DeleteAsync(user);
        return Unit.Value;
    }
}