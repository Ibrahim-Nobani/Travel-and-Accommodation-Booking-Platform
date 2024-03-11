using Microsoft.AspNetCore.Authentication.JwtBearer;
using TravelBookingPlatform.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using TravelBookingPlatform.Application.Authorization;

public static class JwtExtensions
{
    public static void AddCustomJWT(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, JwtTokenGenerator>();
        services.Configure<JwtTokenConfig>(configuration.GetSection("JWTToken"));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtConfig = configuration.GetSection("JWTToken").Get<JwtTokenConfig>() ?? throw new ArgumentNullException("JWTToken:Wrong Configuration");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey));

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = key
                };
            });

        services.AddAuthorization();
    }
}
