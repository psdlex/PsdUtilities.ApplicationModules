using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PsdUtilities.ApplicationModules.Models.Parameters;

public sealed class ApplicationModuleParameters : IReadOnlyList<ApplicationModuleParameter>
{
    private readonly List<ApplicationModuleParameter> _parameters = [];

    internal ApplicationModuleParameters(IEnumerable<ApplicationModuleParameter> parameters)
    {
        _parameters.AddRange(parameters);
    }

    public int Count => _parameters.Count;
    public ApplicationModuleParameter this[int index] => _parameters[index];

    public T? TryGetParameter<T>() 
        where T : class
    {
        return TryGetParameter<T>(typeof(T).Name);
    }

    public T? TryGetParameter<T>(string name)
        where T : class
    {
        var val = this.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return val?.Value as T;
    }

    public T GetParameter<T>()
        where T : class
    {
        return GetParameter<T>(typeof(T).Name);
    }

    public T GetParameter<T>(string name)
        where T : class
    {
        var val = this.First(p => p.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        return (T)val.Value;
    }

    public IEnumerator<ApplicationModuleParameter> GetEnumerator() => _parameters.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}