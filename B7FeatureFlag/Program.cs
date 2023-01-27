using Azure.Identity;
using B7FeatureFlag;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
  .Services
  .AddControllers()
  .AddJsonOptions(options =>
  {
    // When you intention to skip null values from result
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
  });
// Step 1
builder.Host.ConfigureAppConfiguration(config =>
{
  var azureIdentityUrl = config.Build().GetConnectionString("azureIdentityUrl");

  config.AddAzureAppConfiguration(option =>
  {
    var credential = new DefaultAzureCredential();
    option
      .Connect(new Uri(azureIdentityUrl), credential)
      .UseFeatureFlags();
    //option.ConfigureRefresh(refresh =>
    //{
    //  refresh
    //  .Register("refreshAll", true)
    //  // Default Timespan is 30 Seconds
    //  .SetCacheExpiration(TimeSpan.FromSeconds(5));
    //});
  });

  // We will Directly access without Strongly Typed Configuration
  //builder.Services.Configure<WeatherConfiguration>(config.Build().GetSection("weather"));

});




builder.Services.AddHttpContextAccessor();

builder.Services
  .AddFeatureManagement()
  .AddFeatureFilter<PercentageFilter>()
  .AddFeatureFilter<BrowserFilter>();
// Step 2
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

// Step 3
app.UseAzureAppConfiguration();

app.UseAuthorization();
app.MapControllers();
app.Run();
