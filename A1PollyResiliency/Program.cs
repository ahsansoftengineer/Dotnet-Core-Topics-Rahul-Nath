using A1PollyResiliency.Service;
using Polly;

namespace A1PollyResiliency
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      var timeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(5));


      builder.Services
        .AddHttpClient<IWeatherService, WeatherService>(c =>
        {
          c.BaseAddress = new Uri("http://api.weatherapi.com/v1/current.json");
        })
        .AddTransientHttpErrorPolicy(policy =>
        {
          // handling Error 408, 5XX
          // retry times, gap amoung retry
          return policy.WaitAndRetryAsync(3,
            _ => TimeSpan.FromSeconds(2)
          );
        })
        .AddTransientHttpErrorPolicy(policy =>
        {
          return policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(5));
        })
        .AddPolicyHandler(request =>
        {
          if (request.Method == HttpMethod.Get)
            return timeout;
          return Policy.NoOpAsync<HttpResponseMessage>();
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
    }
  }
}