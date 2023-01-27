using B4Filters.Core.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<MyCustomResultFilterAttribute>();

builder.Services.AddControllers(options =>
{
  options.Filters.Add(new MyCustomFilter());
  options.Filters.Add(new MyCustomResourceFilterAttribute("Global"));
  // Using Filter as a Service at Global Level
  //options.Filters.AddService<MyCustomResultFilterAttribute>();
});
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
app.Run();
