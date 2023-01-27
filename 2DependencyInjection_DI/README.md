# DEPENDENCY INJECTION (DI)

### How to Use Scoped Service into Singleton Service
1. Example available in B6BackgroundService Project

### How does DI work in .NET Core?
1. ASP.NET Core supports the dependency injection (DI) software design pattern, which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies.
2. Dependency injection is the design pattern that allows us to inject the dependency into the class from the outer world rather than creating with in class. This will help us to create a loosely coupled applications so that it has provided greater maintainability, testability, and also re-usability.

### What is IoC in ASP.NET Core?
1 ASP.NET Core contains a built-in dependency injection mechanism. In the Startup. cs file, there is a method called ConfigureServices which registers all application services in the IServiceCollection parameter. The collection is managed by the Microsoft.
2. .NET supports the dependency injection (DI) software design pattern, which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies. Dependency injection in . NET is a built-in part of the framework, along with configuration, logging, and the options pattern.

### What is Service Descriptor?
1. A Service Descriptor describes a service with its service type, implementation, and lifetime
2. A Service Type Usually Interface, AbstractClass, Class it Self

### What is Service LifeTimes?
1. Transient -> Create each time they are requested from the service container,
2. Scoped -> Created Once per client request (connection)
3. Singleton -> Created at the first time they are requested (or when Startup.ConfigureServices is run and an instance is specified with the service registration). Every subsequent request uses the same instance. If the app requires singleton behaviour, allowing the service container to manage the service's lifetime is recommended. Don't implement the singleton design pattern and provide user code to manage the object's lifetime in the class.

### What are the overload for Controller Services?
- AddControllers
- AddControllers
- AddControllersWithViews
- AddControllersWithViews
- AddMvc
- AddMvc
- AddRazorPages
- AddRazorPages

### What are Infrastructure Services?
1. Infrastructure Services are those services those are minimum required to working with ASP.NET CORE WEB Application to work
2. 89 Infrastructure Services Added Initially (IHostingEnvironment, IHostEnv, IConfiguration etc...)
3. 180 After Adding AddController() to IoC it Becomes
4. 189 After Adding Swagger
5. So On and So Further Just the Idea Every Services and Several Dependencies

### What does AddController does behind the scene?
1. AddController Does Several Thing Under the Hood
2. This method configures the MVC services for the commonly used features with controllers for an API. This
- AddMvcCore(IServiceCollection) 
- AddApiExplorer(IMvcCoreBuilder) 
- AddAuthorization(IMvcCoreBuilder) 
- AddCors(IMvcCoreBuilder) 
- AddDataAnnotations(IMvcCoreBuilder) 
- AddFormatterMappings(IMvcCoreBuilder) 
3. To add services for controllers with views call  AddControllersWithViews(IServiceCollection) on the resulting builder.
4. To add services for pages call AddRazorPages(IServiceCollection)

### What is ILogger?
1. ILogger by default set to log within the console
2. To Inject the ILogger you have to Add it in within the Constructor

### What Happened when you inject Same Service for Same Type
1. When Add Same Service twice the last Registration Wins

### What are the ways of Registering Services in ASP .Net Core?

### What are the Ways of Injecting Services in ASP .NET Core?
1. Constructor Injection
```c#
public class HomeController : Controller
{
  ILog _log;
  public HomeController(ILog log) {  _log = log; }
  public IActionResult Index()
  {
    _log.info("Executing /home/index");
    return View();
  }
}
```
2. Action Method Injector
```c#
public class HomeController : Controller
{
  public HomeController() { }

  public IActionResult Index([FromServices] ILog log)
  {
    log.info("Index method executing");
    return View();
  }
}
```
3. Property Injection
```c# 
public class HomeController : Controller
{
  public HomeController() { }
  public IActionResult Index()
  {
    // HttpContext is Coming from Base Controller Class
    var services = this.HttpContext.RequestServices;
    var log = (ILog)services.GetService(typeof(ILog));
            
    log.info("Index method executing");
    return View();
  }
}
```

### Singleton VS Transient VS Scoped
- Browser Does not Impact and Result is the same for Each Request
#### First Request Chrome
-------------xxx-----------
From DependencyService1
- Transient - 4a3256f2-b319-4d64-bbc3-856d4eefe38e // Every Step New
- Scoped - 31764c5c-c8df-471f-9b61-06085a3d2f0d // Each for Request
- Singleton - 93fefd73-db11-4713-a305-e0a5468f522e // Same for every Request
- Singleton Instance - 00000000-0000-0000-0000-000000000000 // // Same for every Request

-------------xxx-----------
From DependencyService2
- Transient - 9e52b78c-a178-458c-a1b2-df9c48f3dcf6
- Scoped - 31764c5c-c8df-471f-9b61-06085a3d2f0d
- Singleton - 93fefd73-db11-4713-a305-e0a5468f522e
- Singleton Instance - 00000000-0000-0000-0000-000000000000

#### Second Request Chrome
-------------xxx-----------
From DependencyService1
- Transient - 398f80f3-6ce9-4b4f-8445-b0fde99ded8e
- Scoped - fa350e12-fa3c-4aa3-8903-fc606a7c1718
- Singleton - 93fefd73-db11-4713-a305-e0a5468f522e
- Singleton Instance - 00000000-0000-0000-0000-000000000000

-------------xxx-----------
From DependencyService2
- Transient - 9df26503-b3e5-4f0e-9718-5fab0238a465
- Scoped - fa350e12-fa3c-4aa3-8903-fc606a7c1718
- Singleton - 93fefd73-db11-4713-a305-e0a5468f522e
- Singleton Instance - 00000000-0000-0000-0000-000000000000

#### Third Request Edge
-------------xxx-----------
From DependencyService1
- Transient - f34981a6-d4d0-4f40-88f4-53bb52989647
- Scoped - 42ef379a-48d5-435d-857c-885e0878ce10
- Singleton - 93fefd73-db11-4713-a305-e0a5468f522e
- Singleton Instance - 00000000-0000-0000-0000-000000000000

-------------xxx-----------
From DependencyService2
- Transient - e792eaab-0f05-4c7f-a6a4-4accd34ba04c
- Scoped - 42ef379a-48d5-435d-857c-885e0878ce10
- Singleton - 93fefd73-db11-4713-a305-e0a5468f522e
- Singleton Instance - 00000000-0000-0000-0000-000000000000
