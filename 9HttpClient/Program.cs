using _9HttpClient.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Way 4 | Http Client Example
//builder.Services.AddHttpClient("weather", c =>
//{
//  c.BaseAddress = new Uri("http://api.weatherapi.com/v1/current.json");
//});

// Way 5 | Typed Service Implementation
builder.Services.AddHttpClient<IWeatherService, WeatherService>(c =>
{
   c.BaseAddress = new Uri("http://api.weatherapi.com/v1/current.json");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
