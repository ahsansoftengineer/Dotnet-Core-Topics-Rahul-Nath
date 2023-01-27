using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace B7FeatureFlag.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly IFeatureManager featureManager;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(
      IFeatureManager featureManager,
      ILogger<WeatherForecastController> logger
      )
    {
      this.featureManager = featureManager;
      _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
      return await NewMethod();
    }

    [HttpGet("[action]")]
    [FeatureGate("ShowRoute")]
    public async Task<IEnumerable<WeatherForecast>> FeatureRouteHere()
    {
      return await NewMethod();
    }

    [HttpGet("[action]")]
    public async Task<IEnumerable<WeatherForecast>> AlgorithmSwitchingHere()
    {
      var AlgorithmSwitching = await featureManager.IsEnabledAsync("AlgorithmSwitching");
      if (AlgorithmSwitching)
      {
        _logger.LogWarning("Warning is 50% Executing ");
        return await NewMethod("Warn");
      }
      else
      {
        _logger.LogInformation("Information is 50% Executing");
        return await NewMethod("Info");
      }


    }
    private async Task<IEnumerable<WeatherForecast>> NewMethod(string status = null)
    {
      var isRainEnable = await featureManager.IsEnabledAsync("RainEnable");
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        RainExpected = isRainEnable ? $"{Random.Shared.Next(0, 100)}%" : null,
        Summary = Summaries[Random.Shared.Next(Summaries.Length)],
        Status = status

      })
      .ToArray();
    }

  }
}