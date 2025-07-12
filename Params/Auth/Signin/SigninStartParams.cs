using System.Text.Json.Serialization;
using EchoLib.Auth.Encryption;
using EchoLib.Auth.Signing;

namespace EchoLib.Params.Auth.Signin;

public class SigninStartParams
{
	[JsonPropertyName("sign-key")] public required PublicSigningKey Sk { get; set; }
	[JsonPropertyName("encrypt-key")] public required PublicEncryptionKey Ek { get; set; }
}