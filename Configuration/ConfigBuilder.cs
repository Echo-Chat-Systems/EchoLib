using System.Reflection;
using EchoLib.Configuration.Attributes;
using EchoLib.Configuration.Validation;
using Microsoft.Extensions.Configuration;

namespace EchoLib.Configuration;

public static class ConfigBuilder
{
	public static T Build<T>(IConfiguration config) where T : new()
	{
		Type rootType = typeof(T);

		T instance = new();

		IEnumerable<PropertyInfo> properties = rootType
			.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(p => p.IsDefined(typeof(ConfigPropertyAttribute)));

		foreach (PropertyInfo property in properties)
		{
			IConfigurationSection section = config.GetSection(property.Name);

			object? value = section.Get(property.PropertyType);

			if (value == null)
				throw new Exception($"Missing configuration section: {property.Name}");

			property.SetValue(instance, value);
		}

		ConfigValidator.Validate(instance);

		return instance;
	}
}