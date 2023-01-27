using System.ComponentModel.DataAnnotations;

namespace A5OptionsPattern
{
  public class WeatherApiOptions
  {
    public const string WeatherApi = "WeatherApi";
    [Required]
    public string Key { get; set; }
    [Required]
    public string Url { get; set; }
  }
}
