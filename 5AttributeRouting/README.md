## Attribute Routing
### HttpGet vs Route
1. Route is method unspecific, whereas HttpGet obviously implies that only GET requests will be accepted. Generally, you want to use the specific attributes: HttpGet, HttpPost, etc. Route should be used mostly on controllers to specify the base path for all actions in that controller. 
2. The one exception is if you're creating routes for exception handling / status code pages. Then, you should use Route on those actions, since requests via multiple methods could potentially be routed there.
3. Directly / Indirectly Http Action Attributes (HttpGet, HttpPost ...) are also Implements IRouteTemplateProvider

### Controller Level Settings
```c#
[Route("/")] // To Set Route 
[Route("api/[controller]")] // To Set Route Path and Controller Class Name
[Route("api/[controller]/[action]")] // To Set Route Path and Controller Class Name and Action Method Name
```

### How to Globally Set  Splitting ControllerName and ActionMethod 
1. Create a Class that implements IOutboundParameterTransformer
```c#
public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
  public string? TransformOutbound(object? value)
  {
    if (value == null) return null;

    return Regex.Replace(
      value.ToString(),
      "([a-z])([A-Z])",
      "$1-$2",
      RegexOptions.CultureInvariant,
      TimeSpan.FromMilliseconds(100)).ToLowerInvariant();
  }
}
```
2. Then set the Controller Option in Program.cs file
```c#
builder.Services.AddControllers(options =>
{
  options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});
```

### Points for Attribute Routing
1. By Default dotnet Core uses Conventional Routing
2. [Route("api/[controller]")] Takes the Name of the WeatherForecastController => WeatherForecast
3. [HttpGet("[action]")] => GetAllWeather
4. Routes and Controller names are Case Insensitive
5. You can Overload Routes based on HttpVerbs
6. [Route] Attribute can be used on Controller and Action Level as well
7. We can have Several Endpoints / Routes for Same Controller and Action Method
```c#
[Route("api/[controller]")]
[Route("/")]
[ApiController]
public class ExampleController : ControllerBase
{    
  [HttpDelete]
  [HttpDelete("deleto")]
  public string Delete() { return "Delete for Delete Request"; }
}
```
8. Required Parameter
```c#
[HttpGet("abc/[action]/{parameter}")]
```



