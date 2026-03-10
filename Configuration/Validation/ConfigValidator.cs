using System.Reflection;
using EchoLib.Configuration.Attributes;

namespace EchoLib.Configuration.Validation;

public static class ConfigValidator
{
	public static void Validate(object obj)
	{
		Validate(obj, obj.GetType().Name);
	}

	private static void Validate(object obj, string path)
	{
		Type type = obj.GetType();

		foreach (PropertyInfo prop in type.GetProperties())
		{
			object? value = prop.GetValue(obj);

			string newPath = $"{path}:{prop.Name}";

			if (value == null)
				throw new Exception($"Missing configuration value: {newPath}");

			if (prop.PropertyType == typeof(string) || prop.PropertyType.IsPrimitive)
				continue;

			if (prop.PropertyType.IsDefined(typeof(ConfigModelAttribute))) Validate(value, newPath);
		}
	}
}