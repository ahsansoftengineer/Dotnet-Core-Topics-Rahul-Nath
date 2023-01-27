## By context or functionality

### Use singleton;
1. If your service has a shared state such as cache service. Singleton services with mutable state should consider using a locking mechanism for thread safety.
2. If your service is stateless. If your service implementation is very lightweight and infrequently used, you might also consider registering it as transient.
3. One Instance for Every Request for Every One

### Use scoped;
1. If your service should act as a singleton within the scope of the request. In Asp.Net Core each request has in own service scope. 2. Database and repository services are often registered as scoped services. Default registration of DbContext in EntityFramework Core is also scoped. Scoped lifetime ensures that all the services created within the request shares the same DbContext.
3. Each Request have One Instance

### Use transient;
1. If your service holds a private (non-shared) state for the execution context.
2. If your service will be used by multiple threads concurrently and it is not thread safe.
3. If your service has a transient dependency, such as HttpClient, that has a short intended lifetime.
4. Each Request will have Several Instance

### By dependencies
1. Singleton services can inject other singleton service. Singleton service can also inject transient services but be aware that transient service will be alive as long as the singleton service which is usually the lifetime of the application.
2. Singleton services should not inject scoped services as a best practice. Because scoped services will act as a singleton, which usually is not the intended way by design for scoped services.
3. Scoped services can inject other scoped services and singleton services. Scoped services can also use transient services but you should review your design and see if you can register your transient service as scoped service.
4. Transient services can inject all type of services. They are usually intended as short-lived services.
5. If you need to use transient services in singleton or scoped services you should consider service factory approach for transient services. If your transient service implementation is a disposable class, the service factory approach is recommended.