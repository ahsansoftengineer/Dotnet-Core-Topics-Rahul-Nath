using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _5AttributeRouting.Controllers
{
  // https://localhost:7033/api
  [Route("api/[controller]")]
  [ApiController]
  public class ExampleController : ControllerBase
  {
    // https://localhost:7033/api/Example1/get

    [HttpGet("gET")]
    public string Get() { return "Get Request"; }

    [HttpGet("[action]")]
    public string ActionName() { return "Get Request"; }

    [HttpGet("abc/[action]/{param:int}")]
    public string AnotherActionName(int param) { return "Get Request"; }

    // Setting up Root Route
    [HttpGet("/[action]/{param}")]
    public string EscapingControoler() { return "Get Request"; }

    [HttpGet("~/[action]/{param}")]
    public string AnotherRootRoute() { return "Get Request"; }


  }
}
