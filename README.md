# PsdUtilities.ApplicationModules

## How to use?
```csharp
// Implementation of the module
public sealed class MainModule : IApplicationModule
{
    public ApplicationModuleOrder Order { get; } = ApplicationModuleOrder.Default; // Has implicit conversion from int values

    public void Register(IServiceCollection services)
    {
        services.AddSingleton<MySingletonService>();
    }
}

// Utilization of it via the service collection
IServiceCollection services = new ServiceCollection();

// Scans every assembly from the AppDomain.CurrentDomain
// !! Make sure your AppDomain initializes the target assembly before the scan, otherwise it won't be able to see it !!
services.RegisterModules(assembly => assembly.FullName?.Contains(nameof(MyProject)) ?? false);

var provider = services.BuildServiceProvider();
var myService = provider.GetRequiredService<MySingletonService>();

myService.Action();
```
