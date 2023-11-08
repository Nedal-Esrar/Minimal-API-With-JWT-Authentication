using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinimalApiWithJwtAuthentication.API.Configurations;
using MinimalApiWithJwtAuthentication.API.Interfaces;
using MinimalApiWithJwtAuthentication.API.Models;

namespace MinimalApiWithJwtAuthentication.API.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly JwtAuthenticationConfig _jwtAuthenticationConfig;

  public JwtTokenGenerator(IOptions<JwtAuthenticationConfig> jwtAuthenticationConfig)
  {
    _jwtAuthenticationConfig = jwtAuthenticationConfig.Value;
  }

  public string GenerateToken(User user)
  {
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthenticationConfig.Key));

    var claims = new List<Claim>
    {
      new("sub", user.Id.ToString()),
      new("first_name", user.FirstName),
      new("last_name", user.LastName)
    };

    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var jwtSecurityToken = new JwtSecurityToken(
      issuer: _jwtAuthenticationConfig.Issuer,
      audience: _jwtAuthenticationConfig.Audience,
      claims: claims,
      notBefore: DateTime.UtcNow,
      expires: DateTime.UtcNow.AddMinutes(_jwtAuthenticationConfig.LifetimeMinutes),
      signingCredentials: signingCredentials
    );

    var token = new JwtSecurityTokenHandler()
      .WriteToken(jwtSecurityToken);

    return token;
  }

  public bool ValidateToken(string token)
  {
    var key = Encoding.UTF8.GetBytes(_jwtAuthenticationConfig.Key);

    var validationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = _jwtAuthenticationConfig.Issuer,
      ValidAudience = _jwtAuthenticationConfig.Audience,
      IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    try
    {
      new JwtSecurityTokenHandler()
        .ValidateToken(token, validationParameters, out _);

      return true;
    }
    catch
    {
      return false;
    }
  }
}