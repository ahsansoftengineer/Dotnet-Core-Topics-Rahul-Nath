using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
namespace B9Environment
{
  public class Startup
  {

    // Configure Services
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Environments",
          Version = "v1"
        });
      });
    }
    public void ConfigureDevelopmentServices(IServiceCollection services)
    {
      Console.WriteLine("Startup.cs Development");
      this.ConfigureServices(services);
    }
    public void ConfigureStagingServices(IServiceCollection services)
    {
      Console.WriteLine("Startup.cs Staging");
      this.ConfigureServices(services);
    }

    // Configure
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      logger.LogWarning("Current Environment = {envName}", env.EnvironmentName);
    }
    public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
      app.UseDeveloperExceptionPage();
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Environments v1"));

      Configure(app, env, logger);
    }
    public void ConfigureStaging(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
      Configure(app, env, logger);
    }
    public void ConfigureProduction(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
      ConfigureStaging(app, env, logger);
    }
  }
}
