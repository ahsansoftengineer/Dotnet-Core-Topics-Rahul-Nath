using Azure.Identity;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration.AzureKeyVault;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((context, config) =>
  {
    var builtConfig = config.Build();
    var vaultName = builtConfig["VaultName"];
    var keyVaultClient = new KeyVaultClient(async (authority, resource, scope) =>
    {
      var credential = new DefaultAzureCredential(false);
      var token = credential.GetToken(
        new Azure.Core.TokenRequestContext(
          new[] { "https://vault.azure.net/.default" }));
      return token.Token;
    });
    config.AddAzureKeyVault(vaultName, keyVaultClient, new DefaultKeyVaultSecretManager());
  });

// Add services to the container.

builder.Services.AddControllers();
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
