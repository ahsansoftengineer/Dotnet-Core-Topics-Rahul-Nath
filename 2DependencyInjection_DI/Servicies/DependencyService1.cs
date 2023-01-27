namespace _2DependencyInjection_DI.Servicies
{
  public class DependencyService1
  {
    public DependencyService1(
      IOperationTransient transientOperation,
      IOperationScoped scopedOperation,
      IOperationSingleton singletonOperation,
      IOperationSingletonInstance instanceOperation
      )
    {
      transient = transientOperation;
      scoped = scopedOperation;
      singleton = singletonOperation;
      singletonInstance = instanceOperation;
    }

    public IOperationTransient transient { get; }
    public IOperationScoped scoped { get; }
    public IOperationSingleton singleton { get; }
    public IOperationSingletonInstance singletonInstance { get; }

    public void Write()
    {
      Console.WriteLine();
      Console.WriteLine("-------------xxx-----------");
      Console.WriteLine("From DependencyService1");
      Console.WriteLine($"Transient - {transient.OperationId}");
      Console.WriteLine($"Scoped - {scoped.OperationId}");
      Console.WriteLine($"Singleton - {singleton.OperationId}");
      Console.WriteLine($"Singleton Instance - {singletonInstance.OperationId}");
    }
  }
}
