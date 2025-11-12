using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PsdUtilities.ApplicationModules.Models;

namespace PsdUtilities.ApplicationModules.Internal;

internal static class Utils
{
    public static IList<DiscoveredModule> DiscoverModules(Func<Assembly, bool> assemblyFilter)
    {
        return AppDomain
            .CurrentDomain
            .GetAssemblies()

            // assembly filtering
            .Where(predicate: assemblyFilter)
            .SelectMany(assembly => assembly.GetTypes())

            // type filtering
            .Where(t =>
                typeof(ApplicationModule).IsAssignableFrom(t) &&
                t.IsClass &&
                t.IsAbstract == false
            )

            // instantiating
            .Select(t => new DiscoveredModule(
                    (ApplicationModule)Activator.CreateInstance(t)!,
                    t.GetCustomAttribute<ApplicationModuleAttribute>()?.Order ?? ApplicationModuleOrders.Default
                )
            )
            .ToList();
    }
}
