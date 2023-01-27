using Microsoft.AspNetCore.Mvc;

namespace _9HttpClient.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly string URL = $"https://api.weatherapi.com/v1/current.json?key=4602e38d8e454580b5b61510230201&q={"London"}&aqi=no";

    // Way 3 | HttpClient using Static Constructor
    //private static readonly HttpClient _httpClient;
    //static WeatherForecastController()
    //{
    //  _httpClient = new HttpClient();
    //}

    // Way 4 | HttpClient using HttpClientFactory
    //private readonly IHttpClientFactory _httpClientFactory;
    //public WeatherForecastController(IHttpClientFactory httpClientFactory)
    //{
    //  _httpClientFactory = httpClientFactory;
    //}
    // Way 5 | HttpClient using Service
    private IWeatherService _weatherService;
    public WeatherForecastController(IWeatherService weatherService)
    {
      _weatherService = weatherService;
    }



    [HttpGet(Name = "GetWeatherForecast")]
    // Way 1 Has Drawback
    //public async Task<string> Get(string cityName)
    //{
    //  var _httpClient = new HttpClient();
    //  var response = await _httpClient.GetAsync(URL);
    //  return await response.Content.ReadAsStringAsync();
    //}

    // Way 2 
    //public async Task<string> Get(string cityName)
    //{
    //  using (var client = new HttpClient())
    //  {
    //    var response = await client.GetAsync(URL);
    //    return await response.Content.ReadAsStringAsync();
    //  }
    //}

    // Way 3 httpClient using Static Constructor
    //public async Task<string> Get(string cityName)
    //{
    //  var response = await _httpClient.GetAsync(URL);
    //  return await response.Content.ReadAsStringAsync();
    //}

    // Way 4 Creating httpClient using Factory
    //public async Task<string> Get(string cityName)
    //{
    //  var httpClient = _httpClientFactory.CreateClient("weather");
    //  var url = $"?key=4602e38d8e454580b5b61510230201&q={"London"}&aqi=no";
    //  var response = await httpClient.GetAsync(url);
    //  return await response.Content.ReadAsStringAsync();
    //}

    // Way 5 
    public async Task<string> Get(string cityName)
    {
      return await _weatherService.Get(cityName);
    }
  }
}