using Org.BouncyCastle.Crypto.Parameters;

namespace EchoLib.Auth.Signing;

public class PublicSigningKey
{
	private byte[] Value { get; }

	public Ed25519PublicKeyParameters KeyParams => new(Value, 0);

	public PublicSigningKey(string key)
	{
		Value = Convert.FromBase64String(key);
	}

	public PublicSigningKey(byte[] key)
	{
		Value = key;
	}
}