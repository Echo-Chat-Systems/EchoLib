using Org.BouncyCastle.Crypto.Parameters;

namespace EchoLib.Auth.Signing;

public class PrivateSigningKey
{
	private byte[] Key { get; }

	public Ed25519PrivateKeyParameters KeyParams => new(Key);

	public PrivateSigningKey(string key)
	{
		Key = Convert.FromBase64String(key);
	}

	public PrivateSigningKey(byte[] key)
	{
		Key = key;
	}
}