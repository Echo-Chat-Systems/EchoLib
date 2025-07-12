using System.Text.Json.Serialization;

namespace EchoLib.Params.Auth.Signin;

public class SigninResponseParams
{
	[JsonPropertyName("signature")] public required string Signature { get; set; }
	[JsonPropertyName("decrypted")] public required string Decrypted { get; set; }
	[JsonPropertyName("ref")] public required string Ref { get; set; }
}