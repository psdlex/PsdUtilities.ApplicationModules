using Microsoft.Extensions.Hosting;

namespace PsdUtilities.ApplicationModules.Sample.Services;

public sealed class MyBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested == false)
        {
            Console.WriteLine("MyBackgroundService is running...");
            await Task.Delay(1000, stoppingToken);
        }
    }
}