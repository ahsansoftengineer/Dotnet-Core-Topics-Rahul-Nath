using Microsoft.AspNetCore.Mvc;

namespace _5Routing.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TestController : ControllerBase
  {
    /// <summary>
    /// https://localhost:7268/test/name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("{name:alpha?}")]
    public string Get(string name)
    {
      Console.WriteLine(name);
      return $"Hello from Api Controller {name}";
    }
  }
}
