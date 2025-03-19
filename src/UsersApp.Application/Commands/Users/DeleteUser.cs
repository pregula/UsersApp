using MediatR;

namespace UsersApp.Application.Commands.Users;

public record DeleteUser(Guid UserId) : IRequest<Unit>;