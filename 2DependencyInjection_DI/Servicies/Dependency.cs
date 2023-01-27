namespace _2DependencyInjection_DI.Servicies
{
  public interface IOperation
  {
    Guid OperationId { get; }
  }
  public interface IOperationTransient : IOperation { }
  public interface IOperationScoped : IOperation { }
  public interface IOperationSingleton : IOperation { }
  public interface IOperationSingletonInstance : IOperation { }
  public class Dependency :
    IOperationTransient,
    IOperationScoped,
    IOperationSingleton,
    IOperationSingletonInstance
  {
    public Dependency() : this(Guid.NewGuid()) { }
    public Dependency(Guid id)
    {
      OperationId = id;
    }
    public Guid OperationId { get; private set; }
  }
}
