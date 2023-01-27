using Microsoft.AspNetCore.Mvc.Filters;

namespace B4Filters.Core.Filters
{
  public class MyCustomResultFilterAttribute : Attribute, IResultFilter
  {
    public ILogger<MyCustomResultFilterAttribute> Logger { get; }
    public string Level { get; }

    public MyCustomResultFilterAttribute(ILogger<MyCustomResultFilterAttribute> logger, string level = "Global")
    {
      Logger = logger;
      Level = level;
    }
    public void OnResultExecuting(ResultExecutingContext context)
    {
      Logger.LogInformation($"Before | {Level} | Service Filter");
    }
    public void OnResultExecuted(ResultExecutedContext context)
    {
      Logger.LogWarning($"After | {Level} | Service Filter ");
    }
  }
}
