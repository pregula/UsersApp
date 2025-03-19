using UsersApp.Application.DTO;

namespace UsersApp.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}