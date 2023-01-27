//using A4AzureAppConfiguration.Configuration;

// // WORKING EXAMPLE WITH CONFIGURATION MANAGER


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//builder.Host.ConfigureAppConfiguration(config =>
//{
//  var connectionString = config.Build().GetConnectionString("appConfiguration");

//  // 1 For Non Refreshing Key Access from AzureAppConfiguration
//  //config.AddAzureAppConfiguration(connectionString);

//  // 2 To Set up Refreshing Key from Azure App Configuration
//  config.AddAzureAppConfiguration(option =>
//  {
//    option.Connect(connectionString);
//    option.ConfigureRefresh(refresh =>
//    {
//      refresh
//      //.Register("weather:count")
//      .Register("refreshAll", true)
//      .SetCacheExpiration(TimeSpan.FromSeconds(5));
//    });
//  });

//  // 2.1 Here we are doing something different
//  builder.Services.Configure<WeatherConfiguration>(config.Build().GetSection("weather"));

//});

//// 2.2 Adding Azure App Configuration Service to DI
//builder.Services.AddAzureAppConfiguration();

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();



//var app = builder.Build();


//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//  app.UseSwagger();
//  app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//// 2.3 Adding Middle of Azure App Configuration
//app.UseAzureAppConfiguration();

//app.Run();
