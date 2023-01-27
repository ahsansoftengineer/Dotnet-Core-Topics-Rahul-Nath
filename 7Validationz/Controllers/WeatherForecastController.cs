using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _7Validationz.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {

    //private readonly ILogger<WeatherForecastController> _logger;

    //public WeatherForecastController(ILogger<WeatherForecastController> logger)
    //{
    //  _logger = logger;
    //}
    // 1. Manually Handling Errors using Array
    [Route("Validation_Array")]
    [HttpGet()]
    public ActionResult<string> Validation_Array(string id, bool? isTest)
    {
      var errors = new List<string>();
      if (!isTest.HasValue)
        errors.Add("is Test is required");

      if (string.IsNullOrEmpty(id))
        errors.Add("Id is Required");

      if (errors.Count > 0)
        return BadRequest(errors);
      
      return "Hello World From Validation_Array";
    }
    // 2. Manually Handling Errors using AddModelError
    [Route("Validation_ModelState")]
    [HttpGet()]
    public ActionResult<string> Validation_ModelState(string id, bool? isTest)
    {
      ModelState.ClearValidationState("");
      if (!isTest.HasValue)
        ModelState.AddModelError(nameof(isTest), "is Test is required");

      if (string.IsNullOrEmpty(id))
      {
        ModelState.AddModelError(nameof(id), "id is Required");
        ModelState.AddModelError(nameof(id), "id less than 3");
      }

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      return "Hello World From Validation_ModelState";
    }
    // https://localhost:7019/WeatherForecast/Validation_Attribute?id=2
    [Route("Validation_Attribute")]
    [HttpGet()]
    public ActionResult<string> Validation_Attribute([Required][MinLength(3)] string id, [Required] bool? isTest)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);
      return "Hello World from Validation_Attribute";
    }
    [Route("Validation_Modal")]
    [HttpPost()]
    public ActionResult<string> Validation_Modal(
      [FromBody] MyModal modal
      )
    {
      return $"Validation Modal Id = {modal.Id} Name = {modal.Name}";
    }
  }
}
