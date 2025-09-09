using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using PsdUtilities.ApplicationModules.Models;

namespace PsdUtilities.ApplicationModules;

public static class ServiceCollectionExtensions
{
    public static void RegisterModules(this IServiceCollection services) => services.RegisterModules(_ => true);
    public static void RegisterModules(this IServiceCollection services, Func<Assembly, bool> assemblyFilter)
    {
        var modules = DiscoverModules(assemblyFilter).ToList();

        if (modules.Count == 0)
            throw new InvalidOperationException($"{nameof(PsdUtilities)}.{nameof(ApplicationModules)} couldn't discover any application modules in the app domain.");

        foreach (var module in modules.SortByOrder())
        {
            module.Register(services);
            services.AddSingleton(module);
        }
    }

    private static IEnumerable<IApplicationModule> DiscoverModules(Func<Assembly, bool> assemblyFilter)
    {
        return AppDomain
            .CurrentDomain
            .GetAssemblies()

            // assembly filtering
            .Where(assemblyFilter)
            .SelectMany(assembly => assembly.GetTypes())

            // type filtering
            .Where(t => 
                typeof(IApplicationModule).IsAssignableFrom(t) &&
                t.IsClass &&
                t.IsAbstract == false &&
                t.GetConstructors().Any(c => c.GetParameters().Length == 0))
            .Select(Activator.CreateInstance)
            .Cast<IApplicationModule>();
    }

    private static IEnumerable<IApplicationModule> SortByOrder(this IEnumerable<IApplicationModule> modules)
        => modules.OrderBy(x => x.Order.Value);
}