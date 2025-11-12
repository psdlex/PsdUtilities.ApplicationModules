using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PsdUtilities.ApplicationModules.Internal;

namespace PsdUtilities.ApplicationModules.Extensions;

public static class HostExtensions
{
    public static void UseModules(this IHost host)
    {
        var orderedModules = host
            .Services
            .GetRequiredService<IEnumerable<DiscoveredModule>>()
            .OrderBy(m => m.Order);

        foreach (var module in orderedModules)
            module.Module.Use(host);
    }
}