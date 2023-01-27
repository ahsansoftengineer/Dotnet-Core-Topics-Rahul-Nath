## Controller Action Return Type

### What are the types of data a Controller can Return?
- When there are no chances of Error or any data types to be return at that time we can directly Return Complex / Primitive data types
1. Complex Data Type
```c#
public IEnumerable\<WeatherForecast> Get()
```
2. Primitive Data Type
```c#
public int, string, bool Get()
```

### When to use IAction & IActionResult?
- IActionResult is appropriate when there are chances of different data types tobe return.
- Bad Request, OK, Not Found Result example of IActionResult

### How Does Every Result Type is Directly or Indirectly Return type is ActionResult?
1. OkObjectResult > ObjectResult > ActionResult > IActionResult = Task ExecuteResultAsync(ActionContext context);
2. What is OK, What is Virtual
```c#
[NonAction]
public virtual OkObjectResult Ok([ActionResultObjectValue] object? value)
    => new OkObjectResult(value);
```
### Short hand Syntax alternations
1. Undermentioned Short Hand Syntax are Coming From ControllerBase Class
```c
 if (string.IsNullOrEmpty(cityName))
    //return new BadRequestObjectResult("Please provide city name");
    return BadRequest("Please provide city name");

if (cityName == "invalid")
    //return new NotFoundObjectResult("No such city exist in the DB");
    return NotFound("No such city exist in the DB");
else
    //return new OkObjectResult(result);
    return Ok(result);
```
