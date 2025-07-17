using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Processes.Params.Error;

public class ErrorParams
{
	[JsonPropertyName("message")]
	public string? Message { get; set; } = string.Empty;

	[JsonPropertyName("type")]
	[Required]
	public required int Type { get; set; }

	[JsonPropertyName("miq")]
	[Required]
	public required string MessageInQuestion { get; set; } = string.Empty;
}