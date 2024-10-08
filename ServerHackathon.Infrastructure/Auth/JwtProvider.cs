﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerHackathon.Core.Interfaces.Auth;
using ServerHackathon.DomainModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerHackathon.Infrastructure.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateToken(User user)
    {
        Claim[] claims = [new(CustomClaims.UserId, user.Id.ToString()),
                            new (CustomClaims.UserName, user.Name.ToString()),
                            new (CustomClaims.UserSurname, user.Surname.ToString()),
                            new (CustomClaims.UserLogin, user.Login.ToString())];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}