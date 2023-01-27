namespace DynamoDB
{
  public class WeatherForecast
  {
    public string City { get; set; }  
    public string Date { get; set; }

    public string TemperatureC { get; set; }

    //public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
  }
}