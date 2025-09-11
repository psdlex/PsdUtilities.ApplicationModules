using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using PsdUtilities.ApplicationModules.Models;

namespace PsdUtilities.ApplicationModules;

public static class ServiceCollectionExtensions
{
    public static void RegisterModules(this IServiceCollection services) => RegisterModules(services, _ => true);
    public static void RegisterModules(this IServiceCollection services, Func<Assembly, bool> assemblyFilter) => RegisterModules(services, assemblyFilter, new Dictionary<string, object>());
    public static void RegisterModules(this IServiceCollection services, Func<Assembly, bool> assemblyFilter, IReadOnlyDictionary<string, object> args)
    {
        var modules = DiscoverModules(assemblyFilter, args).ToList();

        if (modules.Count == 0)
            throw new InvalidOperationException($"{nameof(PsdUtilities)}.{nameof(ApplicationModules)} couldn't discover any application modules in the app domain.");

        foreach (var module in modules.SortByOrder())
        {
            module.Register(services);
            services.AddSingleton(module);
        }
    }

    private static IEnumerable<IApplicationModule> DiscoverModules(Func<Assembly, bool> assemblyFilter, IReadOnlyDictionary<string, object> args)
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
                t.IsAbstract == false)

            // instantiating
            .Select(t =>
            {
                var ctor = t.GetConstructors().First();
                var parameters = ctor.GetParameters();
                var ctorArgs = parameters.Select(p =>
                {
                    if (args.TryGetValue(p.Name!, out var arg))
                        return arg;

                    if (p.HasDefaultValue)
                        return p.DefaultValue;

                    throw new InvalidOperationException($"Cannot resolve constructor parameter '{p.Name}' of type '{p.ParameterType}' for application module '{t.FullName}'.");
                }).ToArray();

                return (IApplicationModule)Activator.CreateInstance(t, ctorArgs)!;
            })
            .Cast<IApplicationModule>();
    }

    private static IEnumerable<IApplicationModule> SortByOrder(this IEnumerable<IApplicationModule> modules)
        => modules.OrderBy(x => x.Order.Value);
}