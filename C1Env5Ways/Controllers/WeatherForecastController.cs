using Microsoft.AspNetCore.Mvc;

namespace C1Env5Ways.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration configuration;

    public WeatherForecastController(
      ILogger<WeatherForecastController> logger,
      IConfiguration configuration
      )
    {
      _logger = logger;
      this.configuration = configuration;
    }

    [HttpGet()]
    public IEnumerable<WeatherForecast> Get()
    {
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
    }

    [HttpGet("demo-config")]
    public Dictionary<string, string> GetDemoConfig()
    {
      return configuration
        .GetSection("Demo")
        .GetChildren()
        .ToDictionary(a => a.Key, a => a.Value);
    }
  }
}