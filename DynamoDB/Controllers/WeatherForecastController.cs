using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Mvc;

namespace DynamoDB.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly IDynamoDBContext dynamoDBContext;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(
      IDynamoDBContext dynamoDBContext,
      ILogger<WeatherForecastController> logger)
    {
      this.dynamoDBContext = dynamoDBContext;
      _logger = logger;
    }

    [HttpGet()]
    public async Task<IEnumerable<WeatherForecast>> Get(string city = "Karachi")
    {
      var result = await this.dynamoDBContext
        .QueryAsync<WeatherForecast>(
          city,
          QueryOperator.Between,
          new object[]
          {
            DateTime.UtcNow.Date,
            DateTime.UtcNow.Date.AddDays(1)
          }
        )
        .GetRemainingAsync();

      return result;
    }
    [HttpPost]
    public async Task<string> Post(string city)
    {
      var data = GenerateDummyWeatherForecast(city);
      foreach (var item in data)
      {
        await dynamoDBContext.SaveAsync(item);
        // Chat GPT Example
        //var request = new PutItemRequest
        //{
        //  TableName = "WeatherForecast\r\n",
        //  Item = new Dictionary<string, AttributeValue>
        //  {
        //    { "City", new AttributeValue { N = item.City } },
        //    { "Date", new AttributeValue { S = item.Date } },
        //    { "TemperaureC", new AttributeValue { S = item.TemperatureC } },
        //    { "Summary", new AttributeValue { S = item.Summary } },
        //  }
        //};
        //await client.PutItem(item);
      }
      return "Done";
    }




    private static IEnumerable<WeatherForecast> GenerateDummyWeatherForecast(string city)
    {
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        City = city,
        Date = "02-02-2023", // DateTime.Now.AddDays(index),
        TemperatureC = "-5", // Random.Shared.Next(-20, 55),
        Summary = "My Summary"// Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
    }
  }
}