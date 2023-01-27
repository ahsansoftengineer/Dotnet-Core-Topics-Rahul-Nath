using _2DependencyInjection_DI.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace _2DependencyInjection_DI.Controllers
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
    private readonly DependencyService1 dependencyService1;
    private readonly DependencyService2 dependencyService2;

    public WeatherForecastController(
      ILogger<WeatherForecastController> logger,
      DependencyService1 dependencyService1,
      DependencyService2 dependencyService2

      )
    {
      _logger = logger;
      this.dependencyService1 = dependencyService1;
      this.dependencyService2 = dependencyService2;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
      dependencyService1.Write();
      dependencyService2.Write();


      return Enumerable.Empty<WeatherForecast>();
      //Console.WriteLine("Console WeatherForecast Logging");
      //this._logger.LogWarning("Logger is Logging Weather Forecast");
      //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      //{
      //  Date = DateTime.Now.AddDays(index),
      //  TemperatureC = Random.Shared.Next(-20, 55),
      //  Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      //})
      //.ToArray();
    }
  }
}