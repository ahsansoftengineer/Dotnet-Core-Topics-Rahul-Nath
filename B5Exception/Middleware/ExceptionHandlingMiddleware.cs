using B5Exception.Domain.Exception;
using System.Net;

namespace B5Exception.Middleware
{

  public class ExceptionHandlingMiddleware : IMiddleware
  {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      try
      {
        await next(context);
      }
      catch (ValidationException e)
      {
        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        await context.Response.WriteAsync($"Error: Middleware: ValidationException {e.Message}");
      }
      catch (DomainNotFoundException e)
      {
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        await context.Response.WriteAsync($"Error: Middleware: DomainNotFoundException {e.Message}");
      }
    
      catch (DomainUnHandledException e)
      {
        context.Response.StatusCode = (int)HttpStatusCode.NotExtended;
        await context.Response.WriteAsync($"Error: Middleware: Handling Unhandled Exceptions 500 {e.Message}");
      }
      catch (Exception e)
      {
        context.Response.StatusCode = (int)HttpStatusCode.NotExtended;
        await context.Response.WriteAsync($"Error: Middleware: Exceptions 500 {e.Message}");
      }
    }
  }
  // Generated Code for Middleware by Visual Studio
  //// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
  //public class ExceptionHandlingMiddleware
  //{
  //  private readonly RequestDelegate _next;
  //  public ExceptionHandlingMiddleware(RequestDelegate next)
  //  {
  //    _next = next;
  //  }
  //  public Task Invoke(HttpContext httpContext)
  //  {
  //    return _next(httpContext);
  //  }
  //}

  //// Extension method used to add the middleware to the HTTP request pipeline.
  //public static class ExceptionHandlingMiddlewareExtensions
  //{
  //  public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
  //  {
  //    return builder.UseMiddleware<ExceptionHandlingMiddleware>();
  //  }
  //}
}
