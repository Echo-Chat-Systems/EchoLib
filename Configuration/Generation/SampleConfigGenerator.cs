using System.Reflection;
using System.Text.Json;
using EchoLib.Configuration.Attributes;

namespace EchoLib.Configuration.Generation;

public static class SampleConfigGenerator
{
	public static string Generate<T>()
	{
		object obj = GenerateObject(typeof(T));

		return JsonSerializer.Serialize(obj, new JsonSerializerOptions
		{
			WriteIndented = true
		});
	}

	private static object GenerateObject(Type type)
	{
		Dictionary<string, object?> dict = new();

		foreach (PropertyInfo prop in type.GetProperties())
		{
			Type t = prop.PropertyType;

			if (t == typeof(string))
				dict[prop.Name] = "string";
			else if (t == typeof(int))
				dict[prop.Name] = 0;
			else if (t == typeof(bool))
				dict[prop.Name] = false;
			else if (t.IsDefined(typeof(ConfigModelAttribute)))
				dict[prop.Name] = GenerateObject(t);
			else
				dict[prop.Name] = "value";
		}

		return dict;
	}
}