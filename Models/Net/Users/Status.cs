using System.Text.Json.Serialization;

namespace EchoLib.Models.Users;

public class Status
{
	[JsonPropertyName("type")]
	public required string Type { get; set; }

	[JsonPropertyName("text")]
	public required string Text { get; set; }
}