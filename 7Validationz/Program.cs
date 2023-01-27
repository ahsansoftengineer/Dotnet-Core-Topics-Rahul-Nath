using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
  //options.SuppressAsyncSuffixInActionNames = true;
  //Option To Check Non Nullable Reference Type Required
  //options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
  //options.SuppressInputFormatterBuffering = true;
  //options.SuppressOutputFormatterBuffering = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Disable Model Validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
  //options.SuppressConsumesConstraintForFormFileParameters= true;
  //options.SuppressInferBindingSourcesForParameters= true;
  //options.SuppressMapClientErrors= true;
  //options.SuppressModelStateInvalidFilter = true;
});
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
