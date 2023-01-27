using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace A5OptionsPattern.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly WeatherApiOptions _weatherOptions;

    public WeatherForecastController(
      IHttpClientFactory httpClientFactory, 
      ILogger<WeatherForecastController> logger,
      IConfiguration config,
      IOptionsSnapshot<WeatherApiOptions> weatherOptions
      )
    {
      _logger = logger;
      _config = config;
      _httpClientFactory = httpClientFactory;
      _weatherOptions = weatherOptions.Value;
    }

    [HttpGet(Name = "GetAnotherEP")]
    public async Task<string> AnotherEP(string cityName)
    {
      // Configuration Using Strongly Typed
      //var obj = this._config.GetSection("WeatherApi").Get<WeatherApiConfig>();
      //string url = $"{obj.Url}?key={obj.Key}&q={cityName}";

      // Configuration using Reflection Quite Complex
      //var obj = this._config.GetSection("WeatherApi");

      //var Url = obj.GetValue<string>("Url");
      //var Key = obj.GetValue<string>("Key");


      // Drawback of this By Looking at this Controller no one can say what configuration is required
      //var Url = _config.GetValue<string>("WeatherApi:Url");
      //var Key = _config.GetValue<string>("WeatherApi:Key");

      string Url = _weatherOptions.Url;
      string Key = _weatherOptions.Key;

      string url = $"{Url}?key={Key}&q={cityName}";

      using (var client = _httpClientFactory.CreateClient())
      {
        return await client.GetStringAsync(url);
      }
    }
  }
  // Strongly typed configuration with out option pattern
  public class WeatherApiConfig
  {
    public string Key { get; set; }
    public string Url { get; set; }
  }
}