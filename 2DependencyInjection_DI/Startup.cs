using _2DependencyInjection_DI.Servicies;

namespace _2DependencyInjection_DI
{
  public class Startup
  {
     
    public void ConfigureServices(IServiceCollection services)
    {
      // 1. Registering Services using Built-en Extension Method
      services.AddControllers();
      services.AddSwaggerGen();
      // 2. Reg Serv using Add Method
      //var item = new ServiceDescriptor(
      //  typeof(IOperationTransient),
      //  a => new Operation(),
      //  ServiceLifetime.Transient
      //);
      //services.Add(item);
      // 3. Reg ILogger builtien Dot net Service
      //services.AddLogging();
      //services.AddSingleton<ILogger, ILogger>();
      // 4. Reg Serv based on Lifetime
      //
      // If your service has a transient dependency, such as HttpClient, that has a short intended lifetime.
      services.AddTransient<IOperationTransient, Dependency>();
      // Database and repository services are often registered as scoped services
      services.AddScoped<IOperationScoped, Dependency>();
      services.AddSingleton<IOperationSingleton, Dependency>();
      services.AddSingleton<IOperationSingletonInstance>(
        (a) => new Dependency(Guid.Empty)
      );
      // There we are Registring Same Service Twice Last One will win
      //services.AddSingleton<IOperationSingletonInstance>(
      //  (a) => new Dependency()
      //);
      services.AddTransient<DependencyService1, DependencyService1>();
      services.AddTransient<DependencyService2, DependencyService2>();

    }
    // By Default you Only Have Three Service in ConfigureServicies
    // 1. IConfiguration, 2. IWebHostEnvironment, 3. IHostEnvironment
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostEnvironment host)
    {
      // Application Default Configuration
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

    }
  }
}
