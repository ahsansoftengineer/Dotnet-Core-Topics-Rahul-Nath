### Startup Class

### What are the types of Host in ASP .NET Core?
- In ASP.NET Core, there are two main types of hosts:
1. Web Host: A web host is used to host web applications, which are built using ASP.NET Core. It includes all the components and services required to run an ASP.NET Core web application, such as a web server, middleware components, and a request handling pipeline.
2. Generic Host: A generic host is a host that can be used to host any type of application, including console, Windows Forms, WPF, and web applications. It provides a common set of components and services that can be used by any type of application, including logging, dependency injection, and configuration.
3. Both the web host and the generic host are built on top of the same underlying host and application abstractions, so they share many of the same features and capabilities. The main difference between the two is that the web host is optimized for hosting web applications, while the generic host is more generic and can be used to host any type of application.

### ASP .Net Core Hosting?
- In ASP.NET Core, there are three main types of host:
1. IIS Hosting: This type of host uses Microsoft Internet Information Services (IIS) as the web server. The ASP.NET Core runtime is hosted in the IIS process, and IIS acts as a reverse proxy to forward requests to the ASP.NET Core runtime.
2. Self-hosted: This type of host allows you to host the ASP.NET Core runtime in your own process, using a web server of your choice. This is useful for creating desktop applications or console applications that serve web content.
3. Azure App Service Hosting: This type of host allows you to host your ASP.NET Core application in Azure App Service, which is a fully managed platform for building, deploying, and scaling web applications.
4. There are also other hosting options available, such as hosting in a Docker container or on a cloud service like Amazon Web Services (AWS).

### What are the Servicies Provided by ASP .NET CORE HOST?
- ASP.NET Core provides a 70+ of services by default that are available to your application when it is hosted by a web host. These services include:
- Configuration: Provides access to configuration data from various sources, such as appsettings.json and environment variables.
- Logging: Provides a way to log messages and events from your application.
- Dependency Injection: Provides a way to inject services and other objects into your application's classes, making it easier to manage their lifetimes and resolve dependencies.
- Hosting Environment: Provides information about the current hosting environment, such as the name of the environment (e.g. "Development", "Staging", "Production") and whether the application is running in-memory or on a web server.
- Request Delegate: Represents a delegate that handles an HTTP request.
- Server: Provides a way to start and stop the web server.
- Middleware: ASP.NET Core provides a middleware pipeline that allows you to plug in custom logic to handle requests and responses in your application.
- MVC: ASP.NET Core provides a Model-View-Controller (MVC) framework that you can use to build web applications with a separation of concerns between the model, view, and controller.
- Static Files: ASP.NET Core provides support for serving static files, such as HTML, CSS, and JavaScript files.
- Routing: ASP.NET Core provides a routing system that allows you to define routes for your application and map them to controller actions.
- URL Rewriting: Provides a way to rewrite incoming requests to different URLs before they are processed by the application.

### What are the Default Middleware Provided by ASP .Net Core Host?
- UseHsts: is a piece of middleware in ASP.NET Core that enables HTTP Strict Transport Security (HSTS). HSTS is a security policy that tells browsers to only communicate with a server using a secure connection (e.g., HTTPS) and to not use an insecure connection (e.g., HTTP).
- By including app.UseHsts(); in your middleware pipeline, you can instruct browsers to always use a secure connection when communicating with your server. This can help protect against attacks such as man-in-the-middle attacks, where an attacker could intercept and alter communications between the browser and the server.
- Authentication: Middleware for handling authentication and authorization in your application.
- CORS: Middleware for enabling Cross-Origin Resource Sharing (CORS) in your application.
- Response Caching: Middleware for caching responses from your application to improve performance.
- Response Compression: Middleware for compressing responses from your application to reduce their size and improve performance.
- Routing: Middleware for handling routing in your application and mapping requests to the appropriate controller actions.
- Static Files: Middleware for serving static files, such as HTML, CSS, and JavaScript files.
- Session: Middleware for managing session state in your application.
- URL Rewriting: Middleware for rewriting URLs in your application.

### What is Startup Class?
1. In .NET Core, the Startup class is a central point of entry for an application. It is responsible for configuring the services and middleware that will be used by the application, and for specifying the request handling pipeline that processes incoming requests and produces responses.

2. The Startup class typically defines two methods: ConfigureServices and Configure. The ConfigureServices method is used to configure the services that the application will use. These services can be framework services (such as the built-in dependency injection container), or custom application services. The Configure method is used to specify the middleware that will be used to handle requests and generate responses.

Here is an example of a simple Startup class:
```c#
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Add framework services.
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

```
### How to use Startup Class Overload Methods for Different Environment?
1. Environment Settings are Setups in **Properties > *launchSettings.jsont***
2. Here you can Set  "ASPNETCORE_ENVIRONMENT": "Development" as "Staging", "Production", "Testing" etc...
```json
 "profiles": {
    "A8StartupClass": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:7230;http://localhost:5266",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
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
2. We Can Setup Startup Class for Different Environments
- Here we are using Single file for different environment by providing Naming Convention of ConfigureServices & Configure
```c#
namespace A8StartupClass
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services) { }
    public void ConfigureDevelopmentServices(IServiceCollection services) { }
    public void ConfigureStagingServices(IServiceCollection services) { }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
    public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env) { }
    public void ConfigureStaging(IApplicationBuilder app, IWebHostEnvironment env) { }
  }
}
```
### How to use Multiple Startup Classes for Different Environment?
0. Changes Required in Program.cs
```c#
  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
      .ConfigureWebHostDefaults(builder =>
      {
        //builder.UseStartup<Startup>();
        builder.UseStartup(typeof(Startup).Assembly.FullName);
      });

```
1. We can Use multiple files for Setting up Different Environment by their naming conventions
2. To achieve that you can use Startup.cs, StartupDevelopment.cs, StartupStaging.cs
3. After using the files for different environment you don't have to
```c#
namespace A8StartupClass
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services) { }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
  }

  public class StartupStaging
  {
    public void ConfigureServices(IServiceCollection services) { }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
  }

  public class StartupDevelopment
  {
    public void ConfigureServices(IServiceCollection services) { }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
  }
}
```
### How to Use Startup Functionality in Program.cs?
1. All the Methods are Static with Program.cs 
```c#
public class Program
{
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
        builder
          .ConfigureServices(ConfigureServices)
          .ConfigureServicies(c => {
            // Add Here More Services
          })
          .Configure(Configure);
      });

  public static void ConfigureServices(IServiceCollection services)
  {
    services.AddControllers();
    services.AddSwaggerGen();
  }
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
}
```

