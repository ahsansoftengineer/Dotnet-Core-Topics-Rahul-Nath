using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _5AttributeRouting.Controllers
{
  // https://localhost:7033/api
  [Route("/")]
  [Route("api/[controller]")]
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class Example2Controller : ControllerBase
  {
    // https://localhost:7033/api/Example
    [HttpGet]
    public string Get() { return "Get Request"; }

    [HttpPost]
    public string Post() { return "Post for Creating Request"; }

    [HttpPut]
    public string Put() { return "Put Request for full updating"; }

    [HttpPatch]
    public string Patch() { return "Patch for Partial update"; }

    [HttpDelete]
    [HttpDelete("deleto")]
    public string Delete() { return "Delete for Delete Request"; }

  }
}
