using Layering.BLL;

namespace Layering.DAL
{
  public class DummyWeatherDataRepository : IWeatherDataRepository
  {
    private static readonly string[] Summaries = new[]
 {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    public IEnumerable<WeatherForecast> Get(string cityName, int numberOfDays)
    {
      return GetDummyData(numberOfDays).Select(dto => new WeatherForecast()
      {
        Date = dto.Date,
        Summary = dto.Summary,
        TemperatureC = dto.TemperatureCelcius
      });
    }

    private static IEnumerable<WeatherForecastDto> GetDummyData(int numberOfDays)
    {
      return Enumerable.Range(1, numberOfDays).Select(index => new WeatherForecastDto
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureCelcius = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
     .ToArray();
    }
  }
}
