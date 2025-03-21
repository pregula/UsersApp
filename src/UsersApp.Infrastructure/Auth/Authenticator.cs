﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UsersApp.Application.DTO;
using UsersApp.Application.Security;
using UsersApp.Core.Abstractions;

namespace UsersApp.Infrastructure.Auth;

internal class Authenticator : IAuthenticator
{
    private readonly IClock _clock;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly TimeSpan _expiry;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtHandler = new JwtSecurityTokenHandler();

    public Authenticator(IOptions<AuthOptions> options, IClock clock)
    {
        _clock = clock;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigninKey)),
            SecurityAlgorithms.HmacSha256);
    }

    public JwtDto CreateToken(Guid userId, string role)
    {
        var now = _clock.Current();
        var expiries = now.Add(_expiry);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new Claim(ClaimTypes.Role, role)
        };

        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expiries, _signingCredentials);
        var accessToken = _jwtHandler.WriteToken(jwt);

        return new() { AccessToken = accessToken };
    }
}