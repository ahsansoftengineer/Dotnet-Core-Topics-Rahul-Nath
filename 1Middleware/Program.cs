using _1Middleware.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<AnotherLoggerMiddleware>();

var app = builder.Build();


// 1. Middleware PreDefined by Dotnet
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
// 2. ALL ROUTES | ALL VERBS | CONTINUED EXECUTION | NO CONDITION
app.Use(async (context, next) => await next(context));
// 3. ALL ROUTES | ALL VERBS | CONTINUED EXECUTION | BASED ON CONDITION | HANDLER
app.UseWhen(context => context.Request.Query.ContainsKey("q"), UseWhenHandler);
void UseWhenHandler(IApplicationBuilder builder)
{
  builder.Use(async (context, next) =>
  {
    Console.WriteLine("Before app.Use");
    await next(context);
    Console.WriteLine("After app.Use");
  });
}
// 4. SPEDIFIC FILE | ALL VERBS | NO CONDITION
app.Map("/favicon.ico", (builder) => builder.Use(async (context, next) => await next(context))); // ROUTE
// 5. SPEDIFIC ROUTE | ALL VERBS | NO CONDITION
app.Map("/map", (builder) => builder.Use(async (context, next) => await next(context))); // FILE


// 6. SPECIFIC ROUTE / SPECIFIC HTTP VERBS | IN FILE CASE THERE WILL ONLY ACTION GET
app.MapGet("/hello-get", () => "hello from Map Get");
app.MapPost("/hello-post", () => "hello from Map Post");
app.MapPut("/hello-put", () => "hello from Map Put");
app.MapDelete("/hello-delete", () => "hello from Map Delete"); // etc...

// 7. CUSTOM EXTENSION METHOD (RECOMMENDED) | AS PER YOUR CUSTOMISATION YOUR ROUTE
app.UseConsoleLoggerMiddleware();

// 8. CUSTOM DEPENDENCY INJECTION | BEFORE REGISTER THE CLASS INTO DI CONTAINER
app.UseMiddleware<AnotherLoggerMiddleware>();

// 9. RUN TERMINATOIN EXECUATION FLOW OF MIDDLEWARES
app.Run(async context => {
  await context.Response.WriteAsync("Hello app.Run Middleware");
});
app.UseAuthorization();
app.MapControllers();
app.Run();
