## Output Formatters

### What is Content Negociations?
1. 

### What is Output Formatters?
1. In ASP.NET Core, an output formatter is a component that is responsible for serializing the response body before it is sent to the client. The built-in formatters in ASP.NET Core include JSON, XML, and FormUrlEncoded, but you can also create custom formatters to support other types of data.

2. The JSON formatter is used to serialize and deserialize JSON data, while the XML formatter is used to serialize and deserialize XML data. The FormUrlEncoded formatter is used to serialize and deserialize data encoded in the application/x-www-form-urlencoded format, which is commonly used in HTML forms.

3. You can configure the available formatters in the Startup class, in the ConfigureServices method, by calling the AddMvc method and providing the formatters you want to use. For example, the following code configures the JSON and XML formatters:

4. You can also add custom formatters by creating a class that implements the IOutputFormatter interface and adding it to the IMvcBuilder instance returned by the AddMvc method, like this:
#### Custom Formatters for Input & Return type
```c#
services.AddMvc(options =>
{
    options.InputFormatters.Add(new MyCustomFormatter());
    options.OutputFormatters.Add(new MyCustomFormatter());
});
```

### What are the Default Formatter Comes with ASP .Net Core?
```c#
  HttpNoContentOutputFormatter
  StringOutputFormatter
  StreamOutputFormatter
  SystemTextJsonOutputFormatter
```

### To see the Documentation for Default Formatter Click on the following Links
[MvcCoreMvcOptionsSetup.cs](https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.Core/src/Infrastructure/MvcCoreMvcOptionsSetup.cs)
```c#
internal sealed class MvcCoreMvcOptionsSetup : IConfigureOptions<MvcOptions>, IPostConfigureOptions<MvcOptions>
{
  // Set up default output formatters.
  options.OutputFormatters.Add(new HttpNoContentOutputFormatter());
  options.OutputFormatters.Add(new StringOutputFormatter());
  options.OutputFormatters.Add(new StreamOutputFormatter());

  var jsonOutputFormatter = SystemTextJsonOutputFormatter.CreateFormatter(_jsonOptions.Value);
  options.OutputFormatters.Add(jsonOutputFormatter);

  // Set up ValueProviders
  options.ValueProviderFactories.Add(new FormValueProviderFactory());
  options.ValueProviderFactories.Add(new RouteValueProviderFactory());
  options.ValueProviderFactories.Add(new QueryStringValueProviderFactory());
  options.ValueProviderFactories.Add(new JQueryFormValueProviderFactory());
  options.ValueProviderFactories.Add(new FormFileValueProviderFactory());
}
```

### How to Setupup Json Response Variable Camel Case?
```c#
builder.Services
  .AddControllers()
    .AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // Default
      options.JsonSerializerOptions.PropertyNamingPolicy = null; // Changing Default Behaviour
    });

builder.Servicies
  .AddMvc(options =>
    {
        options.RespectBrowserAcceptHeader = true; // respect the Accept header
        options.ReturnHttpNotAcceptable = true; // return 406 Not Acceptable if no suitable formatter is found
    });
```
```json
{
  "Date": "2023-01-11T16:37:36.0305581+05:00",
  "TemperatureC": -5,
  "TemperatureF": 24,
  "Summary": "Sweltering"
}
```

### How to Setting Format Response for Specific Endpoints
```c#
[HttpGet(Name = "GetWeatherForecast")]
[Produces("application/json")]
public IActionResult Get()
{
  return new JsonResult(
    Enumerable.Range(1, 5)
      .Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray(), new JsonOptions() {
        JsonSerializerOptions =
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }
      }
    );
}
```

### What is Newtonsoft.Json?
1. Newtonsoft.Json is a popular library for working with JSON data in .NET. It provides a simple, flexible, and powerful set of features for working with JSON in your .NET applications.

2. One of the key features of Newtonsoft.Json is its ability to automatically serialize and deserialize between .NET objects and JSON data. This means that you can easily convert between .NET objects and JSON strings, and vice versa, with just a few lines of code.