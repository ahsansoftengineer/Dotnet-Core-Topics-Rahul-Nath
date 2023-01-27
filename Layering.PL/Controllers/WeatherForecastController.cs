using Layering.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Layering.PL.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly IWeatherForecastService weatherService;

    public WeatherForecastController(IWeatherForecastService weatherService)
    {
      this.weatherService = weatherService;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get(string cityName = "London", int numberOfDays = 3)
    {
      return weatherService.Get(cityName, numberOfDays);

    }
  }
}