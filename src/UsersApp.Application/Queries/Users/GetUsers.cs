using MediatR;
using UsersApp.Application.DTO.Users;

namespace UsersApp.Application.Queries.Users;

public class GetUsers : Pagination, IRequest<IEnumerable<UserDto>>
{
    public string Name { get; set; }
    public string Email { get; set; }
}