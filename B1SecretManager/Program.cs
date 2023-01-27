using B1SecretManager.Model.Configuration;
using B1SecretManager.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// How to Setup User Secrets for Different Environments?
builder.Host.ConfigureAppConfiguration((hostContext, config) =>
{
  if (hostContext.HostingEnvironment.EnvironmentName == "LocalDev")
  {
    // Where does this Program File Located ????
    config.AddUserSecrets<Program>();
  }
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions<ConnectionStrings>()
  .Bind(builder.Configuration.GetSection(ConnectionStrings.connectionStrings))
  .ValidateDataAnnotations()
  // By Adding the below code it won't let application start until you have validation errors on Configuration
  .ValidateOnStart();
builder.Services.AddHttpClient<IWeatherService, WeatherService>(c =>
{
  ConnectionStrings cs =  builder.Configuration.GetSection(ConnectionStrings.connectionStrings).Get<ConnectionStrings>();
  //c.BaseAddress = new Uri(cs.WeatherApi.UrlCurrent);
  c.BaseAddress = new Uri(cs.WeatherApi.UrlForecast);
});


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
