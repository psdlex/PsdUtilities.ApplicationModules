namespace PsdUtilities.ApplicationModules.Models;

public sealed partial class ApplicationModuleOrder
{
    public ApplicationModuleOrder(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static implicit operator ApplicationModuleOrder(int value) => new(value);

    public static readonly ApplicationModuleOrder Default = 0;

    //// Common middleware orders
    //public static readonly ApplicationModuleOrder HttpsRedirection = 0;
    //public static readonly ApplicationModuleOrder StaticFiles = 20;
    //public static readonly ApplicationModuleOrder Routing = 40;
    //public static readonly ApplicationModuleOrder Cors = 60;
    //public static readonly ApplicationModuleOrder Authentication = 80;
    //public static readonly ApplicationModuleOrder Authorization = 100;
    //public static readonly ApplicationModuleOrder ExceptionHandler = 120;
    //public static readonly ApplicationModuleOrder Endpoints = 140;

    //// Diagnostics / debugging
    //public static readonly ApplicationModuleOrder DeveloperExceptionPage = -100;
    //public static readonly ApplicationModuleOrder Swagger = -50;
}