using MediatR;

namespace UsersApp.Application.Commands.Users;

public record ChangeUserName(Guid UserId, string UserName) : IRequest<Unit>;