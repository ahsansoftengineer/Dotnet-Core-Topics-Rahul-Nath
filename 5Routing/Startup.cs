namespace _5Routing
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRouting(options =>
      {
        options.ConstraintMap.Add("test-route-constraint", typeof(TestRouteConstraint));
      });
      services.AddControllers();

    }
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
      // To Setup the endpoints
      // The most specific endpoints comes earlier
      app.UseEndpoints(endpoints =>
      {
        // Mapping to Route
        endpoints.MapGet("/hello", async context =>
        {
          await context.Response.WriteAsync("Hello!");
        });
        // Optional Parameters
        // Mapping to Default Constraint => https://localhost:7268/optional/asdfs
        endpoints.MapGet("/optional/{param1:alpha?}", async context =>
        {
          var value = context.GetRouteValue("param1");
          await context.Response.WriteAsync($"Hello From Optional Constraint - {value}");
        });
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
        endpoints.MapGet("/", async context =>
        {
          await context.Response.WriteAsync("Hello World!");
        });
        endpoints.MapControllers();

      });
    }
  }
}
