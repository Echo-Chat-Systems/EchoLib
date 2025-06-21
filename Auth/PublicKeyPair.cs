using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EchoLib.Auth.Encryption;
using EchoLib.Auth.Signing;

namespace EchoLib.Auth;

public class PublicKeyPair
{
	[JsonPropertyName("sk")]
	[Required]
	public PublicSigningKey? SigningKey { get; set; }

	[JsonPropertyName("ek")]
	[Required]
	public PublicEncryptionKey? EncryptionKey { get; set; }
}