
// READING CONFIGURATION IN PROGRAM.CS
// var appSettings = builder.Configuration.GetSection("AllowedOrigins").Get<AppSettings>();

namespace _3Configuration
{
  public class Program
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
        })
        .ConfigureAppConfiguration((context, builder) =>
        {
          //builder.()
          // Args (FileName, File Optional / Required)
          builder.AddJsonFile("MyAppConfigResource.json", true);

          Dictionary<string, string> inMemoryConfiguration = new Dictionary<string, string>
          {
            {
              "MyApi:InMemoryCollection", "In Memory Collection"
            }
          };
          builder.AddInMemoryCollection(inMemoryConfiguration);
        });
    }
  }
}