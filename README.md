# PsdUtilities.ApplicationModules

## How to use?
```csharp
// Implementing the module
public sealed class MainModule : IApplicationModule
{
    public ApplicationModuleOrder Order { get; } = ApplicationModuleOrder.Default; // Has implicit conversion from int values

    public void Register(IServiceCollection services)
    {
        services.AddSingleton<MySingletonService>();
    }
}

// Run
IServiceCollection services = new ServiceCollection();

// Scans every assembly from the AppDomain.CurrentDomain
// !! Make sure your AppDomain initializes the target assembly before the scan, otherwise it won't be able to see it !!
services.RegisterModules(assembly => assembly.FullName?.Contains(nameof(MyProject)) ?? false);

var provider = services.BuildServiceProvider();
var myService = provider.GetRequiredService<MySingletonService>();

myService.Action();
```
