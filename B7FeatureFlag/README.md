## FEATURE FLAG

### What is Feature Flag?
1. .NET Core Feature Flags, also known as feature toggles or feature switches, are a technique used to enable or disable specific features of an application at runtime. 
2. This allows for the ability to test new features in a production environment without affecting the entire system, and also allows for the ability to quickly turn off a feature that is causing issues. 
3. This can be done through the use of configuration settings, environment variables, or programmatic controls. The Microsoft.FeatureManagement library can be used to implement feature flags in .NET Core applications.
4. This package has the ability Microsoft.FeatureManagement.AspNetCore to enable feature Flag in dotnet core

### How to use Feature Flag
- It is very much similar to Configuration, You can store Feature Flag at all this locations where Configuration saves
1. Add **Microsoft.FeatureManagement.AspNetCore** Library
2. Add Service to Container
```c#
builder.Services.AddFeatureManagement();
```
3. Adding keys int the appsetting.json
```json
  "FeatureManagement": {
    "RainEnable":  false
  }
```
4. Utilizing in the Controller
```c#
[HttpGet]
public async Task<IEnumerable<WeatherForecast>> Get()
{
  var isRainEnable = await featureManager.IsEnabledAsync("RainEnable");
  return Enumerable.Range(1, 5).Select(index => new WeatherForecast
  {
    Date = DateTime.Now.AddDays(index),
    TemperatureC = Random.Shared.Next(-20, 55),
    RainExpected = isRainEnable ? $"{Random.Shared.Next(0, 100)}%" : null,
    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
  })
  .ToArray();
}
```
5. Skipping the null values from Array
```c#
builder
  .Services
  .AddControllers()
  .AddJsonOptions(options =>
  {
    // When you intention to skip null values from result
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
  });
```

### How to Setup Feature Flag from a Controller Action
- To Skip a Controller Action we use Feature Attribute to Hide from Accessing this Endpoint
- This will be displayed in Swagger but when you hit the route then you will get 404
```c#
[HttpGet("FeatureRouteHere")]
[FeatureGate("ShowRoute")]
public async Task<IEnumerable<WeatherForecast>> FeatureRouteHere()
{
  return await NewMethod();
}
```

### We can Use Feature Flag for
- For Controllers
- For Middleware
- For Filters
- For Properties 
- etc..

### How to Use Feature Flag to control the execution of code percentage wise result
- This Feature Flag is really interesting you can utilized it for
- Feature Enabled for Targeting (Users, Groups, Roles etc...)
- Feature Enabled as per Time
- Changing Code Algorithm
- Percentage wise Generating Result
- Default Roll Out Percentage when none of options meets the criteria


1. Program.cs
```c#
builder.Services
  .AddFeatureManagement()
  .AddFeatureFilter<PercentageFilter>(); // <=
```
2. appsettings.json
```json
"FeatureManagement": {
  "AlgorithmSwitching": {
    "EnabledFor": [
      {
        "Name": "Microsoft.Percentage",
        "Parameters": {
          "Value": 50
        }
      }
    ]
  }
```
3. Controller Action
```c#
[HttpGet("[action]")]
public async Task<IEnumerable<WeatherForecast>> AlgorithmSwitchingHere()
{
  var AlgorithmSwitching = await featureManager.IsEnabledAsync("AlgorithmSwitching");
  if (AlgorithmSwitching)
  {
    _logger.LogWarning("Warning is 50% Executing ");
    return await NewMethod("Warn");
  }
  else
  {
    _logger.LogInformation("Information is 50% Executing");
    return await NewMethod("Info");
  }
}
```

### How to use Customize Feature Flag with Custom Filter?
1. Create a BrowserFilter Class
```c#
public class BrowserFilter : IFeatureFilter
{
  private readonly IHttpContextAccessor httpContextAccessor;
  public BrowserFilter(IHttpContextAccessor httpContextAccessor)
  {
    this.httpContextAccessor = httpContextAccessor;
  }
  public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
  {
    if (httpContextAccessor.HttpContext != null)
    {
      var userAgent = httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString();
      var settings = context.Parameters.Get<BrowserFilterSettins>();
      return Task.FromResult(settings.Allowed.Any(a => userAgent.Contains(a)));
    }
    return Task.FromResult(false);
  }
}
public class BrowserFilterSettins
{
  public string[] Allowed { get; set; }
}
```
2. appsettings.json
```json
"FeatureManagement": {
  "RainEnable": true,
  "ShowRoute": true,
  "AlgorithmSwitching": {
    "EnabledFor": [
      // For Other Browser Percentage 50% Console.Info
      {
        "Name": "Microsoft.Percentage",
        "Parameters": {
          "Value": 50
        }
      },
      // For Edg Browser 100% Console.Warn
      {
        "Name": "Browser",
        "Parameters": {
          "Allowed": [ "Edg" ]
        }
      }
    ]
  }
}
```
3. Program.cs
```c#
builder.Services.AddHttpContextAccessor();

builder.Services
  .AddFeatureManagement()
  .AddFeatureFilter<PercentageFilter>()
  .AddFeatureFilter<BrowserFilter>();
```

### What is the difference between Azure Access Key & Azure AD?
1. The "Switch Access Key" in Azure refers to the ability to change the access keys for an Azure storage account. This can be useful for rotating keys, for example, as part of a security best practice.

2. "Switch Azure AD" refers to the ability to switch between different Azure Active Directory tenants when working with Azure services. This can be useful for organizations that have multiple Azure AD tenants, such as for different business units or geographic regions.

### How to Use Feature Flag with Azure App Configuration?
1. Install