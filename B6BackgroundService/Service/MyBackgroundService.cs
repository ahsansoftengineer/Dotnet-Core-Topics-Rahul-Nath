namespace B6BackgroundService.Service
{
  public class MyBackgroundService : BackgroundService
  {
    private readonly ILogger<MyBackgroundService> logger;
    private readonly IServiceProvider serviceProvider;

    public MyBackgroundService(
      ILogger<MyBackgroundService> logger,
      IServiceProvider serviceProvider
      )
    {
      this.logger = logger;
      this.serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        using (var scope = serviceProvider.CreateScope())
        {
          logger.LogWarning("From MyBackgroundService: ExecuteAsync", DateTime.Now);
          var scopedService = scope.ServiceProvider.GetRequiredService<IScopedService>();
          scopedService.Write();
          await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
     
      }
      //return Task.CompletedTask;
    }
    // To Gracefully shutting down application
    public override Task StopAsync(CancellationToken cancellationToken)
    {
      logger.LogWarning("From MyBackgroundService: StopAsync", DateTime.Now);
      return base.StopAsync(cancellationToken);
    }
  }
}
