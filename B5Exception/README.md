## EXCEPTION HANDLING

### How to handle Exception from Controller Action?
```c#
[HttpGet(Name = "GetWeatherForecast")]
public ActionResult<IEnumerable<WeatherForecast>> Get(string cityName)
{
  try
  {
    if (cityName == "Sydney")
      throw new Exception("No Weather data for Sydney");

    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
      Date = DateTime.Now.AddDays(index),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
  }
  catch (Exception e)
  {
    return NotFound(e.Message);
  }
}
```
### How to use Middleware for Global Exception?
1. Create a Exception Handling Middleware
```c#
public class ExceptionHandlingMiddleware : IMiddleware
{
public async Task InvokeAsync(HttpContext context, RequestDelegate next)
{
  try
  {
    await next(context);
  }
  catch (ValidationException e)
  {
    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
    await context.Response.WriteAsync($"Error: Middleware: ValidationException {e.Message}");
  }
  catch (DomainNotFoundException e)
  {
    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
    await context.Response.WriteAsync($"Error: Middleware: DomainNotFoundException {e.Message}");
  }
    
  catch (DomainUnHandledException e)
  {
    context.Response.StatusCode = (int)HttpStatusCode.NotExtended;
    await context.Response.WriteAsync($"Error: Middleware: Handling Unhandled Exceptions 500 {e.Message}");
  }
  catch (Exception e)
  {
    context.Response.StatusCode = (int)HttpStatusCode.NotExtended;
    await context.Response.WriteAsync($"Error: Middleware: Exceptions 500 {e.Message}");
  }
}
}
```
2. Registering and Adding it into Middleware
```c#
builder.Services.AddControllers();

// 1. Registering Domain Services
builder.Services.AddTransient<IWeatherService, WeatherService>();

// 2. Registering Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

// 3. Adding Middleware to Pipeline
app.UseMiddleware<ExceptionHandlingMiddleware>();
```
3. Throwing Exception from Controller Action
```c#
[HttpGet("domain-exception")]
public ActionResult<IEnumerable<WeatherForecast>> DomainException(string cityName)
{
  if (cityName == "DomainNotFoundException")
    throw new DomainNotFoundException("Cause by Domain Exception");

  if (cityName == "ValidationException")
    throw new ValidationException("Cause by Validation Exception");

  if (cityName == "DomainUnHandledException")
    throw new DomainUnHandledException("Cause by UnHandled Exception");

  if (cityName == "Exception")
    throw new Exception("Cause by Exception");

  return weatherService.Get(cityName).ToArray();
}
```
4. Handling Exceptions