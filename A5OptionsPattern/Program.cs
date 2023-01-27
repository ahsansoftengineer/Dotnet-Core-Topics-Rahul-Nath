using A5OptionsPattern;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// 1.  Registering Weather Api Options
//builder.Services.Configure<WeatherApiOptions>(
//  builder.Configuration.GetSection(WeatherApiOptions.WeatherApi));


// 2. Registering Weather Api Options with Validation
// https://localhost:7034/WeatherForecast
builder.Services.AddOptions<WeatherApiOptions>()
  .Bind(builder.Configuration.GetSection(WeatherApiOptions.WeatherApi))
  .ValidateDataAnnotations()
  // By Adding the below code it won't let application start until you have validation errors on Configuration
  .ValidateOnStart();


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
