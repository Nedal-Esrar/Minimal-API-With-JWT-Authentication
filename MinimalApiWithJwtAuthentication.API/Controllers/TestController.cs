using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApiWithJwtAuthentication.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class TestController : ControllerBase
{
  [HttpGet]
  public ActionResult<string> GetMessage()
  {
    return Ok("OMAIWA NO TAMASHI GA EIEN NI");
  }
}