using A2Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(build =>
  {
    // Provide Here the External Route those will communicate with API
    //build.WithOrigins("http://localhost:3000");

    // Adding CORS with Configuration, Origins, Headers
    var appSettings = builder.Configuration.GetSection("AllowedOrigins").Get<AppSettings>();
    build
      .WithOrigins(appSettings.AllowedOrigins)
      .WithHeaders("Authorization")
      .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH");
  });
  // To make a Naming of Origin Policy to be used
  options.AddPolicy("SpecificOrigin", build =>
  {
    build.WithOrigins("http://local:2020")
    .WithHeaders("Authorization");
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseSpaStaticFiles();
//app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// Using a Default Policy
//app.UseCors();
// Using a Named Policy
app.UseCors("SpecificOrigin");

app.Run();
