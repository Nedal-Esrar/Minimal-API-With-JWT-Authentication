using Microsoft.AspNetCore.Mvc;
using MinimalApiWithJwtAuthentication.API.Interfaces;
using MinimalApiWithJwtAuthentication.API.Models;

namespace MinimalApiWithJwtAuthentication.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;

  private readonly IUserRepository _userRepository;

  public AuthenticationController(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
  {
    _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
  }

  [HttpPost("authenticate")]
  public async Task<ActionResult<string>> Authenticate(AuthenticationRequestBody authenticationRequestBody)
  {
    var user = await _userRepository.Get(authenticationRequestBody.Username, authenticationRequestBody.Password);

    if (user is null) return Unauthorized();

    return Ok(_jwtTokenGenerator.GenerateToken(user));
  }
}