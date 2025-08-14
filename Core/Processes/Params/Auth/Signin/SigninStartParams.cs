using System.Text.Json.Serialization;
using Models.Crypto.Encryption;
using Models.Crypto.Signing;

namespace Core.Processes.Params.Auth.Signin;

public class SigninStartParams
{
	[JsonPropertyName("sign-key")] public required PublicSigningKey Sk { get; set; }
	[JsonPropertyName("encrypt-key")] public required PublicEncryptionKey Ek { get; set; }
}