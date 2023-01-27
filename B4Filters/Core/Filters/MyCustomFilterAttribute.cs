using Microsoft.AspNetCore.Mvc.Filters;

namespace B4Filters.Core.Filters
{
  public class MyCustomFilterAttribute : Attribute, IActionFilter, IOrderedFilter
  {
    private readonly string name;
    public int Order { get; }
    public MyCustomFilterAttribute(string name, int order)
    {
      this.name = name;
      Order = order;
    }
    public MyCustomFilterAttribute(string name) : this(name, 0) { }
    public void OnActionExecuting(ActionExecutingContext context)
    {
      Console.WriteLine("MyCustomFilterAttribute | On Action Executing " + name + " Order " + Order);
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
      Console.WriteLine("MyCustomFilterAttribute | On Action Executed " + name + " Order " + Order);
    }
  }
}
