using A1PollyResiliency.Service;
using Microsoft.AspNetCore.Mvc;

namespace A1PollyResiliency.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private IWeatherService _weatherService;
    public WeatherForecastController(IWeatherService weatherService)
    {
      _weatherService = weatherService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<string> Get(string cityName)
    {
      return await _weatherService.Get(cityName);
    }
  }
}