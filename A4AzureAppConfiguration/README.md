## Azure App Configuration

### How to Create a App Configuration?
1. Open [Azure App Configuration](https://portal.azure.com/#create/Microsoft.Azconfig)
2. Create a Resource Group
3. Create a Resource Group with free tier
4. See images for further configuration in Azure App Configuration
5. Install Microsoft.Azure.AppConfiguration.AspNetCore Package from Nuget

### Configuration Link [Azure App Configuration Link](https://portal.azure.com/#@ahsansoftengineeroutlook.onmicrosoft.com/resource/subscriptions/12ec7fbd-979d-4dbd-a11b-fe2d91e135fe/resourceGroups/AzureResourceGroupAppConfiguration/providers/Microsoft.AppConfiguration/configurationStores/ResourceNameAppConfiguration/kvs)

### How to Setup for Non Refreshing Azure App Configuration by Simple Accessing Configuration ?
1. Program.cs Changes
```c#
builder.Host.ConfigureAppConfiguration(config =>
{
  var connectionString = config.Build().GetConnectionString("appConfiguration");
  config.AddAzureAppConfiguration(connectionString);
});
```
2. Controller Changes
```c#
public class ConfigurationController : ControllerBase
{
  private readonly IConfiguration configuration;
  public ConfigurationController(IConfiguration configuration)
  {
    this.configuration = configuration;
  }

  [HttpGet]
  public IEnumerable<WeatherForecast> Get()
  {
    var count = configuration.GetValue<int>("weather:count");
    return WeatherService.Get(count);
  }
  }
```

### How to Setup for Refreshing Azure App Configuration with Strongly Typed Configuration?
1. Program.cs Changes
```c#
builder.Host.ConfigureAppConfiguration(config =>
{
  var connectionString = config.Build().GetConnectionString("appConfiguration");

  // 2 To Set up Refreshing Key from Azure App Configuration
  config.AddAzureAppConfiguration(option =>
  {
    option.Connect(connectionString);
    option.ConfigureRefresh(refresh =>
    {
      refresh
      .Register("weather:count")
      .SetCacheExpiration(TimeSpan.FromSeconds(5));
    });
  });

  // 2.1 Here we are Setting Strongly Typed Configuration 
  builder.Services.Configure<WeatherConfiguration>(config.Build().GetSection("weather"));
});

// 2.2 Adding Azure App Configuration Service to DI
builder.Services.AddAzureAppConfiguration();


app.MapControllers();

// 2.2 Adding Middle of Azure App Configuration
app.UseAzureAppConfiguration();
```
2. Creating Configuration Option Class
```c#
public class WeatherConfiguration
{
  public int Count { get; set; }
  public string Url { get; set; }
  public string Token { get; set; }
}
```
2. Controller Changes
```c#
public class OptionSnapshotController : ControllerBase
{
  private readonly IOptionsSnapshot<WeatherConfiguration> optionSnapshot;

  public OptionSnapshotController(IOptionsSnapshot<WeatherConfiguration> optionSnapshot)
  {
    this.optionSnapshot = optionSnapshot;
  }

  [HttpGet]
  public IEnumerable<WeatherForecast> Get()
  {
    var count = optionSnapshot.Value.Count;
    return WeatherService.Get(count);
  }
}
```

### What is purpose of Adding a sentinel key Azure App Configuration?
1. A sentinel key is a special key that you update after you complete the change of all other keys
2. You application monitors the sentinel key, When a change is detected your application refreshes all configuration values. 
3. This approach helps to ensure the ,consistency of configuration in your application and reduces of request made to App Configuration compared to monitoring all keys for changes.

### How to Use Sentinel Key in Azure App Configuration?
1. Create a in Azure App Configuration of your choice as "refreshAll"
2. When ever you want to update all configuration you just need to update this key
3. Program.cs change the following code rest of the code is same as above
```c#
config.AddAzureAppConfiguration(option =>
{
  option.Connect(connectionString);
  option.ConfigureRefresh(refresh =>
  {
    refresh
    //.Register("weather:count")
    .Register("refreshAll", true) // <= Here is the Change
    .SetCacheExpiration(TimeSpan.FromSeconds(5));
  });
});

```

### Azure App Configuration VS Azure Managed Identity
1. Azure App Configuration is a service that allows developers to store and manage configuration settings for their applications. It is designed to help simplify the process of managing configuration data in a centralized, secure, and scalable way.

2. Azure Managed Identity, on the other hand, is a service that allows Azure resources to authenticate to other Azure services without the need for a shared password or access keys. It provides a way for applications to authenticate to Azure services using a managed identity that is managed by Azure Active Directory.

3. In short, Azure App Configuration is for managing the configuration of the application and Azure Managed Identity is for authenticating and managing the identity of the application in Azure.

### What is Azure Access Control (IAM)?
1. Azure Access Control (also known as Azure Identity and Access Management, or IAM) is a service provided by Azure Active Directory (Azure AD) that allows you to manage access to Azure resources. It provides a way for you to create and manage role-based access control (RBAC) policies that determine who can access your Azure resources and what actions they can perform.

2. With Azure IAM, you can create roles that define the actions that a user or group of users can perform on a particular resource or resource group. You can then assign users or groups to those roles, giving them the permissions they need to perform their work. This allows you to have fine-grained control over who can access your resources and what they can do with them.

3. Azure IAM also provides features such as multi-factor authentication, conditional access, and identity protection to help secure your resources and protect them from unauthorized access. These features can be used in conjunction with Azure IAM to provide an additional layer of security for your resources.

4. In short, Azure IAM is a service that allows you to control access to Azure resources by creating and managing role-based access control policies. It enables fine-grained control over who can access your resources and what actions they can perform. It also provides features to secure and protect resources from unauthorized access.

### To Use the Azure.Identity Use the following Steps
1. Install the following Azure.Identity from Nuget
2. Follows the steps in Image File "AzureAppConfiguration Azure Identity 5"
3. Azure CLI needs to be installed [Azure Cli](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli)
4. To Login in Azure Account use this command (It will redirect you to chrome browser)
```c#
az login
```
5. Controller, Option, Service have the same as previous example
6. Changes in appsetting.json
```c#
"ConnectionStrings": {
  "appConfiguration": "Available in Secret Manager",
  "azureIdentityUrl": "https://resourcenameappconfiguration.azconfig.io"
}
```
7. Slight Changes for using Azure.Identity in Program.cs
```c#
builder.Host.ConfigureAppConfiguration(config =>
{
  // 1. Reading Azure Identity URL
  var azureIdentityUrl = config.Build().GetConnectionString("azureIdentityUrl");

  config.AddAzureAppConfiguration(option =>
  {
    // 2. Here we are Setting up Azure Credential
    // 3. The Application Get Locally Logged in by az login 

    var credential = new DefaultAzureCredential();
    option.Connect(new Uri(azureIdentityUrl), credential);

    option.ConfigureRefresh(refresh =>
    {
      refresh
      .Register("refreshAll", true)
      .SetCacheExpiration(TimeSpan.FromSeconds(5));
    });
  });

  // 2.1 Strongly Typed Configuration
  builder.Services.Configure<WeatherConfiguration>(config.Build().GetSection("weather"));
});
```

### What is Azure Tool Kit for Rider?
1. The Azure Toolkit for Rider is a plugin for the Rider IDE (Integrated Development Environment) that enables developers to easily develop, test, and deploy Azure applications from within the Rider IDE. The plugin provides a user-friendly interface for working with Azure services such as Azure App Service, Azure Functions, Azure Kubernetes Service (AKS), and more. It also includes features such as debugging and profiling of Azure applications, integration with Azure DevOps, and support for various programming languages including C#, Java, Python, and more.

2. It simplifies the process of developing and deploying Azure applications by providing developers with a set of tools and templates that can be used to quickly and easily create and deploy Azure applications. This allows developers to focus on writing code and building their applications, rather than worrying about the underlying infrastructure.

### How to Login with AZ CLI?
1. First you have to installed az cli 
2. az login (will redirect to Browser)
3. displays the following information
```json
[
  {
    "cloudName": "AzureCloud",
    "homeTenantId": "base64 encoded",
    "id": "base64 ",
    "isDefault": true,
    "managedByTenants": [],
    "name": "Azure Subscription Name Display Here",
    "state": "Enabled",
    "tenantId": "base64",
    "user": {
      "name": "ahsansoftengineer@outlook.com",
      "type": "user"
    }
  }
]
```

### How to Deploy you static app using Azure CLI Command?
1. First you have to login in az login
2. The use the following command in your static Application Folder
3. Replace the $resourceGroup, applicationName with original
```c#
az webapp up -g $resourceGroup -n applicationName --html
```

### Only the deployment part of the application remains because it is a huge project (Dot net core play list Part 30 [Rahul Nath])
