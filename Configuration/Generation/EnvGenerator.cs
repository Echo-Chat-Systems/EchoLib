using System.Reflection;
using System.Text;
using EchoLib.Configuration.Attributes;

namespace EchoLib.Configuration.Generation;

public static class EnvGenerator
{
	public static string Generate<T>()
	{
		StringBuilder sb = new();

		Walk(typeof(T), "", sb);

		return sb.ToString();
	}

	private static void Walk(Type type, string prefix, StringBuilder sb)
	{
		foreach (PropertyInfo prop in type.GetProperties())
		{
			string name = string.IsNullOrEmpty(prefix)
				? prop.Name.ToUpper()
				: $"{prefix}__{prop.Name}".ToUpper();

			if (prop.IsDefined(typeof(ConfigSecretAttribute)))
				sb.AppendLine($"{name}=");

			if (prop.PropertyType.IsDefined(typeof(ConfigModelAttribute)))
				Walk(prop.PropertyType, name, sb);
		}
	}
}