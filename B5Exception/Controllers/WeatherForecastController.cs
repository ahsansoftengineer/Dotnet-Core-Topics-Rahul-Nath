using B5Exception.Domain;
using B5Exception.Domain.Exception;
using Microsoft.AspNetCore.Mvc;

namespace B5Exception.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService weatherService;

    public WeatherForecastController(
      ILogger<WeatherForecastController> logger,
      IWeatherService weatherService
      )
    {
      _logger = logger;
      this.weatherService = weatherService;
    }
    [HttpGet("try-catch")]
    public ActionResult<IEnumerable<WeatherForecast>> Get(string cityName)
    {
      try
      {
        if (cityName == "Sydney")
          throw new Exception("No Weather data for Sydney");

        return weatherService.Get(cityName).ToArray();
      }
      catch (Exception e)
      {
        return NotFound(e.Message);
      }
    }
    [HttpGet("exception-middleware")]
    public ActionResult<IEnumerable<WeatherForecast>> ExceptionMiddleware(string cityName)
    {
      if (cityName == "ExceptionMiddleware")
        throw new Exception("No Weather data for Sydney");

      return weatherService.Get(cityName).ToArray();
    }

    [HttpGet("domain-exception")]
    public ActionResult<IEnumerable<WeatherForecast>> DomainException(string cityName)
    {

      if (cityName == "DomainNotFoundException")
        throw new DomainNotFoundException("Cause by Domain Exception");

      if (cityName == "ValidationException")
        throw new ValidationException("Cause by Validation Exception");

      if (cityName == "DomainUnHandledException")
        throw new DomainUnHandledException("Cause by UnHandled Exception");

      if (cityName == "Exception")
        throw new Exception("Cause by Exception");

      return weatherService.Get(cityName).ToArray();
    }
  }
  public static class Exceptionz
  {
    public const string DomainNotFoundException = "DomainNotFoundException";
    public const string ValidationException = "ValidationException";
    public const string DomainUnHandledException = "DomainUnHandledException";
    public const string Exception = "Exception";
  }
}