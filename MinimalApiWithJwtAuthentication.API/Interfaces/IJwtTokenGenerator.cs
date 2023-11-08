using MinimalApiWithJwtAuthentication.API.Models;

namespace MinimalApiWithJwtAuthentication.API.Interfaces;

public interface IJwtTokenGenerator
{
  string GenerateToken(User user);

  bool ValidateToken(string token);
}