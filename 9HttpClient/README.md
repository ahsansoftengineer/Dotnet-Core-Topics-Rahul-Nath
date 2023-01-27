## HttpClient

### What is HttpClient?
1. System.Net.Http.HttpClient is a class in the .NET Standard library that provides a way to send HTTP requests and receive HTTP responses from a resource identified by a URI. 
2. It can be used to communicate with an HTTP server, such as a web server or API endpoint. 
3. It is designed to be easy to use and handle common tasks, such as sending GET and POST requests, uploading files, and parsing HTTP responses. 
4. The HttpClient class is part of the System.Net.Http namespace and can be used in any .NET application, including ASP.NET, Windows Forms, and console applications.
### Simple Example 
```c#
class Program
{
    static async Task Main(string[] args)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync("http://www.example.com");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
    }
}
```

### Commands
```c#
    ping api.weatherapi.com
    netstat -ano | findstr 138.199.46.65
```

### How to create a Singleton Connection of HttpClient
0. Solve the problem of making Connection again and again for every Request.
1. HttpClient Creating Singleton Connection
2. It has a draw back if the DNS Changes then it will fail to load data from External Resource
3. 
```c#

private static readonly HttpClient _httpClient;

// Static Constructor
static WeatherForecastController()
{
    _httpClient = new HttpClient();
}

[HttpGet(Name = "GetWeatherForecast")]
public async Task<string> Get(string cityName)
{
    string URL = $"https://api.weatherapi.com/v1/current.json?key=4602e38d8e454580b5b61510230201&q={cityName}&aqi=no";
    var response = await _httpClient.GetAsync(URL);
    return await response.Content.ReadAsStringAsync();
}
```

### If your requirement is to Create Object every time connection and drop it
```c#
[HttpGet(Name = "GetWeatherForecast")]
public async Task<string> Get(string cityName)
{
    string URL = $"https://api.weatherapi.com/v1/current.json?key=4602e38d8e454580b5b61510230201&q={cityName}
    using (var client = new HttpClient())
    {
    var response = await client.GetAsync(URL);
    return await response.Content.ReadAsStringAsync();
    }
}
```
### Benefits of using IHttpClientFactory
- The current implementation of IHttpClientFactory, that also implements IHttpMessageHandlerFactory, offers the following benefits:

- Provides a central location for naming and configuring logical HttpClient objects. For example, you may configure a client (Service Agent) that's pre-configured to access a specific microservice.
- Codify the concept of outgoing middleware via delegating handlers in HttpClient and implementing Polly-based middleware to take advantage of Polly's policies for resiliency.
- HttpClient already has the concept of delegating handlers that could be linked together for outgoing HTTP requests. You can register HTTP clients into the factory and you can use a Polly handler to use Polly policies for Retry, CircuitBreakers, and so on.
- Manage the lifetime of HttpMessageHandler to avoid the mentioned problems/issues that can occur when managing HttpClient lifetimes yourself.


### HttpClient Factory Example
1. Add HttpClient to DI (Program.cs)
```c#
builder.Services.AddHttpClient("weather", c =>
{
  c.BaseAddress = new Uri("http://api.weatherapi.com/v1/current.json");
});
```
2. Utilize in Controller
```c#
private readonly IHttpClientFactory _httpClientFactory;

public WeatherForecastController(IHttpClientFactory httpClientFactory)
{
    _httpClientFactory = httpClientFactory;
}

[HttpGet(Name = "GetWeatherForecast")]
public async Task<string> Get(string cityName)
{
    var httpClient = _httpClientFactory.CreateClient("weather");
    var url = $"?key=4602e38d8e454580b5b61510230201&q={"London"}&aqi=no";
    var response = await httpClient.GetAsync(url);
    return await response.Content.ReadAsStringAsync();
}
```

### Using HttpClient using DI
1. Interface & Service Implementation
```c#
public interface IWeatherService
{
    Task<string> Get(string cityName);
}

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> Get(string cityName)
    {
        var apiKey = "4602e38d8e454580b5b61510230201";
        string APIURL = $"?key={apiKey}&q={cityName}";
        var response = await _httpClient.GetAsync(APIURL);
        return await response.Content.ReadAsStringAsync();
    }
```

2. Registering Service Program.cs
```c#
builder.Services.AddHttpClient<IWeatherService, WeatherService>(c =>
{
   c.BaseAddress = new Uri("http://api.weatherapi.com/v1/current.json");
});
```

3. Controller Implementation
```c#
private IWeatherService _weatherService;
public WeatherForecastController(IWeatherService weatherService)
{
    _weatherService = weatherService;
}

[HttpGet(Name = "GetWeatherForecast")]
public async Task<string> Get(string cityName)
{
    return await _weatherService.Get(cityName);
}

```

### Delegating Handler
- Typically, a series of message handlers are chained together. The first handler receives an HTTP request, does some processing, and gives the request to the next handler. At some point, the response is created and goes back up the chain. This pattern is called a delegating handler.