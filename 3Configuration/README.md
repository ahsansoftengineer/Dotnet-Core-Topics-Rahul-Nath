# CONFIGURATION
### You can set Configuration 10 ways and the presedence from To Bottom to Top
0. Configuration Keys are Case Insensitives
1. Settings files, such as appsettings.json -> "MyKey": "From appsettings.json"
2. Settings files, such as appsettings.Development.json -> "MyKey": "From appsettings.Development.json"
3. Environment Variable OS System  ->  MyKey Environment Variable form System
4. Environment Variable OS Users ->  MyKey This is from PC User Environment Variable
5. Azure Key Vault
6. Azure App Configuration
7. Command-line arguments -> dotnet run MyKey="Any Value of my choice"
8. Custom providers, installed or created
9. Directory files
10. In-memory .NET objects

### How does CommandLine Arguments Works?
var builder = WebApplication.CreateBuilder(args);

### Storing Arrays in ASP .NET Core?
#### Note
1. Do not store array values in my base configuration, thus eliminating the possibilities of array overrides.
2. Use a comma or semicolon delimited string to store the configuration data, and split the string into an array.
3. The first two arrays have the expected values which are from the appsettings.Devleopment.json file. While, the third array MyArray2 has values from both appsettings.*.json files, which contradicts our intuition.

### How to Use Configuration with Strongly Typed Class?
// Configuration Array with binding to a Class
1.  AppSettings _settings = Configuration.GetSection("AppSettings").Get<AppSettings>();
2.  AppSettings _settings2 = new AppSettings();
3.  Configuration.GetSection("AppSettings").Bind(_settings2);

- Console.WriteLine($"MyArray0: \t {string.Join(", ", _settings.Array0 ?? Array.Empty<string>())}");
- Console.WriteLine($"MyArray1: \t {string.Join(", ", _settings.Array1 ?? Array.Empty<string>())}");
- Console.WriteLine($"MyArray2: \t {string.Join(", ", _settings.Array2 ?? Array.Empty<string>())}");

### How to Use Dependency Injection with Strongly Typed Configuration?
1. Adding Strongly Typed Configuration Servicies
```c#
public void ConfigureServices(IServiceCollection services)
{
      // Registering Strongly Typed Configuration
      services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
}
```
2. Injecting and Utlizing in the Controller
```c#
private readonly IOptions<AppSettings> _options;

public WeatherForecastController(IOptions<AppSettings> options)
{
  _options = options;
}
```

### How to Pass User Secret using CommandLine?
1. This type of Secret Configuration can be passed by CommandLine
2. Run the below command within the project
```c#
dotnet user-secrets init
dotnet user-secrets set "MyApi:CLI_SecretManager" "This information Set by Secret Manager"
dotnet run
```
3. You can also set UserSecret using Visual Studio Only Visual Studio Provide this Flexibilty to Write Visually User Secret 
- Right Click > Project > User Secret

### What is Fall back Key for Configuration?
1. Fall back Use When the Key is not availaible in any Configuration (appsetting.json, appsetting.Development.json, etc...)
```c#

```

### Three Ways of Passwing Configuraiton using Command Line Args
1. dotnet run MyKey="Using =" Position:Title=Cmd Position:Name=Cmd_Rick
2. dotnet run /MyKey "Using /" /Position:Title=Cmd /Position:Name=Cmd_Rick
3. dotnet run --MyKey "Using --" --Position:Title=Cmd --Position:Name=Cmd_Rick

### Adding Own Resource Configration Resource & Adding InMemory Collection in DI Container
1. This Configuration Goes in Program.cs File
```c#
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
```
2. After Injecting Cofiguration Service use it as below
```c#
Console.WriteLine("----------------Own Configration Resource-----------------");
Console.WriteLine("MyApi:MyAppConfigResource " + _config["MyApi:MyAppConfigResource"]);

Console.WriteLine("----------------Own Configration In Memory Collection -----------------");
Console.WriteLine("MyApi:InMemoryCollection " + _config["MyApi:InMemoryCollection"]);
```
### 
1. This Configuration goes in Program.cs file
```c#

```



### Azure KeyVault Can Also be used for Configuraiton
### Entity Framework also be used for Configuration
