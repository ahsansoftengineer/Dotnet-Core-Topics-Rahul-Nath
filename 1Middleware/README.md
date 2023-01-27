# Middleware

## What is Middleware in ASP.NET CORE?
1. Middleware in ASP.NET Core controls how our application responds to HTTP requests. It can also control how our application looks when there is an error, and it is a key piece in how we authenticate and authorize a user to perform specific actions.

# Middleware

### What is the difference between and RUN and USE Middleware?
1.  Use may call next middleware component in the pipeline. On the other hand, middleware defined using app. Run will never call subsequent middleware.
2. Run Middleware will terminate execution Further
3. Use Middleware will may continue execution Further

### Startup.cs VS Program.cs
1. Program.cs is where the application starts.
2. Startup.cs is where lot of the configuration happens.

### Middleware Methods
1. app.Use
2. app.UseWhen
3. app.Map
4. app.MapGet
5. app.MapPost
6. app.MapPut
7. app.MapDelete
8. app.UseConsoleLoggerMiddleware()
9. app.UseMiddleware<AnotherLoggerMiddleware>()
10 app.Run

### What is Middleware in Dotnet Core?
1. In the context of .NET, middleware refers to software components that run in the pipeline of an application built using the .NET framework. These components are responsible for handling specific tasks, such as authentication, routing, and handling HTTP requests and responses. In .NET Core, middleware is implemented using classes that implement the IMiddleware interface, and can be configured and added to the pipeline using the IApplicationBuilder class.

2. .NET Core Middleware component will be executed in the order they are added to pipeline, which is important because it can affect how the request is handled. Every middleware component can decide whether to pass the request on to the next middleware component in the pipeline or to stop the request from being processed any further.

3. For example, in a web application built using .NET Core, you might have middleware components for authentication, logging, and routing. When a request is made to the application, it would first be passed through the authentication middleware, which would check to see if the request is coming from an authenticated user. If the user is authenticated, the request would then be passed on to the logging middleware, which would log the request. Finally, the request would be passed to the routing middleware, which would determine which controller action to execute based on the URL of the request.