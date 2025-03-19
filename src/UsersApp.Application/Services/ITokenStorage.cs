using UsersApp.Application.DTO;

namespace UsersApp.Application.Services;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}