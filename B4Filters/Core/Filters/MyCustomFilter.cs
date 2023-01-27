using Microsoft.AspNetCore.Mvc.Filters;

namespace B4Filters.Core.Filters
{
  // Async Filter can also be used as global
  public class MyCustomFilter : Attribute, IActionFilter, IOrderedFilter
  {
    public int Order { get; }
    public MyCustomFilter(int? order = 0)
    {
      Order = order ?? 0;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      Console.WriteLine("MyCustomFilter | Before | Global | On Action Executing | Order " + Order);
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
      Console.WriteLine("MyCustomFilter | After | Global | On Action Executed | Order " + Order);
    }
  }
}
