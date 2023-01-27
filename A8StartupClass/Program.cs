using A8StartupClass;
using Microsoft.AspNetCore;
using System.Reflection;

internal class Program
{
  //As you can see above, the Main() method calls method expression BuildWebHost() to build web host with pre-configured defaults.The BuildWebHost expression can also be written as a method that returns IWebHost as shown below.
  private static void Main(string[] args)
  {
    //BuildWebHost(args).Run();
    CreateHostBuilder(args).Build().Run();
  }

  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
      .ConfigureWebHostDefaults(builder =>
      {
        // 0. Using Program.cs with out any Startup.cs Class Currently it is only working for Development Environment
        //builder
        //  .ConfigureServices(ConfigureServices)
        //  .Configure(Configure);

        // 1. For Using Single Startup For Different Environment
        // builder.UseStartup<Startup>();

        //2.For Using Files Environment Specific
         builder.UseStartup(typeof(Startup).Assembly.FullName);
      });

  // CONFIGURE SERVICES
  public static void ConfigureServices(IServiceCollection services)
  {
    services.AddControllers();
    services.AddSwaggerGen();
  }
  // CONFIGURE
  public static void Configure(IApplicationBuilder app)
  {
    IWebHostEnvironment env = app.ApplicationServices.GetService<IWebHostEnvironment>();
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
  // The WebHost is a static class which can be used for creating an instance of IWebHost and IWebHostBuilder with pre-configured defaults. The CreateDefaultBuilder() method creates a new instance of WebHostBuilder with pre-configured defaults. Internally, it configures Kestrel, IISIntegration and other configurations. The following is CreateDefaultBuilder() method.
  //public static IWebHost BuildWebHost(string[] args) =>
  //        WebHost.CreateDefaultBuilder(args)
  //            .UseStartup<Startup>()
  //            .Build();



  //public static IWebHostBuilder CreateDefaultBuilder(string[] args)
  //{
  //  var builder = new WebHostBuilder()
  //      .UseKestrel()
  //      .UseContentRoot(Directory.GetCurrentDirectory())
  //      .ConfigureAppConfiguration((hostingContext, config) =>
  //      {
  //        var env = hostingContext.HostingEnvironment;

  //        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
  //                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

  //        if (env.IsDevelopment())
  //        {
  //          var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
  //          if (appAssembly != null)
  //          {
  //            config.AddUserSecrets(appAssembly, optional: true);
  //          }
  //        }

  //        config.AddEnvironmentVariables();

  //        if (args != null)
  //        {
  //          config.AddCommandLine(args);
  //        }
  //      })
  //      .ConfigureLogging((hostingContext, logging) =>
  //      {
  //        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
  //        logging.AddConsole();
  //        logging.AddDebug();
  //      })
  //      .UseIISIntegration()
  //      .UseDefaultServiceProvider((context, options) =>
  //      {
  //        options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
  //      });

  //  return builder;
  //}
}