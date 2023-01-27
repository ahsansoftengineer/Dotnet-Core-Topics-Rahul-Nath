using Microsoft.AspNetCore.Builder;

namespace A8StartupClass
{
  public class Startup
  {
    // CONFIGURE SERVICES
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddSwaggerGen();
    }
    public void ConfigureDevelopmentServices(IServiceCollection services) {
      Console.WriteLine("Startup.cs Development");
      this.ConfigureServices(services);
    }
    public void ConfigureStagingServices(IServiceCollection services)
    {
      Console.WriteLine("Startup.cs Staging");
      this.ConfigureServices(services);
    }

    // CONFIGURE
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
    public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env) 
    {
      this.Configure(app, env);
    }
    public void ConfigureStaging(IApplicationBuilder app, IWebHostEnvironment env)
    {
      this.Configure(app, env);
    }
  }
}
