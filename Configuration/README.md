# EchoLib.Configuration

This is just a relatively simple framework designed for use in EchoLib and other
projects following the same architecture.

## How To

Usage of this framework is relatively simple, all you do is build a model that 
matches your configuration file (or vise versa) and call `ConfigBuilder.Build<T>(IConfiguration config)`
to get a built configuration object. 

### Example

```csharp
class MyConfig 
{
    [ConfigProperty]
    public DatabaseModel Database { get; set; }
    
    [ConfigModel]
    public class DatabaseModel
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        [ConfigSecret]
        public string Password { get; set; }
    }
}

// Get IConfig (from appsettings.json for example)
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Build configuration object
MyConfig configObject = ConfigBuilder.Build<MyConfig>(config);

// Config object is ready to use :)
```