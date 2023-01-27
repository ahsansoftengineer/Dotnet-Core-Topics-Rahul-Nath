# Logging
### What is Logging
1. Logging is an essential feature in applications for detecting or investigating issues. 
2. ASP.Net Core is an open source, cross-platform, lean, and modular framework for building high-performance web applications. 
3. Logging is now a first-class citizen in ASP.Net—support

### Where we cannot not use Logging?
1. We Cannot use Logging before DI Initialization because it is a Service
2. We cannot use Logger in Program.cs
3.  
### What is Logging Provider
1. Logging providers persist logs, except for the Console provider, which only displays logs as standard output. 
2. For example, the Azure Application Insights provider stores logs in Azure Application Insights. Multiple providers can be enabled.

### What are the parameters of Logger?
- logger -> Extension
- logLevel -> Entry will be written on this level. (LogLevel.Error)
- eventId -> The event id associated with the log. (10001)
- exception -> The exception to log. (try, catch(e))
- message -> Format string of the log message. (Your Informative Message)
- args -> An object array that contains zero or more objects to format. (???)

### What resources Logger can use?
By default ASP .Net Core Logging uses following logging providers:
1. Console
2. Debug
3. EventSource
4. EventLog (Windows only)

### How to Configure Customize Logging Providers?
1. By Default ILogger is included in Project by ASP .NET CORE
2. First we have to Inject ILogger in Class as Dependency Injection
```c#
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }
```
3. Logger Provide Several type for Logging
```c#
// This Methods doesn't required LogLevel
_logger.LogInformation("_logger.LogInformation");
_logger.LogWarning("_logger.LogWarning {p1}, {p2}", "MyArgs", 10001);
_logger.LogDebug("_logger.LogDebug");
_logger.LogTrace("_logger.LogTrace");
_logger.LogCritical("_logger.LogCritical");
//_logger.LogNone("LogLevel.None"); // Doesn't Exsits
try
{
throw new Exception("This is Custom Exception");
}
catch (Exception ex)
{
_logger.LogError(ex, "_logger.LogError");
}
```
### Note:

### What is Seq in Dotnet Core?
1. Seq is the intelligent search, analysis, and alerting server built specifically for modern structured log data.
2. You can use it to create the visibility you need to quickly identify and diagnose problems in complex applications and microservices.

### How to Implement Structured Logging in DotNet Core? Or
### How to setup for SEQ?
1. Download Package using Nuget (Seq.Extension.Logging)    
2. Download Seq App From [Seq](https://datalust.co/download)
3. Add Seq to DI Container > Program.cs > builder.Logging.AddSeq();
4. Run the Seq Service Admininstrator
5. Run Your Application Ctrl + F5 OR F5
6. By Default Seq Uses [Port](http://localhost:5341/)

### What is Purpose of "Microsoft.AspNetCore" in Logging?
- The "Default" and "Microsoft.AspNetCore" categories are specified.
- The "Microsoft.AspNetCore" category applies to all categories that start with "Microsoft.AspNetCore". 
- For example, this setting applies to the "Microsoft.AspNetCore.Routing.EndpointMiddleware" category.
- "Microsoft.Hosting.Lifetime": "Trace",
- "logging.Controller.WeatherForecastController": "Trace"


### What are the Logging Precedence?
- Trace = 0, Debug = 1, Information = 2, Warning = 3, Error = 4, Critical = 5, and None = 6.
- When a LogLevel is specified, logging is enabled for messages at the specified level and higher. 
- In the preceding JSON, the Default category is logged for Information and higher. 
- For example, Information, Warning, Error, and Critical messages are logged. 
- If no LogLevel is specified, logging defaults to the Information level. For more information, see Log levels.

### How to Set Logging Level?
```c#
{
    "LogLevel": {
        "Default": "Trace",
        "Microsoft": "Trace",
        "Microsoft.AspNetCore": "Trace",
        "Microsoft.Hosting.Lifetime": "Trace",
        "logging.Controller.WeatherForecastController": "Trace"
    },
    "Seq": {
        "Default": "Trace",
        "Microsoft": "Trace",
        "Microsoft.AspNetCore": "Trace",
        "Microsoft.Hosting.Lifetime": "Trace",
        "logging.Controller.WeatherForecastController": "Trace"
    }
}

```