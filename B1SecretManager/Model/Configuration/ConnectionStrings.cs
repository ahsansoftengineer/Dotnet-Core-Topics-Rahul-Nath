namespace B1SecretManager.Model.Configuration
{
  public class ConnectionStrings
  {
    public readonly static string connectionStrings = "ConnectionStrings";
    public WeatherApi WeatherApi { get; set; }
  }
  public class WeatherApi
  {
    public readonly static string weatherApi = "WeatherApi";
    public string Key { get; set; }
    public string UrlForecast { get; set; }
    public string UrlCurrent { get; set; }
  }
}
