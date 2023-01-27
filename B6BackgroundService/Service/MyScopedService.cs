namespace B6BackgroundService.Service
{
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

}
