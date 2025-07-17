using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Auth.Encryption;
using Core.Auth.Signing;

namespace Core.Auth;

public class PublicKeyPair
{
	[JsonPropertyName("sk")]
	[Required]
	public PublicSigningKey? SigningKey { get; set; }

	[JsonPropertyName("ek")]
	[Required]
	public PublicEncryptionKey? EncryptionKey { get; set; }
}