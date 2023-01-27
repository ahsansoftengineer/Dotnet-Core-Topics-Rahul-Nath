using B1SecretManager.Model;
using B1SecretManager.Model.Configuration;
using Microsoft.Extensions.Options;

namespace B1SecretManager.Service
{
  public interface IWeatherService
  {
    Task<string> GetCityData(string cityName);
    Task<IEnumerable<WeatherForecast>> GetDayData(string cityName);
  }

  public class WeatherService : IWeatherService
  {
    private readonly HttpClient _httpClient;
    private readonly ConnectionStrings _cs;

    public WeatherService(
      HttpClient httpClient,
      IOptionsSnapshot<ConnectionStrings> cs
      )
    {
      _httpClient = httpClient;
      this._cs = cs.Value;
    }
    public async Task<string> GetCityData(string cityName)
    {
      string APIURL = $"?key={_cs.WeatherApi.Key}&q={cityName}";
      var response = await _httpClient.GetAsync(APIURL);
      return await response.Content.ReadAsStringAsync();
    }
    public async Task<IEnumerable<WeatherForecast>> GetDayData(string cityName)
    {
      string APIURL = $"?key={_cs.WeatherApi.Key}&q={cityName}&days={20}";
      var response = await _httpClient.GetFromJsonAsync<Root>(APIURL);
      return response.Forecast.Forecastday.Select(x => new WeatherForecast()
      {
        Date = DateTime.Parse(x.Date).Date,
        Summary = x.Day.Condition.Text,
        TemperatureC = x.Day.AvgtempC
      });
    }
  }
}
