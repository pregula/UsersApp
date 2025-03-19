using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UsersApp.Application.Security;
using UsersApp.Application.Services;
using UsersApp.Infrastructure.DAL;

namespace UsersApp.Infrastructure.Auth;

public static class Extensions
{
    private const string SectionName = "auth";

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure <AuthOptions>(configuration.GetRequiredSection(SectionName));
        var options = configuration.GetOptions<AuthOptions>(SectionName);

        services
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddSingleton<ITokenStorage, HttpContextTokenStorage>()
            .AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jb =>
            {
                jb.Audience = options.Audience;
                jb.IncludeErrorDetails = false; // true only if test
                jb.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigninKey))
                };
            });

        services.AddAuthorization(authorization =>
        {
            authorization.AddPolicy("is-admin-or-moderator", policy =>
            {
                policy.RequireRole("admin", "moderator");
            });
        });

        return services;
    }
}