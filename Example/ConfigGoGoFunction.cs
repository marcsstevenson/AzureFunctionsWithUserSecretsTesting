using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Example;

/// <summary>
/// Lists out our configuration values
/// </summary>
public class ConfigGoGo(IConfiguration configuration, IOptions<AppSettings> appSettingsOptions)
{
    AppSettings appSettingsappSettings = appSettingsOptions.Value;

    [Function("ConfigGoGo")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        return new OkObjectResult(new
        {
            Key1 = configuration["Key1"],
            Key2 = configuration["Key2"],
            Key3 = configuration["Key3"],
            Key4 = configuration["Key4"],
            Key5 = configuration["Key5"],
            ConnectionString1 = configuration.GetConnectionString("ConnectionString1"),
            ConnectionString2 = configuration.GetConnectionString("ConnectionString2"),
            ConnectionString3 = configuration.GetConnectionString("ConnectionString3"),
            AppSettingsKey4 = appSettingsappSettings.Key4,
            AppSettingsKey5 = appSettingsappSettings.Key5,
            AppSettingsKey6 = appSettingsappSettings.Key6
        });
    }
}
