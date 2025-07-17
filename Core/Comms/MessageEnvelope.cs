using System.Text.Json.Serialization;

namespace Core.Comms;

public class MessageEnvelope<TParams>
{
	[JsonPropertyName("target")] public string Target { get; set; } = string.Empty;
	[JsonPropertyName("data")] public ActionWrapper<TParams> Data { get; set; } = new();
}