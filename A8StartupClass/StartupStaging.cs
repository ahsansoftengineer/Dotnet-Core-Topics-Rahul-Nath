using Microsoft.AspNetCore.Builder;

namespace A8StartupClass
{
  public class StartupStaging
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddSwaggerGen();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      Console.WriteLine("StartupStaging.cs");

        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
