using Example;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        // Add User Secrets in Development Mode - See ReadMe.md
        if (hostContext.HostingEnvironment.IsDevelopment())
        {
            config.AddUserSecrets<Program>();
        }
    })
    .ConfigureServices((hostContext, services) => {
        services.AddOptions<AppSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("MySectionName").Bind(settings);
            });

        var appSettings = hostContext.Configuration.GetSection("MySectionName").Get<AppSettings>();

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
