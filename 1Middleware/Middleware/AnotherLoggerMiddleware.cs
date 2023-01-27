namespace _1Middleware.Middleware
{
  // CONTINUED EXECUTION MIDDLE WARE
  public class AnotherLoggerMiddleware : IMiddleware
  {
    // 1. ASYNC MIDDLEWARE
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

      Console.WriteLine("Before AnotherLoggerMiddleware");
      await next(context);
      Console.WriteLine("After AnotherLoggerMiddleware");
    }
  }
}
