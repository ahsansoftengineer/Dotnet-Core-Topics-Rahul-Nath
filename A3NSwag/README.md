
### What is the Default Swagger comes with ASP .NET CORE?
1. When you create ASP .Net Core Project with Visual Studio it gives the option to create project with the following Swagger Package Configured
- Swashbuckle.AspNetCore

### To see the JSON Representation of Swagger API use the Provided Link?
- https://localhost:7187/swagger/v1/swagger.json
- It is usefull when you want to create Model / Interface based on API Documentation
- .Net also provide easy way of Creating Client Side Model by it self you just have configure the route where you want to put the created model.

### How to Change Swagger Endpoints
- Change the Launch Endpoints in launchSettings.json file
```json
  "profiles": {
    "A3NSwag": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "weatherforecast",
      //"launchUrl": "swagger",
      "applicationUrl": "https://localhost:7187;http://localhost:5081",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "weatherforecast",
      //"launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
```



### What are the Components of Swashbuckle?
- There are three main components to Swashbuckle:

1. Swashbuckle.AspNetCore.Swagger: a Swagger object model and middleware to expose SwaggerDocument objects as JSON endpoints.

2. Swashbuckle.AspNetCore.SwaggerGen: a Swagger generator that builds SwaggerDocument objects directly from your routes, controllers, and models. It's typically combined with the Swagger endpoint middleware to automatically expose Swagger JSON.

3. Swashbuckle.AspNetCore.SwaggerUI: an embedded version of the Swagger UI tool. It interprets Swagger JSON to build a rich, customizable experience for describing the web API functionality. It includes built-in test harnesses for the public methods.

### Implementation Swashbuckle?
```c#
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Version = "v1",
    Title = "ToDo API",
    Description = "An ASP.NET Core Web API for managing ToDo items",
    TermsOfService = new Uri("https://example.com/terms"),
    Contact = new OpenApiContact
    {
      Name = "Example Contact",
      Url = new Uri("https://example.com/contact")
    },
    License = new OpenApiLicense
    {
      Name = "Example License",
      Url = new Uri("https://example.com/license")
    }
  });

  // using System.Reflection;
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI();
}
```

### How to Enhance Swagger Docs?
1. Adding triple-slash comments to an action enhances the Swagger UI by adding the description to the section header. Add a \<summary\> element above the Delete action:
```c#
/// <summary>
/// Deletes a specific TodoItem.
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
[HttpDelete("{id}")]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<IActionResult> Delete(long id)
{
}
```

### What is NSwag?
- NSwag offers the following capabilities:

1. The ability to utilize the Swagger UI and Swagger generator.
2. Flexible code generation capabilities.
3. With NSwag, you don't need an existing API—you can use third-party APIs that incorporate Swagger and generate a client implementation. NSwag allows you to expedite the development cycle and easily adapt to API changes.

### What is NSwag Studio?
- NSwag Studio has the Capability to Convert JSON Representation of Swagger Documentation into Types and Models for Client Side Like JavaScript and TypeScript Models So You don't have to Make Models by Yourself

### What is NSwag MSBuild?
1. MSBuild, which allows access to the code generator from MSBuild. Newtonsoft. Json, needed to compile the generated client. System.

### How to Install NSwag?
- From the Manage NuGet Packages dialog:

- Right-click the project in Solution Explorer > Manage NuGet Packages
- Set the Package source to "nuget.org"
- Enter "NSwag.AspNetCore" in the search box
- Select the "NSwag.AspNetCore" package from the Browse tab and click Install

### How to Implement NSwag?
- To Implement NSwag update the Program.cs
```c#
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
```

### What is URL to see the Documentations?
- Launch the app. Navigate to:
1. http://localhost:<port>/swagger to view the Swagger UI.
2. http://localhost:<port>/swagger/v1/swagger.json to view the Swagger specification.


