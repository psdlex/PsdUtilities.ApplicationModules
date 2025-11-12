using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PsdUtilities.ApplicationModules.Models;
using PsdUtilities.ApplicationModules.Models.Parameters;

namespace PsdUtilities.ApplicationModules.Sample.Modules;

[ApplicationModule(ApplicationModuleOrders.Web.Debug.Swagger)] // runs prior to MainModule, -50 < 0
public sealed class DebugModule : ApplicationModule
{
    public override void Register(IServiceCollection services, ApplicationModuleParameters parameters)
    {
        var data = parameters.GetParameter<CrazyData>();
        NeverDoInProduction.Log("DebugModule received CrazyData with Title: " + data.Title);
        NeverDoInProduction.Log("Registering DebugModule");
        // some swagger registrations
    }

    public override void Use(IHost host)
    {
        NeverDoInProduction.Log("Using DebugModule");
    }
}
