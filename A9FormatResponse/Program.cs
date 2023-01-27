using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
  .AddControllers(
  // This Code is not Tested
  //options =>
  //  {
  //    options.RespectBrowserAcceptHeader = true; // respect the Accept header
  //    options.ReturnHttpNotAcceptable = true; // return 406 Not Acceptable if no suitable formatter is found
  //  }
  )
  .AddXmlSerializerFormatters();
  //.AddJsonOptions(options =>
  //{
  //  //options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // Default
  //  options.JsonSerializerOptions.PropertyNamingPolicy = null; // Default
  //});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
