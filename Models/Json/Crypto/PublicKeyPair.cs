using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Models.Crypto.Encryption;
using Models.Crypto.Signing;

namespace Models.Json.Crypto;

public class PublicKeyPair
{
	[JsonPropertyName("sk")]
	[Required]
	public PublicSigningKey? SigningKey { get; set; }

	[JsonPropertyName("ek")]
	[Required]
	public PublicEncryptionKey? EncryptionKey { get; set; }
}