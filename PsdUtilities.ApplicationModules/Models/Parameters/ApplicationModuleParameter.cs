namespace PsdUtilities.ApplicationModules.Models.Parameters;

public sealed class ApplicationModuleParameter
{
    public ApplicationModuleParameter(object value)
        : this(value.GetType().Name, value)
    {
    }

    public ApplicationModuleParameter(string name, object value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; }
    public object Value { get; }
}
