using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using PsdUtilities.ApplicationModules.Internal;
using PsdUtilities.ApplicationModules.Models;
using PsdUtilities.ApplicationModules.Models.Parameters;

namespace PsdUtilities.ApplicationModules.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddModules(this IServiceCollection services) => AddModules(services, _ => true);
    public static void AddModules(this IServiceCollection services, Func<Assembly, bool> assemblyFilter) => AddModules(services, assemblyFilter);
    public static void AddModules(this IServiceCollection services, Func<Assembly, bool> assemblyFilter, params ApplicationModuleParameter[] parameters)
    {
        var discoveredModules = Utils.DiscoverModules(assemblyFilter);

        if (discoveredModules.Count == 0)
            throw new InvalidOperationException($"{nameof(PsdUtilities)}.{nameof(ApplicationModules)} couldn't discover any application modules in the app domain.");

        var parametersInstance = new ApplicationModuleParameters(parameters);

        foreach (var discoveredModule in discoveredModules.OrderBy(m => m.Order))
        {
            discoveredModule.Module.Register(services, parametersInstance);
            services.AddSingleton(discoveredModule);
        }
    }
}