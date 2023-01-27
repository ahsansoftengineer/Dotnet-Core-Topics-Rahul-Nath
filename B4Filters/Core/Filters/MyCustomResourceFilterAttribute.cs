using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace B4Filters.Core.Filters
{
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
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
      Console.WriteLine("MyCustomResourceFilterAttribute | After | " + name + " Order " + Order);
    }
  }
}
