### User Secrets 

### What is the purpose of User Secrets?
1. In software development, user secrets are a way to store sensitive information, such as API keys or passwords, that are used by an application at runtime. 
2. They are typically stored in a separate location from the rest of the code, and are not included in version control. 
3. This helps to keep sensitive information secure and out of the hands of unauthorized users. User secrets can be used in a variety of contexts, including web and mobile applications, desktop software, and server-side processes. 
4. The purpose of user secrets is to protect the security and privacy of an application and its users by keeping sensitive information separate from the rest of the code base.

### CLI To Create & Set User Secrets?
```c#
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:WeatherApi:Key" "Here goes the key of my choice"
dotnet user-secrets remove "ConnectionStrings:WeatherApi:Url"
```
### Where does User Secrets Stored in the Project?
1. The Secret Manager Folder has a guid that exactly the same as  UserSecretsId
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UserSecretsId>8b66fa33-5e10-4b2d-a155-4c7d2c62c6c3</UserSecretsId>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.2.3" />
  </ItemGroup>

</Project>
```
2. File Path C:\Users\ali_a\AppData\Roaming\Microsoft\UserSecrets\8b66fa33-5e10-4b2d-a155-4c7d2c62c6c3

### How to Setup for User Secrets and Strongly Typed Configurations?
1. Creating Model for Configuration
```c#
  public class ConnectionStrings
  {
    public readonly static string connectionStrings = "ConnectionStrings";
    public WeatherApi WeatherApi { get; set; }
  }
  public class WeatherApi
  {
    public readonly static string weatherApi = "WeatherApi";
    public string Key { get; set; }
    public string UrlForecast { get; set; }
    public string UrlCurrent { get; set; }
  }
```
2. Setting Secret Manager Right "Click on Project" and "Manage User Secret"
3. Configuring Program.cs
```c#
builder.Services.AddOptions<ConnectionStrings>()
  .Bind(builder.Configuration.GetSection(ConnectionStrings.connectionStrings))
  .ValidateDataAnnotations()
  // By Adding the below code it won't let application start until you have validation errors on Configuration
  .ValidateOnStart();
```
4. Using Configuration in Program.cs
```c#
builder.Services.AddHttpClient<IWeatherService, WeatherService>(c =>
{
  ConnectionStrings cs =  builder.Configuration.GetSection(ConnectionStrings.connectionStrings).Get<ConnectionStrings>();
  c.BaseAddress = new Uri(cs.WeatherApi.UrlForecast);
});
```
5. Utilizing in Service and Controller is Same
```c#
- public interface IWeatherService
  {
    Task<string> GetCityData(string cityName);
    Task<IEnumerable<WeatherForecast>> GetDayData(string cityName);
  }

  public class WeatherService : IWeatherService
  {
    private readonly HttpClient _httpClient;
    private readonly ConnectionStrings _cs;

    public WeatherService(
      HttpClient httpClient,
      IOptionsSnapshot<ConnectionStrings> cs
      )
    {
      _httpClient = httpClient;
      this._cs = cs.Value;
    }
    public async Task<IEnumerable<WeatherForecast>> GetDayData(string cityName)
    {
      string APIURL = $"?key={_cs.WeatherApi.Key}&q={cityName}&days={20}";
      var response = await _httpClient.GetFromJsonAsync<Root>(APIURL);
      return response.Forecast.Forecastday.Select(x => new WeatherForecast()
      {
        Date = DateTime.Parse(x.Date).Date,
        Summary = x.Day.Condition.Text,
        TemperatureC = x.Day.AvgtempC
      });
    }
  }

```