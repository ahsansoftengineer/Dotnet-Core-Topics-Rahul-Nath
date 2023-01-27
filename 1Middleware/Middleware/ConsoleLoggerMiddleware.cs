using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace _1Middleware.Middleware
{
  // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
  public class ConsoleLoggerMiddleware
  {
    private readonly RequestDelegate _next;

    public ConsoleLoggerMiddleware(RequestDelegate next)
    {
      _next = next;
    }
    // This method created by default
    public async Task Invoke(HttpContext httpContext)
    {
      // Do anything that you want to with the context
      Console.WriteLine("Before ConsoleLoggerMiddlewareExtensions");
      await _next(httpContext);
      Console.WriteLine("After ConsoleLoggerMiddlewareExtensions");
    }
  }

  // Extension method used to add the middleware to the HTTP request pipeline.
  public static class ConsoleLoggerMiddlewareExtensions
  {
    public static IApplicationBuilder UseConsoleLoggerMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<ConsoleLoggerMiddleware>();
    }
  }
}



