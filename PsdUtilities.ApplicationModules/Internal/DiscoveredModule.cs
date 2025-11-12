using PsdUtilities.ApplicationModules.Models;

namespace PsdUtilities.ApplicationModules.Internal;

internal sealed class DiscoveredModule
{
    public DiscoveredModule(ApplicationModule module, int order)
    {
        Module = module;
        Order = order;
    }

    public ApplicationModule Module { get; }
    public int Order { get; }
}