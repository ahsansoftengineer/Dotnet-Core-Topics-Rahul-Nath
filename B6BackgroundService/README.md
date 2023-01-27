## Background Task

### What is Background Task in dotnet Core?
1. In ASP.NET Core, background tasks can be implemented as hosted services. 
2. A hosted service is a class with background task logic that implements the IHostedService interface. 
3. This article provides three hosted service examples: Background task that runs on a timer. Hosted service that activates a scoped service.

### How do I use Background Task as Singleton Service?
1. Create a background task using BackgroundService 
```c#
public class MyBackgroundService : BackgroundService
{
  private readonly ILogger<MyBackgroundService> logger;

  public MyBackgroundService(ILogger<MyBackgroundService> logger)
  {
    this.logger = logger;
  }
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      logger.LogWarning("From MyBackgroundService: ExecuteAsync", DateTime.Now);
      await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
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
```
2. Add the Service into Service Container using AddHostedService
```c#
builder.Services.AddControllers();
builder.Services.AddHostedService<MyBackgroundService>();
```

### How to use Scoped Service for BackgroundService Task
1. Creating a Scoped Service
```c#
public interface IScopedService
{
  void Write();
}
public class MyScopedService : IScopedService
{
  private readonly ILogger<MyScopedService> logger;
  public Guid Id { get; set; }

  public MyScopedService(ILogger<MyScopedService> logger)
  {
    this.logger = logger;
    this.Id = Guid.NewGuid();
  }
  public void Write()
  {
    logger.LogWarning("MyScopedService {Id}", Id);
  }
}
```
2. Registering the Service as Scoped
```c#
builder.Services.AddControllers();
builder.Services.AddHostedService<MyBackgroundService>();
builder.Services.AddScoped<IScopedService, MyScopedService>();  
```
3. Utilizing ScopedService into Background Singleton Service
```c#
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
      // Injecting Scoped Service into Singleton Service
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

````