using Microsoft.Extensions.Configuration;

namespace _4Logging
{
  public class Program
  {
    //private readonly IConfiguration Configuration;
    //public Program(IConfiguration configuration)
    //{
    //  Configuration = configuration;
    //}
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      
      // Add services to the container.

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      builder.Logging.AddSeq();


      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }
      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}