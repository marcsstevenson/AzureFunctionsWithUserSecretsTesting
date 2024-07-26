# Azure Functions With User Secrets Testing
An Azure Functions project using .NET8 isolated.

Configured to use User Secrets to provide a layer of configuration outside of your repo which can contain sensitive configuration values.

## Setup
Add your user secrets to the project using these values:
```json
{
    "Key2": "Key2 from secrets.json",
    "Key3": "Key3 from secrets.json",
    "ConnectionStrings": {
        "ConnectionString2": "ConnectionString2 from secrets.json",
        "ConnectionString3": "ConnectionString2 from secrets.json"
    }
}
```

Note: You do not need to prefix the settings keys with "Values" but you do need to prefix the connection strings with "ConnectionStrings". 

## Testing
Run the project and call the HTTP endpoint with a GET request which does the following:

```csharp
public class ConfigGoGo(IConfiguration configuration)
{
    [Function("ConfigGoGo")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        return new OkObjectResult(new
        {
            Key1 = configuration["Key1"],
            Key2 = configuration["Key2"],
            Key3 = configuration["Key3"],
            ConnectionString1 = configuration.GetConnectionString("ConnectionString1"),
            ConnectionString2 = configuration.GetConnectionString("ConnectionString2"),
            ConnectionString3 = configuration.GetConnectionString("ConnectionString3")
        });
    }
}
```

Note the output from the HTTP endpoint is:

```json
{
  "key1": "Key1 from local.settings.json",
  "key2": "Key2 from secrets.json",
  "key3": "Key3 from secrets.json",
  "connectionString1": "ConnectionString1 from local.settings.json",
  "connectionString2": "ConnectionString2 from secrets.json",
  "connectionString3": "ConnectionString2 from secrets.json"
}
```

This demonstrates that the settings from the local.settings.json file are being used in conjunction with the user secrets and the keys can belong in either or both of the config sources.

Note also that the HostBuilder in program.cs picks up the local.settings.json by convension and there is not need for:

```csharp
config.AddJsonFile("local.settings.json");
```

## Reference
Created in response to this Stack Overflow question: [Azure Functions - How to use User Secrets in Azure Functions](https://stackoverflow.com/questions/72200094/azure-functions-how-to-use-user-secrets-in-azure-functions)