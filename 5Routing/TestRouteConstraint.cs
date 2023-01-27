namespace _5Routing
{
  public class TestRouteConstraint : IRouteConstraint
  {
    public bool Match(
      HttpContext? httpContext, 
      IRouter? route, string routeKey, 
      RouteValueDictionary values, 
      RouteDirection routeDirection
      )
    {
      if(values.TryGetValue(routeKey, out object value))
      {
        if(value is string stringValue)
        {
          return stringValue.StartsWith("0");
        }
      }
      return false;
    }
  }
}
