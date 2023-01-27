# Options Pattern

### What is Options Pattern?
1. The options pattern uses classes to provide strongly typed access to groups of related settings. When configuration settings are isolated by scenario into separate classes, the app adheres to two important software engineering principles:
2. The Options pattern is a design pattern that allows developers to add additional behavior to an object at runtime, without modifying the object's original code. It provides a way to create flexible and extensible software systems by allowing new behavior to be added to existing objects through the use of "option" objects.
3. Options Pattern Compound of Several Principal
- *Encapsulation:* Classes that depend on configuration settings depend only on the configuration settings that they use.
- *Separation of Concerns:* Settings for different parts of the app aren't dependent or coupled to one another.
- *Strategy Pattern* The Options pattern is often used in conjunction with the Strategy pattern, which allows an object to change its behavior based on the options it is given.

### What is Dotnet Options Pattern?
- The .NET Options pattern is a way to configure and manage options and settings for a .NET application in a clean and flexible way. The basic idea behind the pattern is to define options and settings as plain classes, and then use the built-in dependency injection (DI) feature of .NET to provide instances of these classes to the components of the application that need them.
- Here are the basic steps to implement the Options pattern in a .NET application:
1. Define one or more plain classes that represent the options and settings you want to manage. These classes should have properties that correspond to the different settings, and should be decorated with the [Options] attribute.
2. Register the options classes with the DI container. This is typically done in the application's Startup class, in the ConfigureServices method.
3. Inject the options classes into the components that need them. This is done by adding the IOptions<T> interface to the constructor of the component, where T is the options class.
4. When the application runs, the DI container will create an instance of each options class and provide it to the components that have the IOptions<T> interface in their constructor.
6. To configure the options classes, you can use the configuration system in .NET, like reading the values from appsettings.json file or Environment variables, the DI container will automatically load the values into the options classes.
7. By using the options pattern, you can easily add, remove, or change the options and settings in your application without modifying the code that uses them. Additionally, this pattern make the code more loosely coupled and easy to test.

### How to Use Options Pattern for Configuration?
1. Create a Class
```c#
  public class WeatherApiOptions
  {
    public const string WeatherApi = "WeatherApi";
    public string Key { get; set; } 
    public string Url { get; set; }
  }
```
2. Registered Services
```c#
// Registering Weather Api Options
builder.Services.Configure<WeatherApiOptions>(
  builder.Configuration.GetSection(WeatherApiOptions.WeatherApi));
```
3. Using Option Pattern in Controller 
```c#
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly WeatherApiOptions _weatherOptions;

    public WeatherForecastController(
      IHttpClientFactory httpClientFactory, 
      IOptions<WeatherApiOptions> weatherOptions
      )
    {
      _httpClientFactory = httpClientFactory;
      _weatherOptions = weatherOptions.Value;
    }

    [HttpGet(Name = "GetAnotherEP")]
    public async Task<string> AnotherEP(string cityName)
    {
      string Url = _weatherOptions.Url;
      string Key = _weatherOptions.Key;

      string url = $"{Url}?key={Key}&q={cityName}";

      using (var client = _httpClientFactory.CreateClient())
      {
        return await client.GetStringAsync(url);
      }
    }
```
### How to Use IOptionsSnapshot<TOptions>?
- The Only Change is required when Registering Service
```c#
  public WeatherForecastController(
      IHttpClientFactory httpClientFactory, 
      IOptionsSnapshot<WeatherApiOptions> weatherOptions
      )
    {
      _httpClientFactory = httpClientFactory;
      _weatherOptions = weatherOptions.Value;
    }

```

### What is the benefit of using Validation on Configuration?
1. Benefit of using Validation on Configuration is that if you have typo on Configuration then a meaningfull message will be displayed over error
2. Program.cs File Changes
```c#
// https://localhost:7034/WeatherForecast
// 1. Replace the Following Code
//builder.Services.Configure<WeatherApiOptions>(
//  builder.Configuration.GetSection(WeatherApiOptions.WeatherApi));

// 2. Registering Weather Api Options with Validation
builder.Services.AddOptions<WeatherApiOptions>()
  .Bind(builder.Configuration.GetSection(WeatherApiOptions.WeatherApi))
  .ValidateDataAnnotations()
  .ValidateOnStart();
```
3. Add Validation Attribute on the Model
```c#
  public class WeatherApiOptions
  {
    public const string WeatherApi = "WeatherApi";
    [Required]
    [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
    public string Key { get; set; }
    [Required]
    public string Url { get; set; }
  }
```
### What are the Different Ways of Utilizing Option Pattern?
1. There are three ways of Utilizing Option Pattern

1. IOptions<TOptions>:
- Does not support: 
- Reading of configuration data after the app has started.
- Named options, 
- Is registered as a Singleton and can be injected into any service lifetime.

2. IOptionsSnapshot<TOptions>: 
- Is useful in scenarios where options should be recomputed on every request. For more information, see Use IOptionsSnapshot to read updated data.
- Is registered as Scoped and therefore can't be injected into a Singleton service.
- Supports named options

3. IOptionsMonitor<TOptions>:
- Is used to retrieve options and manage options notifications for TOptions instances.
- Is registered as a Singleton and can be injected into any service lifetime.
- Supports: 
- Change notifications
- named options
- Reloadable configuration
- Selective options invalidation (IOptionsMonitorCache<TOptions>)


1. Here's an example from Chat Bot

```c#
class WebServer {
  private options: ServerOptions;

  constructor(options: ServerOptions) {
    this.options = options;
  }

  start() {
    // Use the options to configure the server
  }
}

interface ServerOptions {
  port: number;
  host: string;
  ssl?: boolean;
  auth?: AuthOptions;
}

interface AuthOptions {
  username: string;
  password: string;
}

const options: ServerOptions = {
  port: 8080,
  host: 'localhost',
  ssl: true,
  auth: {
    username: 'admin',
    password: 'secret',
  },
};

const server = new WebServer(options);
server.start();
````
### Conclusion
1. In this example, the WebServer class takes a ServerOptions object in its constructor. The ServerOptions object contains all of the necessary configuration options for the server, including the port and host to bind to, and optional SSL and authentication settings. By using the Options pattern, the WebServer class can be configured with a wide variety of options, without the need to modify its code.

### What is Strategy Pattern?
1. The Strategy pattern is a design pattern that allows an object to change its behavior based on a given set of strategies or algorithms. It provides a way to create flexible and extensible software systems by allowing new algorithms to be added to an object through the use of "strategy" objects.

2. The Strategy pattern is often used in conjunction with the Options pattern, which allows an object to be configured with a set of options at runtime.

```c#class WebServer {
  private strategy: RequestHandlerStrategy;

  constructor(strategy: RequestHandlerStrategy) {
    this.strategy = strategy;
  }

  handleRequest(request: Request) {
    this.strategy.handleRequest(request);
  }
}

interface RequestHandlerStrategy {
  handleRequest(request: Request): void;
}

class StaticFileHandler implements RequestHandlerStrategy {
  handleRequest(request: Request) {
    // Handle the request by serving a static file
  }
}

class DynamicContentHandler implements RequestHandlerStrategy {
  handleRequest(request: Request) {
    // Handle the request by generating dynamic content
  }
}

const server = new WebServer(new StaticFileHandler());
server.handleRequest(request);

```
1. In this example, the WebServer class takes a RequestHandlerStrategy object in its constructor. The RequestHandlerStrategy object defines an interface for handling requests, and two different implementations are provided: StaticFileHandler and DynamicContentHandler. By using the Strategy pattern, the WebServer class can be configured to use different request-handling strategies depending on the needs of the application.