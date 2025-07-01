using System.Text.Json;

namespace EchoLib.Helpers;

public static class StaticOptions
{
	public static JsonSerializerOptions JsonSerialzer = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower };
}