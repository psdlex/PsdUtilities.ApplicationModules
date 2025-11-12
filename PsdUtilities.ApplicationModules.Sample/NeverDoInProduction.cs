namespace PsdUtilities.ApplicationModules.Sample;

public static class NeverDoInProduction
{
    public static void Log(string message)
        => Console.WriteLine(message);
}
