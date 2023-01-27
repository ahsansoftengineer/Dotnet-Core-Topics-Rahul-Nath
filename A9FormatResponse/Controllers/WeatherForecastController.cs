using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace A9FormatResponse.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }
    // 1
    [HttpGet("content-type-by-end-point")]
    [Produces("plain/text")]
    public IActionResult ContentTypeByEndPoint()
    {
      // string Text = "Here is my text that I want to return";
      // return Text; // string | content-type = plain/text
      // return Content(Text); // IActionResult | content-type = plain/text
      // return Content(Text); // IActionResult | content-type = plain/text
      string result =  @"
        // string Text = 'Here is my text that I want to return';
        // return Text; // string | content-type = plain/text
        // return Content(Text); // IActionResult | content-type = plain/text
        // return Content(Text); // IActionResult | content-type = plain/text'
      ";
        return Content(result);

    }
    // 2 content-type-by-return-type & Overriding Serialization Behavior
    [HttpGet("content-type-by-return-type")]
    [Produces("application/json")]
    public IActionResult Get()
    {
      return new JsonResult(
        MyData().ToArray(), new JsonOptions()
        {
          JsonSerializerOptions = {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase
          }
        }
       );
    }
    // 3
    [HttpGet("content-type-by-headers")]
    [Produces("application/json", "application/xml")]
    public IEnumerable<WeatherForecast> ContentTypeByHeaders()
    {
      return MyData().ToArray();
    }
    // 4
    [HttpGet("content-type-by-query-param/{format?}")]
    [FormatFilter]
    public IEnumerable<WeatherForecast> ContentTypeByQueryParam()
    {
      return MyData().ToArray();
    }
    

    // DATA
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private static IEnumerable<WeatherForecast> MyData()
    {
      return Enumerable.Range(1, 5)
                .Select(index => new WeatherForecast
                {
                  Date = DateTime.Now.AddDays(index),
                  TemperatureC = Random.Shared.Next(-20, 55),
                  Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                });
    }
  }
}