namespace _3Configuration
{
  public class Startup
  {
    public IConfiguration _config {get;}
    public Startup(IConfiguration configuration)
    {
      _config = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddSwaggerGen();
      // Registering Strongly Typed Configuration
      services.Configure<AppSettings>(_config.GetSection("AppSettings"));

    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      PrintConfiguration();

      // TODO: Below Section needs to be explored
      // builder.Configuration.AddEnvironmentVariables(prefix: "MyCustomPrefix_");

      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
    public void PrintConfiguration()
    {
      // Configuration Object
      Console.WriteLine("----------------Configuration[ParentKey:ChildKey Example]-----------------");
      Console.WriteLine($"MyKey {_config["MyKey"]}");
      Console.WriteLine($"MyApi.Url {_config["MyApi:Url"]}");
      Console.WriteLine($"MyApi.ApiKey {_config["MyApi:ApiKey"]}");

      // Configuration Array with get as type Class
      Console.WriteLine("----------------GetSection.Get<>() Array Override Example-----------------");
      AppSettings _settings = _config.GetSection("AppSettings").Get<AppSettings>();
      Console.WriteLine($"MyArray0: \t {string.Join(", ", _settings.Array0 ?? Array.Empty<string>())}");
      Console.WriteLine($"MyArray1: \t {string.Join(", ", _settings.Array1 ?? Array.Empty<string>())}");
      Console.WriteLine($"MyArray2: \t {string.Join(", ", _settings.Array2 ?? Array.Empty<string>())}");

      // Configurtion Array with Binding to instance variable
      Console.WriteLine("----------------GetSection.Bind() Array Override Example-----------------");
      AppSettings _settings2 = new AppSettings();
      _config.GetSection("AppSettings").Bind(_settings2);
      Console.WriteLine($"MyArray0: \t {string.Join(", ", _settings2.Array0 ?? Array.Empty<string>())}");
      Console.WriteLine($"MyArray1: \t {string.Join(", ", _settings2.Array1 ?? Array.Empty<string>())}");
      Console.WriteLine($"MyArray2: \t {string.Join(", ", _settings2.Array2 ?? Array.Empty<string>())}");

      // Configuration From Secret Manager
      // This Key will be Set using CLI Secret Manager
      Console.WriteLine("----------------User Secret Manager Can be using Visual Studio and Command Line-----------------");
      Console.WriteLine("MyApi:CLI_UserSecretManager " + _config["MyApi:CLI_UserSecretManager"]);

      Console.WriteLine("----------------Own Configration Resource-----------------");
      Console.WriteLine("MyApi:MyAppConfigResource " + _config["MyApi:MyAppConfigResource"]);

      Console.WriteLine("----------------Own Configration In Memory Collection -----------------");
      Console.WriteLine("MyApi:InMemoryCollection " + _config["MyApi:InMemoryCollection"]);

      Console.WriteLine("----------------Fall back Key -----------------");
      Console.WriteLine("nonExsistanceKey " + _config.GetValue<int>("nonExsistanceKey", 799));
    }
  }
}
