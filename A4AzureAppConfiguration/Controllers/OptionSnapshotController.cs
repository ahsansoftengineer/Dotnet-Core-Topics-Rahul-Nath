using A4AzureAppConfiguration.Configuration;
using A4AzureAppConfiguration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace A4AzureAppConfiguration.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OptionSnapshotController : ControllerBase
  {
    private readonly IOptionsSnapshot<WeatherConfiguration> optionSnapshot;

    public OptionSnapshotController(IOptionsSnapshot<WeatherConfiguration> optionSnapshot)
    {
      this.optionSnapshot = optionSnapshot;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
      var count = optionSnapshot.Value.Count;
      return WeatherService.Get(count);
    }
  }
}