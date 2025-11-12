using System;

namespace PsdUtilities.ApplicationModules.Models;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ApplicationModuleAttribute : Attribute
{
    public ApplicationModuleAttribute(int order)
    {
        Order = order;
    }

    public int Order { get; }
}