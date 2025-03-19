using MediatR;

namespace UsersApp.Application.Commands.Users;

public record SignUp(Guid UserId, string Email, string UserName, string Password) : IRequest<Unit>;