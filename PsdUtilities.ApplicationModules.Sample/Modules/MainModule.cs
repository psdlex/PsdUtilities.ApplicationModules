using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PsdUtilities.ApplicationModules.Models;
using PsdUtilities.ApplicationModules.Models.Parameters;
using PsdUtilities.ApplicationModules.Sample.Services;

namespace PsdUtilities.ApplicationModules.Sample.Modules;

[ApplicationModule(ApplicationModuleOrders.Default)] // unnecessary, just for demonstration
public sealed class MainModule : ApplicationModule
{
    public override void Register(IServiceCollection services, ApplicationModuleParameters parameters)
    {
        NeverDoInProduction.Log("Registering MainModule");
        services.AddHostedService<MyBackgroundService>();
    }

    public override void Use(IHost host)
    {
        NeverDoInProduction.Log("Using MainModule");
    }
}