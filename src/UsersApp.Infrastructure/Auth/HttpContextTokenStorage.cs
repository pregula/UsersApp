using Microsoft.AspNetCore.Http;
using UsersApp.Application.DTO;
using UsersApp.Application.Services;

namespace UsersApp.Infrastructure.Auth;

internal sealed class HttpContextTokenStorage : ITokenStorage
{
    private readonly string TokenKey = "jwt";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Set(JwtDto jwt)
        => _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);

    public JwtDto Get()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return null;
        }

        if (_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt))
        {
            return jwt as JwtDto;
        }

        return null;
    }
}