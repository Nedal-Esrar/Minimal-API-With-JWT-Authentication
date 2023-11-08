namespace MinimalApiWithJwtAuthentication.API.Configurations;

public class JwtAuthenticationConfig
{
  public string Key { get; set; } = string.Empty;

  public string Issuer { get; set; } = string.Empty;

  public string Audience { get; set; } = string.Empty;

  public double LifetimeMinutes { get; set; }
}