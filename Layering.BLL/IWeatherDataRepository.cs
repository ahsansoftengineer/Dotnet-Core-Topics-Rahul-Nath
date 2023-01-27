namespace Layering.BLL
{
  public interface IWeatherDataRepository
  {
    IEnumerable<WeatherForecast> Get(string cityName, int numberOfDays);
  }
}