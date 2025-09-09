using Microsoft.Extensions.DependencyInjection;

namespace PsdUtilities.ApplicationModules.Models;

public interface IApplicationModule
{
    ApplicationModuleOrder Order { get; }
    void Register(IServiceCollection services);
}