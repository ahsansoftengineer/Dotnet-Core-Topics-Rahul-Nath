using Microsoft.OpenApi.Any;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Polly;

namespace A4AzureKeyVault.Controllers
{
  public interface IMessageSender
  {
    Task SendMessage(string content);
  }
  public class AzureQueueSender: IMessageSender
  {
    public IConfiguration Configuration { get; }
    public AzureQueueSender(IConfiguration configuration)
    {
      Configuration = configuration;  
    }

    public async Task SendMessage(string content)
    {
      var connectionString = Configuration.GetValue<string>("QueueConnectionString");
      var retryPolicy = Policy.Handle<StorageException>()
        .RetryAsync(2, async (ex, count, context) =>
        {
          (Configuration as IConfigurationRoot).Reload();
          connectionString = Configuration.GetValue<string>("QueueConnectionString");
        });

      await retryPolicy.ExecuteAsync(() => SendMessaage(connectionString));
    }
    private static async Task SendMessaage(string connectionString)
    {
      var storageAccount = CloudStorageAccount.Parse(connectionString);
      storageAccount.CreateCloudQueueClient();
      var queueClient = storageAccount.CreateCloudQueueClient();

      var queue = queueClient.GetQueueReference("ahsanaccount");
      var message = new CloudQueueMessage("Hello, World");

      await queue.AddMessageAsync(message);

    }
  }
}
