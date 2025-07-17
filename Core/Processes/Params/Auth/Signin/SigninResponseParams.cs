using System.Text.Json.Serialization;
using Core.Auth.Signing;

namespace Core.Processes.Params.Auth.Signin;

public class SigninResponseParams
{
	[JsonPropertyName("signature")] public required Signature Signature { get; set; }
	[JsonPropertyName("decrypted")] public required string Decrypted { get; set; }
	[JsonPropertyName("ref")] public required string Ref { get; set; }
}