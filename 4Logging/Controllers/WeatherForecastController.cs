using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace _4Logging.Controllers
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

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
      // Logging with Log Levels
      //_logger.Log(LogLevel.Information, "LogLevel.Information");
      //_logger.Log(LogLevel.Warning, "LogLevel.Warning");
      //_logger.Log(LogLevel.Debug, "LogLevel.Debug");
      //_logger.Log(LogLevel.Trace, "LogLevel.Trace");
      //_logger.Log(LogLevel.Critical, "LogLevel.Critical");
      //_logger.Log(LogLevel.None, "LogLevel.None");
      //try
      //{
      //  throw new Exception("This is Throw Exception");
      //} catch(Exception ex)
      //{
      //  _logger.Log(LogLevel.Error, ex, "LogLevel.Error");
      //}



      // This Methods doesn't required LogLevel
      //_logger.LogNone("LogLevel.None"); // Doesn't Exsits
      _logger.LogTrace("_logger.LogTrace");
      _logger.LogDebug("_logger.LogDebug Ahsan");
      _logger.LogCritical("_logger.LogCritical");
      _logger.LogInformation("_logger.LogInformation");
      DateTime now = DateTime.Now;
      string strDate = now.ToString("dd-MM-yyyy mm:mm:s");
      _logger.LogWarning("_logger.LogWarning {p1}, {p2}", "MyArgs", strDate);
      using(_logger.BeginScope(
        new Dictionary<string, object> { { "PersonId", 5 } })
      )
      {
        _logger.LogInformation("Hello");
        _logger.LogInformation("World");

      }
      try
      {
        throw new Exception("This is Custom Exception");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "_logger.LogError");
      }
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
    }
    
  }
}