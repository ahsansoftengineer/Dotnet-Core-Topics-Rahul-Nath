## Filters

### What is a Marker Interface?
1. A marker interface in .NET is an interface that contains no methods or properties, and is used to indicate that a class that implements it has a specific behavior or characteristic. Marker interfaces are used to provide metadata about a class, rather than to define behavior. Examples of marker interfaces in the .NET Framework include the ICloneable and ISerializable interfaces.

### Types of Filter in dotnet core?
1. *Authorization filters* — These run first in the pipeline, so are useful for protecting your APIs and action methods. If an authorization filter deems the request unauthorized, it will short-circuit the request, preventing the rest of the filter pipeline from running.
2. *Resource filters* — After authorization, resource filters are the next filters to run in the pipeline. They can also execute at the end of the pipeline, in much the same way that middleware components can handle both the incoming request and the outgoing response. Alternatively, they can completely short-circuit the request pipeline and return a response directly. Thanks to their early position in the pipeline, resource filters can have a variety of uses. You could add metrics to an action method, prevent an action method from executing if an unsupported content type is requested, or, as they run before model binding, control the way model binding works for that request.
3. *Action filters* — Action filters run just before and after an action is executed. As model binding has already happened, action filters let you manipulate the arguments to the method—before it executes—or they can short-circuit the action completely and return a different IActionResult. Because they also run after the action executes, they can optionally customize IActionResult before it’s executed.
4. *Exception filters* — Exception filters can catch exceptions that occur in the filter pipeline and handle them appropriately. They let you write custom MVC-specific error-handling code, which can be useful in some situations. For example, you could catch exceptions in Web API actions and format them differently to exceptions in your MVC actions.
5. *Result filters* — Result filters run before and after an action method’s IActionResult is executed. This lets you control the execution of the result, or even short-circuit the execution of the result.

### How to create a Custom Filter
- This type of Filters add for Whole Application
1. Simple Implements IActionFilter to a Class 
```c#
  public class MyCustomFilter : IActionFilter
  {
    public void OnActionExecuting(ActionExecutingContext context)
    {
      Console.WriteLine("On Action Executing");
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
      Console.WriteLine("On Action Executed");
    }
  }
```
2. Adding Filter to Program.cs File
```c#
builder.Services.AddControllers(options =>
{
  options.Filters.Add(new MyCustomFilter());
});
```

### How to Use Filter as Annotations?
- All Attribute must Suffix with Attribute Key word
- Just Extend the Class with Attribute Partial Class
- Annotation Filter doesn't need to be the Part of Program.cs instead you can directly used it at Controller, Action Level
- Name Property is not necessary it is required only to pass some arguments
```c#
public class MyCustomFilterAttribute : Attribute, IActionFilter
{
  private readonly string name;
  public MyCustomFilterAttribute(string name)
  {
    this.name = name;
  }
  public void OnActionExecuting(ActionExecutingContext context)
  {
    Console.WriteLine("My Custom Filter Attribute | On Action Executing " + name);
  }
  public void OnActionExecuted(ActionExecutedContext context)
  {
    Console.WriteLine("My Custom Filter Attribute | On Action Executed " + name);
  }
}
```

### Dotnet core AsyncFilter and Middleware Similarities?
1. In ASP.NET Core, both filters and middleware are used to perform pre- and post-processing logic on requests and responses. However, they have some key differences in terms of their usage and functionality.

#### Filters:
- Filters are used to perform logic that is specific to a particular action or controller. They can be used to perform tasks such as logging, authorization, and exception handling.
- Filters can be applied to specific actions or controllers, or globally to all actions and controllers.
- Filters are executed before and after the execution of an action method, and can modify the request and response objects.
- Filters can be asynchronous and can be used with the async/await pattern.

#### Middleware:
- Middleware are used to perform logic that is common to all requests, regardless of the action or controller being executed. They can be used to perform tasks such as authentication, logging, and caching.
- Middleware is executed in a pipeline, and each middleware component can choose to pass the request to the next component or terminate the pipeline.
- Middleware can be asynchronous and can be used with the async/await pattern.

#### Filters & Middleware Conclusion
- Both filters and middleware can be asynchronous and can use the async/await pattern. But filters are used to perform logic that is specific to a particular action or controller, while middleware is used to perform logic that is common to all requests. 
- Middleware is executed in a pipeline, and each middleware component can choose to pass the request to the next component or terminate the pipeline, while filters are executed before and after the execution of an action method.


### How to Order Filter Execution?
- Even you have applied Filter at Global Level you can control execution by setting its Order Value
- The Lesser the Number the early it execute
- The Higher the Number the later it execute
- If there is no Order specified for Filter then Default is Zero (0) the precedence will be set by where the filter applied whether at Global, Controller, Action Level
1. Setting Filter Order at Global Level
- You have to Implement IOrderedFilter
```c#
public class MyCustomFilter : Attribute, IActionFilter, IOrderedFilter
{
  public int Order { get; }
  public MyCustomFilter(int? order)
  {
    Order = order ?? 0;  
  }
  public void OnActionExecuting(ActionExecutingContext context) { }
  public void OnActionExecuted(ActionExecutedContext context) { }
}
2. Registering Filter in Program.cs
```c#
builder.Services.AddControllers(options =>
{
  options.Filters.Add(new MyCustomFilter(-2));
});
```
3. Setting Filter for Attribute, Async Filter is same you just have to Implement IOrderedFilter
4. Utilizing in Attribute
```c#
[HttpGet("get-async")]
[MyCustomFilterAttributeAsync("UserController > GetAsync", -1)]
public string GetAsync()
{
  return "User Controller | Get Async Called";
}
```
5. Execution Result 
```c#
MyCustomResourceFilterAttribute | Before | Global Order 0
MyCustomFilter | Before | Global | On Action Executing | Order 0
MyCustomAsyncFilterAttribute | Before | UserController > GetAsync Order 0
MyCustomAsyncFilterAttribute | After | UserController > GetAsync Order 0
MyCustomFilter | After | Global | On Action Executed | Order 0
MyCustomResultFilterAttribute | Before | Global | Order 0
MyCustomResultFilterAttribute | After | Global | Order 0
MyCustomResourceFilterAttribute | After | Global Order 0
```


### What is Resource Filters?
1. In ASP.NET Core, resource filters are a type of filter that are used to perform pre- and post-processing logic on requests and responses. They are typically used for caching or logging.

2. Resource filters are executed before and after the execution of an action method, allowing them to perform logic before and after the action method is executed. They can be applied globally to all actions and controllers or on specific actions or controllers.

3. You can then apply this filter globally or on specific actions or controllers using the [TypeFilter] attribute.

4. Resource filters are useful when you want to perform logic both before and after an action method is executed, for example, you can use resource filter to cache the response of an action method and return the cached response instead of executing the action method again.
```c#
public class MyCustomResourceFilterAttribute : Attribute, IResourceFilter, IOrderedFilter
{
  private readonly string name;
  public int Order { get; }
  public MyCustomResourceFilterAttribute(string name, int order)
  {
    this.name = name;
    Order = order;
  }
  public MyCustomResourceFilterAttribute(string name) : this(name, 0) { }
  public void OnResourceExecuting(ResourceExecutingContext context)
  {
    Console.WriteLine("MyCustomResourceFilterAttribute | Before | " + name + " Order " + Order);
  }
  public void OnResourceExecuted(ResourceExecutedContext context)
  {
    Console.WriteLine("MyCustomResourceFilterAttribute | After | " + name + " Order " + Order);
  }
}
// Program.cs
builder.Services.AddControllers(options =>
{
  options.Filters.Add(new MyCustomFilter());
  options.Filters.Add(new MyCustomResourceFilterAttribute("Global"));
});
```
### What is Dotnet Core Exception Filters?
1. Dotnet Core exception filters allow you to handle exceptions in a more granular way, by providing a mechanism to filter exceptions based on certain conditions. You can use exception filters to catch specific types of exceptions, or to catch exceptions based on certain properties of the exception, such as the exception message or the stack trace. 
2. Exception filters are implemented as attributes that can be applied to a method or a class, and they are executed before the catch block of a try-catch statement. They can also be used to log exception information, or to perform additional actions, such as redirecting the user to a specific page or sending an email notification.

### Exception Filter VS Middleware
- Both exception filters and middleware are used to handle exceptions in Dotnet Core applications, but they are used in different ways.
1. Exception filters are used to handle exceptions that occur within a specific action or controller method, and they are executed before the catch block of a try-catch statement. They are useful for handling exceptions that are specific to a certain part of the application, such as a specific controller or action method.
2. Middleware, on the other hand, is used to handle exceptions that occur at a global level, across the entire application. It is executed earlier in the request pipeline and can handle exceptions that occur in any part of the application, not just a specific controller or action method.
3. In general, it's preferable to use exception filters for handling specific exceptions that occur within a specific action or controller method, and middleware for handling exceptions that occur at a global level across the entire application.
4. It's also worth noting that you can use both exception filters and middleware together in an application, to handle exceptions at both the global and local levels.


### What is purpose of using IAlwaysRunResultFilter Or IAsyncAlwaysRunResultFilter
- IAlwaysRunResultFilter is an interface in ASP.NET Core that defines a filter that will be executed after a controller action has executed, regardless of whether the action succeeded or failed. 
- It is used to perform cleanup or other tasks that should always be executed, regardless of the outcome of the action. 
- This interface is part of the Microsoft.AspNetCore.Mvc.Filters namespace.

### What is Exception Filters?
Exception Filters:
- Don't have before and after events.
- Implement OnException or OnExceptionAsync
- Handle unhandled exceptions that occur in Razor Page or controller creation, mode binding, action filters, or action methods.
- Do not catch exceptions that occur in resource filters, result filters, or MVC result execution.
- To handle an exception, set the ExceptionHandled property to true or write a response. This stops propagation of the exception.
- An exception filter can't turn an exception into a "success". Only an action filter can do that.
- Are good for trapping exceptions that occur within actions.
- Are not as flexible as error handling middleware.
- Prefer middleware for exception handling. Use exception filters only where error

### How to Use Dependency Injection for Filters?
1. The Following filters support constructor dependencies from DI:
- ServiceFilterAttribute
- TypeFilterAttribute
- IFilterFactory
2. The preceding filters can be applied to a controller or action method.
3. Loggers are available from DI. However, avoid creating and using filters purely for logging typically provides what's needed for logging. Logging added to filters:
- should focus on business domain concerns or behavior specific to the filters.
- Should not log actions or other framework events. The Built-in filters log actions

### Code of Using Dependency Injection for Filters?
1. Result Filter using with Dependency Injection
```c#
public class MyCustomResultFilterAttribute : Attribute, IResultFilter
{
  public ILogger<MyCustomResultFilterAttribute> Logger { get; }

  public MyCustomResultFilterAttribute(ILogger<MyCustomResultFilterAttribute> logger)
  {
    Logger = logger;
  }
  public void OnResultExecuting(ResultExecutingContext context)
  {
    Logger.LogInformation("Before | Global | Service Filter");
  }
  public void OnResultExecuted(ResultExecutedContext context)
  {
    Logger.LogWarning("After | Global | Service Filter ");
  }
}
```
2. When you Register it in IOC Container it will create the instance as per Registering Mode (Transient, Singleton...)
```c#
builder.Services.AddSingleton<MyCustomResultFilterAttribute>();
```
3. When you add it into Filter Collection then it will be add as Global Level
```c#
builder.Services.AddControllers(options =>
{
  // Using Filter as a Class
  options.Filters.Add(new MyCustomFilter());
  options.Filters.Add(new MyCustomResourceFilterAttribute("Global"));
  // Using Filter as a Service at Global Level
  options.Filters.AddService<MyCustomResultFilterAttribute>();
});
```
4. Using Service Filter with DI
```c#    
[HttpGet("filter-service")]
[ServiceFilter(typeof(MyCustomResultFilterAttribute))]
public string GetFilterService()
{
  return "User Controller | Get Filter Service Called";
}
```
5. Passing Arguments to Service DI Filter
```c#
[HttpGet("filter-service-args")]
[TypeFilter(typeof(MyCustomResultFilterAttribute), Arguments = new object[] { "Action" })]
public string GetServiceFilterWithArgs()
{
  return "User Controller | Get Service Filter With Args Called";
}
```

### What is the Short Circuited Pipeline?
1. Short Circuited Filter is used to stop execution of request for any reason of your choice.
2. It is specific to Resource Filter
```c#
public class MyCustomResourceFilterAttribute : Attribute, IResourceFilter, IOrderedFilter
{
  private readonly string name;
  public int Order { get; }
  public MyCustomResourceFilterAttribute(string name, int order)
  {
    this.name = name;
    Order = order;
  }
  public MyCustomResourceFilterAttribute(string name) : this(name, 0) { }
  public void OnResourceExecuting(ResourceExecutingContext context)
  {
    Console.WriteLine("MyCustomResourceFilterAttribute | Before | " + name + " Order " + Order);
    context.Result = new ContentResult()
    {
      Content = "This is from MyCustomResourceFilterAttribute > OnResourceExecuting - for short circuiting pipeline "
    };
  }
}
```