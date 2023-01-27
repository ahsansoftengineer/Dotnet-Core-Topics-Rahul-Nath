using B4Filters.Core.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B4Filters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
    [HttpGet()]
    [MyCustomFilterAttribute("UserController > Get")]
    public string Get()
    {
      return "User Controller | Get Called (This is an Index Method by default mapping)";
    }

    [HttpGet("get-async")]
    [MyCustomAsyncFilter("UserController > GetAsync")]
    public string GetAsync()
    {
      return "User Controller | Get Async Called";
    }

    // This is how we use Filter as Service
    [HttpGet("filter-service")]
    [ServiceFilter(typeof(MyCustomResultFilterAttribute))]
    public string GetFilterService()
    {
      return "User Controller | Get Filter Service Called";
    }

    // This is how we use Service Filter with Args
    [HttpGet("filter-service-args")]
    [TypeFilter(typeof(MyCustomResultFilterAttribute), Arguments = new object[] { "Action" })]
    public string GetServiceFilterWithArgs()
    {
      return "User Controller | Get Service Filter With Args Called";
    }
  }
}
