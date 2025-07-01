using System.Text.Json.Serialization;
using EchoLib.Auth.Encryption;
using EchoLib.Auth.Signing;

namespace EchoLib.Auth;

public class KeySet
{
	public PublicSigningKey PubSk { get; set; }
	public PrivateSigningKey PrvSk { get; set; }
	public PublicEncryptionKey PubEk { get; set; }
	public PrivateEncryptionKey PrvEk { get; set; }
}