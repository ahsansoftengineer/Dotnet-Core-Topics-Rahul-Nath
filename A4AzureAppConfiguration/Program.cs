using A4AzureAppConfiguration.Configuration;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Host.ConfigureAppConfiguration(config =>
{
  var azureIdentityUrl = config.Build().GetConnectionString("azureIdentityUrl");
  // 2 To Set up Refreshing Key from Azure App Configuration
  config.AddAzureAppConfiguration(option =>
  {
    var credential = new DefaultAzureCredential();
    option.Connect(new Uri(azureIdentityUrl), credential);
    option.ConfigureRefresh(refresh =>
    {
      refresh
      //.Register("weather:count")
      .Register("refreshAll", true)
      .SetCacheExpiration(TimeSpan.FromSeconds(5));
    });
  });

  // 2.1 Strongly Typed Configuration
  builder.Services.Configure<WeatherConfiguration>(config.Build().GetSection("weather"));

});

// 2.2 Adding Azure App Configuration Service to DI
builder.Services.AddAzureAppConfiguration();

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

// 2.3 Adding Middle of Azure App Configuration
app.UseAzureAppConfiguration();

app.Run();
