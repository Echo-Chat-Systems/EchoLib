# EchoLib.Configuration

This is just a relatively simple framework designed for use in EchoLib and other
projects following the same architecture.

## How To

Usage of this framework is relatively simple, all you do is build a model that 
matches your configuration file (or vise versa) and call `ConfigBuilder.Build<T>(IConfiguration config)`
to get a built configuration object. 

### Example

```csharp
// Get IConfig (from appsettings.json for example)
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Build configuration object
MyConfig configObject = ConfigBuilder.Build<MyConfig>(config);

// Config object is ready to use :)
```