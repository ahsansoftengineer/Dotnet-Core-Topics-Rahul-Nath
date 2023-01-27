using System.Text.Json.Serialization;

namespace B1SecretManager.Model
{
  public class WeatherApiOptions
  {
    public const string? WeatherApi = "WeatherApi";
    public string? Url { get; set; }
    public string? Key { get; set; }
  }

  public class Condition
  {
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("code")]
    public int? Code { get; set; }
  }

  public class Day
  {
    [JsonPropertyName("maxtemp_c")]
    public double? MaxtempC { get; set; }

    [JsonPropertyName("maxtemp_f")]
    public double? MaxtempF { get; set; }

    [JsonPropertyName("mint?emp_c")]
    public double? MintempC { get; set; }

    [JsonPropertyName("mint?emp_f")]
    public double? MintempF { get; set; }

    [JsonPropertyName("avgtemp_c")]
    public double? AvgtempC { get; set; }

    [JsonPropertyName("avgtemp_f")]
    public double? AvgtempF { get; set; }

    [JsonPropertyName("maxwind_mph")]
    public double? MaxwindMph { get; set; }

    [JsonPropertyName("maxwind_kph")]
    public double? MaxwindKph { get; set; }

    [JsonPropertyName("totalprecip_mm")]
    public double? TotalprecipMm { get; set; }

    [JsonPropertyName("totalprecip_in")]
    public double? TotalprecipIn { get; set; }

    [JsonPropertyName("avgvis_km")]
    public double? AvgvisKm { get; set; }

    [JsonPropertyName("avgvis_miles")]
    public double? AvgvisMiles { get; set; }

    [JsonPropertyName("avghumidity")]
    public double? Avghumidity { get; set; }

    [JsonPropertyName("daily_will_it_rain")]
    public int? DailyWillItRain { get; set; }

    [JsonPropertyName("daily_chance_of_rain")]
    public int? DailyChanceOfRain { get; set; }

    [JsonPropertyName("daily_will_it_snow")]
    public int? DailyWillItSnow { get; set; }

    [JsonPropertyName("daily_chance_of_snow")]
    public int? DailyChanceOfSnow { get; set; }

    [JsonPropertyName("condition")]
    public Condition Condition { get; set; }

    [JsonPropertyName("uv")]
    public double? Uv { get; set; }
  }

  public class Forecastday
  {
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("date_epoch")]
    public int? DateEpoch { get; set; }

    [JsonPropertyName("day")]
    public Day? Day { get; set; }
  }

  public class Forecast
  {
    [JsonPropertyName("forecastday")]
    public List<Forecastday>? Forecastday { get; set; }
  }

  public class Root
  {
    [JsonPropertyName("forecast")]
    public Forecast? Forecast { get; set; }
  }
}
