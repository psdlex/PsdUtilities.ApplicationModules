using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PsdUtilities.ApplicationModules.Models.Parameters;

namespace PsdUtilities.ApplicationModules.Models;

public abstract class ApplicationModule
{
    public abstract void Register(IServiceCollection services, ApplicationModuleParameters parameters);
    public virtual void Use(IHost host)
    {
    }
}