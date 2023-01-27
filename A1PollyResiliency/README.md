## Polly Resiliency
### What is Polly Resiliency?
1. Polly is an open-source .NET library that allows developers to express resilience and transient fault-handling policies such as retry, circuit breaker, and timeout when working with remote dependencies. 
2. It is designed to help developers build more resilient and reliable applications by providing a simple, expressive API for handling exceptions and transient fault conditions when calling HTTP and other resources.
3. Polly can help reduce the complexity of error handling code and make it easier to build applications that are able to recover from failure and continue operating despite intermittent issues.
4. Package Name Microsoft.Extensions.Http.Polly
5. To Understand better see [README.md](https://github.com/App-vNext/Polly)
6. Polly is more likely as RXJS retry, blocking, delay in some cases

### What is Resilience?
Resilience refers to the ability of a system, service, or application to recover from failures, errors, and other types of disruptions in a timely and effective manner. A resilient system is able to maintain its core functionality and continue operating even when faced with adverse conditions or unexpected events.

### What is Transient?
Transient fault refers to a temporary error or failure that occurs when a system or service is unable to complete a request or operation due to a temporary issue such as a network connectivity problem or resource contention. Transient faults are usually self-correcting and do not indicate a permanent failure or problem with the system.

### How to Implement Polly for HttpRequest from Backend?
1. Configuring Polly in Program.cs
```c#
 var timeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(5));


builder.Services
.AddHttpClient<IWeatherService, WeatherService>(c =>
{
    c.BaseAddress = new Uri("http://api.weatherapi.com/v1/current.json");
})
.AddTransientHttpErrorPolicy(policy =>
{
    // handling Error 408, 5XX
    // retry times, gap amoung retry
    return policy.WaitAndRetryAsync(3,
    _ => TimeSpan.FromSeconds(2)
    );
})
.AddTransientHttpErrorPolicy(policy =>
{
    return policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(5));
})
.AddPolicyHandler(request =>
{
    if (request.Method == HttpMethod.Get)
        return timeout;
    return Policy.NoOpAsync<HttpResponseMessage>();
});

```
2. Rest of the Example is same as *[9HttpClient Way 5](#)*