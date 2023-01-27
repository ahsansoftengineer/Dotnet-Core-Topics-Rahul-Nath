## ROUTING
### UseRouting VS UseEndpoints
1. Routing uses a pair of middleware, registered by UseRouting and UseEndpoints:
2. Always use More Specific Endpoints Earlier

2. **Use0Routing** adds route matching to the middleware pipeline. - This middleware looks at the set of endpoints defined in the app, and selects the best match based on the request
- UseRouting Middleware add the functionality to application for Routing and HttpVerbs
```c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  app.UseRouting();

  app.Use(async (context, next) =>
  {
    var endpoint = context.GetEndpoint();
    await next();
  });
  // To Setup the endpoints
  // The most specific endpoints comes earlier
  app.UseEndpoints(endpoints =>
  {
    // Mapping to Route
    endpoints.MapGet("/", async context =>
    {
      await context.Response.WriteAsync("Hello!");
    });
  }
}
```
3. **UseEndpoints** adds endpoint execution to the middleware pipeline. It runs the delegate associated with the selected endpoint.
3. 
```c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  // Always use more specific endpoints earlier
  app.UseEndpoints(endpoints =>
  {
    // Mapping to Route
    endpoints.MapGet("/hello", async context =>
    {
      await context.Response.WriteAsync("Hello!");
    });
    endpoints.MapGet("/", async context =>
    {
      await context.Response.WriteAsync("Hello World!");
    });

  });
}

```

### How to Define a Custom Constraints?
1. Create a Custom Constraint by Implementing IRouteConstraint to a Class
2. Dotnet Default Constraint (string, int, bool, datetime, decimal, double, guid, required, range, alpha, regex, etc...)
```c#
public class TestRouteConstraint : IRouteConstraint
{
  public bool Match(
    HttpContext? httpContext, 
    IRouter? route, string routeKey, 
    RouteValueDictionary values, 
    RouteDirection routeDirection
    )
  {
    if(values.TryGetValue(routeKey, out object value))
    {
      if(value is string stringValue)
      {
        return stringValue.StartsWith("0");
      }
    }
    return false;
  }
}
```
2. Adding Custom Constraint to Services Container
```c#
public void ConfigureServices(IServiceCollection services)
{
  services.AddRouting(options =>
  {
    options.ConstraintMap.Add("test-route-constraint", typeof(TestRouteConstraint));
  });
}
```
3. Utilizing Constraint to Endpoints
```c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  if (env.IsDevelopment())
  {
    app.UseDeveloperExceptionPage();
  }

  app.UseRouting();
  // Adding Middleware to understand Which Endpoint going to hit
  // For This middleware to Work app.UseRouting(); called earlier
  app.Use(async (context, next) =>
  {
    var endpoint = context.GetEndpoint();
    await next();
  });
  // To Setup the endpoints
  // The most specific endpoints comes earlier
  app.UseEndpoints(endpoints =>
  {
    // endpoints.MapGet("/users/{userId}/books/{bookId}", 
    //    (int userId, int bookId) => $"The user id is {userId} and book id is {bookId}");

    // Mapping to Default Constraint => https://localhost:7268/default-constraint/asdfs
    endpoints.MapGet("/default-constraint/{param1:alpha:minlength(2):maxlength(5)}", async context =>
    {
      var value = context.GetRouteValue("param1");
      await context.Response.WriteAsync($"Hello From Default Constraint - {value}");
    });

    // Mapping to Custom Constraint => https://localhost:7268/custom-constraint/0Ahsan
    endpoints.MapGet("/custom-constraint/{param1:test-route-constraint}", async context =>
    {
      // This Constraint Checks Weather the value starts from Leading Zero "0"
      var value = context.GetRouteValue("param1");
      await context.Response.WriteAsync($"Hello From Custom Constraint - {value}");
    });
  });
}
```
### What are the Types of Routing .NET Core?
- ASP .net Core has two types of routing
1. Convention Based Routing -> Routing Based on Controller and Methods Name and their Naming Convention
2. Attribute Routing -> Using Attributes To Map Classes as Controller and their Methods as Endpoints
3. You can also use Mixed Mode (Convention Based Routing & Attribute Routing at the same time)

### How to Use Api Controller in Dotnet?
1. Create a Controller with Action Method
```c#
using Microsoft.AspNetCore.Mvc;
namespace _5Routing.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TestController : ControllerBase
  {
    // https://localhost:7268/test/name
    [HttpGet("{name:alpha?}")]
    public string Get(string name)
    {
      Console.WriteLine(name);
      return $"Hello from Api Controller {name}";
    }
  }
}
```
2. Register Service in Startup.cs
```c#
public void ConfigureServices(IServiceCollection services)
{
  services.AddControllers();
}
```
3. Provide Configuration for All Controllers
```c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  if (env.IsDevelopment())
  {
    app.UseDeveloperExceptionPage();
  }

  app.UseRouting();
  // Adding Middleware to understand Which Endpoint going to hit
  // For This middleware to Work app.UseRouting(); called earlier
  app.Use(async (context, next) =>
  {
    var endpoint = context.GetEndpoint();
    Console.WriteLine(endpoint);
    await next();
  });
  app.UseEndpoints(endpoints =>
  {
    endpoints.MapGet("/", async context =>
    {
      await context.Response.WriteAsync("Hello World!");
    });
    endpoints.MapControllers();

  });
}
```

