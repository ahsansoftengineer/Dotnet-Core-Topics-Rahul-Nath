using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace _1Middleware
{
  public class Old_Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
    }
        

    public static IWebHostBuilder CreateDefaultBuilder(string[] args)
    {
      var builder = new WebHostBuilder()
          .UseKestrel()
          .UseContentRoot(Directory.GetCurrentDirectory())
          .ConfigureAppConfiguration((hostingContext, config) =>
          {
            var env = hostingContext.HostingEnvironment;

            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            if (env.IsDevelopment())
            {
              var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
              if (appAssembly != null)
              {
                config.AddUserSecrets(appAssembly, optional: true);
              }
            }

            config.AddEnvironmentVariables();

            if (args != null)
            {
              config.AddCommandLine(args);
            }
          })
          .ConfigureLogging((hostingContext, logging) =>
          {
            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            logging.AddConsole();
            logging.AddDebug();
          })
          .UseIISIntegration()
          .UseDefaultServiceProvider((context, options) =>
          {
            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
          });

      return builder;
    }
  }
}
