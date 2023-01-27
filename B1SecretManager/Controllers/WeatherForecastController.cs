using B1SecretManager.Model.Configuration;
using B1SecretManager.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace B1SecretManager.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService _weatherService;
    public WeatherForecastController(
      IWeatherService weatherService,
      ILogger<WeatherForecastController> logger
      )
    {
      _weatherService = weatherService;
      _logger = logger;
    }

    [HttpGet("city")]
    public async Task<string> GetCityData(string cityName)
    {
      this._logger.LogInformation("Get Weather Forecast City");
      return await _weatherService.GetCityData(cityName);
    }

    [HttpGet("day")]
    public async Task<IEnumerable<WeatherForecast>> GetDayData(string cityName = "Karachi")
    {
      this._logger.LogInformation("Get Weather Forecast Day");
      return await _weatherService.GetDayData(cityName);
    }
  }

}