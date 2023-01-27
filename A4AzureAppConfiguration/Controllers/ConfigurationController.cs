using A4AzureAppConfiguration.Services;
using Microsoft.AspNetCore.Mvc;

namespace A4AzureAppConfiguration.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ConfigurationController : ControllerBase
  {
    private readonly IConfiguration configuration;
    public ConfigurationController(IConfiguration configuration)
    {
      this.configuration = configuration;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
      var count = configuration.GetValue<int>("weather:count");
      var result = configuration.GetSection("weather");
      return WeatherService.Get(count);
    }
  }
}