namespace A1PollyResiliency.Service
{
  public interface IWeatherService
  {
    Task<string> Get(string cityName);
  }
  public class WeatherService : IWeatherService
  {
    private readonly HttpClient _httpClient;
    public WeatherService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }
    public async Task<string> Get(string cityName)
    {
      var apiKey = "4602e38d8e454580b5b61510230201";
      string APIURL = $"?key={apiKey}&q={cityName}";
      var response = await _httpClient.GetAsync(APIURL);
      return await response.Content.ReadAsStringAsync();
    }
  }
}

