using _1Middleware.Middleware;

namespace _1Middleware
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      // MIDDLEWARE REGISTRATION IN DI CONTAINER
      services.AddTransient<ConsoleLoggerMiddleware>();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // Application Default Configuration
      //if(env.IsDevelopment())
      //{
      //  app.UseDeveloperExceptionPage();
      //}
      //app.UseHttpsRedirection();
      //app.UseStaticFiles();
      //app.UseRouting();
      //app.UseAuthorization();
      //app.UseEndpoints(endpoints =>
      //{
      //  endpoints.MapControllers();
      //});


      // 1. OLD WAY OF USING MIDDLEWARE IN DOTNET CORE BUT IT USES SPERATION OF CONCERNN PRINCIPLE
      // 2. CONTINUED MIDDLEWARE
      app.Use(async (context, next) => await next(context));
      // 3. Middleware Mapping to a Specific File
      app.Map("/favicon.ico", (app) => { });
      // 4. Middleware Mapping to a Specific route
      app.Map("/map", MapHandler);
      // 5. Middleware for Everything
      app.UseMiddleware<ConsoleLoggerMiddleware>();
      // 6. Middleware Conditionally
      app.UseWhen(context => context.Request.Query.ContainsKey("q"), HandleRequestWithQuery);
      // 7. TERMINATING MIDDLEWARE
      app.Run(async context =>
      {
        Console.WriteLine("RUN MIDDLEWARE");
        await context.Response.WriteAsync("RUN MIDDLEWARE");
      });
      // 8. CONVERTING USE AS RUN METHOD
      //app.Use(_ => async context =>
      //{
      //  Console.WriteLine("RUN MIDDLEWARE");
      //  await context.Response.WriteAsync("RUN MIDDLEWARE");
      //});


    }

    // Middleware as a Function
    private void HandleRequestWithQuery(IApplicationBuilder app)
    {
      app.Use(async (context, next) =>
      {
        Console.WriteLine("Contains Query");
        await next();
      });
    }


    private void MapHandler(IApplicationBuilder app)
    {
      app.Run(async context =>
      {
        Console.WriteLine("Hello for Map Method");
        await context.Response.WriteAsync("Hello from Map Method");
      });
    }
  }
}
