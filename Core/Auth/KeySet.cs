using System.Text.Json.Serialization;
using Core.Auth.Encryption;
using Core.Auth.Signing;

namespace Core.Auth;

public class KeySet
{
	[JsonPropertyName("pub_sk")]
	public PublicSigningKey PubSk { get; set; }
	
	[JsonPropertyName("prv_sk")]
	public PrivateSigningKey PrvSk { get; set; }
	
	[JsonPropertyName("pub_ek")]
	public PublicEncryptionKey PubEk { get; set; }
	
	[JsonPropertyName("prv_ek")]
	public PrivateEncryptionKey PrvEk { get; set; }
}