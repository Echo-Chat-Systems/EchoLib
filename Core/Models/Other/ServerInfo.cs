using System.Text.Json.Serialization;

namespace Core.Models.Other;

public class ServerInfo
{
	[JsonPropertyName("address")]
	public required string Address;
	
	[JsonPropertyName("port")]
	public required int Port;
	
	[JsonPropertyName("version")]
	public required string Version;
}