using System.Text.Json;
using Core.Helpers.Snowflake;

namespace Core.Helpers;

public static class StaticOptions
{
	public static JsonSerializerOptions JsonSerialzer = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower };
	public static SnowflakeGenerator SnowflakeGenerator = new();
}