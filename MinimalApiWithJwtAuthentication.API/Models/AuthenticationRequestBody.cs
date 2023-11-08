using System.ComponentModel.DataAnnotations;

namespace MinimalApiWithJwtAuthentication.API.Models;

public class AuthenticationRequestBody
{
  [Required] 
  public string Username { get; set; } = string.Empty;

  [Required] 
  public string Password { get; set; } = string.Empty;
}