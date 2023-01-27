using Microsoft.AspNetCore.Mvc.Filters;

namespace B4Filters.Core.Filters
{
  public class MyCustomAsyncFilterAttribute : Attribute, IAsyncActionFilter, IOrderedFilter
  {
    private readonly string value;
    public int Order { get; }
    public MyCustomAsyncFilterAttribute(string value, int order)
    {
      this.value = value;
      this.Order = order;
    }
    public MyCustomAsyncFilterAttribute(string value) : this(value, 0) { }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      Console.WriteLine("MyCustomAsyncFilterAttribute | Before | " + value + " Order " + Order);
      await next();
      Console.WriteLine("MyCustomAsyncFilterAttribute | After | " + value + " Order " + Order);
    }
  }
}
