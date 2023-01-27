## ENVIRONMENT

### We Already Discussed several Steps in Startup Class Project

### What are the ways of Setting Environment Variable in Web Application at Production / Development?
- *There are several ways to set environment variables in .NET Core:*

1. Using the command line: "set" command (Windows) or "export" command (Linux/macOS). 
- "set ASPNETCORE_ENVIRONMENT=Development" or "export ASPNETCORE_ENVIRONMENT=Development"
2. Using the launchSettings.json file: 
- which is located in the Properties folder of your project. when you run your application from Visual Studio.
3. Using the Environment Variables in the Operating System
4. Using the IHostingEnvironment in the application: You can use the IHostingEnvironment class in the application to read the environment variable.
5. Using the appsettings.json file: You can set environment variable in the appsettings.json file and read using the IConfiguration class.
6. Using the dotnet run command : You can use the --environment option to specify the environment variable when you run your application using the dotnet run command.

### Configuration VS Environment Variables
- Configuration API is a way of managing and reading configuration settings from various sources (Json, XML, CommandLine, Azure AD), 
- Environment Variables are system or user-specific settings that are stored in the operating system and can be accessed by applications
- Both can be used together to manage application settings.

### What are the Ways of Managing your Application in Different Environment?
- Dotnet has several ways of managing your application
1. Dotnet Core 6 has Single Program.cs file but you can split it in Several Files as Per your need
2. Startup.cs File is for Managing Middleware and Services
3. Startup.cs Configure, ConfigureServices can be Split Further as Per Env ConfigureDevelopment, ConfigureDevelopmentServices
4. Startup.cs ca be Split as StartupDevelopment.cs, StartupStaging.cs

### What are the Default Implementation of Host.CreateDefaultBuilder
- You can see the Implementation by Navigating the Code by Ctrl Click
- Host.CreateDefaultBuilder > builder.ConfigureDefaults > ConfigureDefaults
- HostingHostBuilderExtensions Class has All Extension Methods for Default Builders


### What are the Default Environment available in dotnet core?
1. ASP .NET Core has three default Environment
- Development
- Stagging 
- Production
2. To Check this Environment you can use Extension Methods
- env.IsDevelopment()
- env.IsProduction()
- env.IsStagging()

### What is Launch Settings.json
- Is only used on the local development Machine.
- Is not deployed
- contains profile settings
- launchSetting.json File is used to override environment that sets at the System Level

### ASPNETCORE_ENVIRONMENT vs DOTNET_ENVIRONMENT
- It is just a way of setting Environment for Development in launchSettings.json file
- Both of them are Identical same you can use Either of them to Set Development Environment
```json
"profiles": {
    "B9Environment": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:7100;http://localhost:5024",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development" // <=
        "DOTNET_ENVIRONMENT": "Development" // <=
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
```
### How to Directly Access Environment and Logger in Program.cs
### How to Check which Environment your application is running
```c#
app.Logger.LogWarning("Current Environment = {envName}", app.Environment.EnvironmentName);
```

### How to Setup Env Variable From using Command Line
```c#
// Power Shell
$Env:ASPNETCORE_ENVIRONMENT = "Staging"

// Power Shell Run as Administrator to Set System Variables
[Environment]::SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Staging", "Machine")
[Environment]::SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Staging", "User")

// CMD
setx ASPNETCORE_ENVIRONMENT Staging

// After Changing the Env Variable CMD Restart is mandatory

// To Launch Dotnet application without launch profile
dotnet run --no-launch-profile
```

### What are the Services and Middleware are available in Program.cs?
### Explorer Program.cs Dotnet6 Builder and App
1. Builder Class Implements IHost, IApplicationBuilder, IEndpointRouteBuilder, IAsyncDisposable
#### Builder has undermentioned Properties
```c#
  public IServiceProvider Services => _host.Services;
  public IConfiguration Configuration => _host.Services.GetRequiredService<IConfiguration>();
  public IWebHostEnvironment Environment => _host.Services.GetRequiredService<IWebHostEnvironment>();
  public IHostApplicationLifetime Lifetime => _host.Services.GetRequiredService<IHostApplicationLifetime>();
  public ILogger Logger { get; }
  internal IDictionary<string, object?> Properties => ApplicationBuilder.Properties;
  internal IFeatureCollection ServerFeatures => _host.Services.GetRequiredService<IServer>().Features;
  internal ICollection<EndpointDataSource> DataSources => _dataSources;
  ICollection<EndpointDataSource> IEndpointRouteBuilder.DataSources => DataSources;
  IServiceProvider IEndpointRouteBuilder.ServiceProvider => Services;

  public static WebApplication Create(string[]? args = null) =>
      new WebApplicationBuilder(new() { Args = args }).Build();
  public Task StartAsync(CancellationToken cancellationToken = default) =>
      _host.StartAsync(cancellationToken);
  public Task StopAsync(CancellationToken cancellationToken = default) =>
      _host.StopAsync(cancellationToken);

  public Task RunAsync(string? url = null)
  {
      Listen(url);
      return HostingAbstractionsHostExtensions.RunAsync(this);
  }

  public void Run(string? url = null)
  {
      Listen(url);
      HostingAbstractionsHostExtensions.Run(this);
  }

  IApplicationBuilder IApplicationBuilder.New()
  {
      var newBuilder = ApplicationBuilder.New();
      // Remove the route builder so branched pipelines have their own routing world
      newBuilder.Properties.Remove(GlobalEndpointRouteBuilderKey);
      return newBuilder;
  }

  private void Listen(string? url)
```

