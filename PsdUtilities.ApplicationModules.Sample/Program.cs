using Microsoft.Extensions.Hosting;
using PsdUtilities.ApplicationModules.Extensions;
using PsdUtilities.ApplicationModules.Models.Parameters;
using PsdUtilities.ApplicationModules.Sample;

var builder = new HostBuilder();

builder.ConfigureServices(services =>
{
    services.AddModules(
        assemblyFilter: a => a == typeof(Program).Assembly, // assembly filtering
        new ApplicationModuleParameter(new CrazyData("Title from my parameter!"))
    );
});

var app = builder.Build();

app.UseModules();
app.Run();