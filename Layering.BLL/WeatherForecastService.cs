namespace Layering.BLL
{
  public class WeatherForecastService : IWeatherForecastService
  {
    private readonly IWeatherDataRepository repo;

    public WeatherForecastService(IWeatherDataRepository repo)
    {
      this.repo = repo;
    }
    public IEnumerable<WeatherForecast> Get(string cityName, int numberOfDays)
    {
      return repo.Get(cityName, numberOfDays);
    }
  }
}
