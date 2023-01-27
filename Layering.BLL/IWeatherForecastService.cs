namespace Layering.BLL
{
  public interface IWeatherForecastService
  {
    IEnumerable<WeatherForecast> Get(string cityName, int numberOfDays);
  }
}