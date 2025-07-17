using System.Text.Json;

namespace Core.Helpers;

public static class StaticOptions
{
	public static JsonSerializerOptions JsonSerialzer = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower };
}