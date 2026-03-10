namespace EchoLib.Configuration.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigDescriptionAttribute(string description) : Attribute
{
	public string Description { get; } = description;
}