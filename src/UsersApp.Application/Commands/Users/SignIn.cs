using MediatR;

namespace UsersApp.Application.Commands.Users;

public record SignIn(string Email, string Password) : IRequest<Unit>;