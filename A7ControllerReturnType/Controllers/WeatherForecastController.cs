using Microsoft.AspNetCore.Mvc;

namespace A7ControllerReturnType.Controllers
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WeatherForecast>))]
    public IActionResult Get(string cityName)
    {
      if (string.IsNullOrEmpty(cityName))
        //return new BadRequestObjectResult("Please provide city name");
        return BadRequest("Please provide city name");

      if (cityName == "invalid")
        //return new NotFoundObjectResult("No such city exist in the DB");
        return NotFound("No such city exist in the DB");

      WeatherForecast[] result = NewMethod();

      //return new OkObjectResult(result);
      return Ok(result);
    }



    // To Implicitly Cast Operator Works on Array
    public ActionResult<IEnumerable<WeatherForecast>> GetAll() {
      return NewMethod();
    }
    private static WeatherForecast[] NewMethod()
    {
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      }).ToArray();
    }
  }
}