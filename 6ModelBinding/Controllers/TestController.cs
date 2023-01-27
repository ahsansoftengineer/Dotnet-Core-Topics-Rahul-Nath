using Microsoft.AspNetCore.Mvc;

namespace _6ModelBinding.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TestController : ControllerBase
  {
    [BindProperty(SupportsGet =true, Name = "bindPropertyNameOverride")]
    public bool bindPropertyName { get; set; }

    // Required Parameter to be added in HttpGet Attribute
    // Query Implicitly Added by Dotnet Core
    // https://localhost:7083/api/test/105?isValue=true&bindPropertyName=True
    // https://localhost:7083/api/test/105?isValue=true&bindPropertyNAMEOverride=false
    [HttpGet("{id:int}")]
    public string Get(
      int id, 
      bool isValue, 
      [FromHeader(Name = "Accept-Language")] string language
    )
    {
      return $"id = {id} isValue = { isValue } bindProperty = { bindPropertyName } Language = { language }";
    }
    // https://localhost:7083/api/test/complex?Id=101&Name=Ahsan&Description=This%20is%20Description&Marks=1&Marks=2
    // Marks[0]=1&Marks[1]=2
    [HttpGet("complex")]
    public string Get([FromQuery] MyComplexType mct)
    {
      return $"id = {mct.Id} Name = {mct.Name} Description = {mct.Description} Marks = [{ArrayToString(mct.Marks)}]";
    }

    [HttpPost("complex")]
    [Consumes("application/xml", "application/json")]
    public string contentNegociation(MyComplexType mct)
    {

      return $"id = {mct.Id} Name = {mct.Name} Description = {mct.Description} Marks = [{ArrayToString(mct.Marks)}]";
    }


    public string ArrayToString(string[] arrayz)
    {
      string Results = "";

      foreach (string item in arrayz)
      {
        Results += item + ", ";
      }
      return Results;
    }
  }
}

public class MyComplexType
{
  public int Id { get; set;}
  public string Name { get; set;} 
  public string Description { get; set;} 
  public string[] Marks { get; set;}

}
