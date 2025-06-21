using System.Text.Json.Serialization;

namespace EchoLib.Comms;

public class ActionWrapper<TParams>
{
	[JsonPropertyName("action")] public string Action { get; set; } = string.Empty;
	[JsonPropertyName("params")] public TParams Params { get; set; } = default!;
}