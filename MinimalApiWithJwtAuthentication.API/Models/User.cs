namespace MinimalApiWithJwtAuthentication.API.Models;

public class User
{
  public int Id { get; set; }

  public string FirstName { get; set; } = string.Empty;

  public string LastName { get; set; } = string.Empty;

  public string Username { get; set; } = string.Empty;

  public string Password { get; set; } = string.Empty;
}