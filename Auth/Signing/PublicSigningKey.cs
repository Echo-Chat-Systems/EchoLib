using Org.BouncyCastle.Crypto.Parameters;

namespace EchoLib.Auth.Signing;

public class PublicSigningKey
{
	private byte[] Key { get; }

	public Ed25519PublicKeyParameters KeyParams => new(Key);

	public PublicSigningKey(string key)
	{
		Key = Convert.FromBase64String(key);
	}

	public PublicSigningKey(byte[] key)
	{
		Key = key;
	}

	public override string ToString()
	{
		return Convert.ToBase64String(Key);
	}
}