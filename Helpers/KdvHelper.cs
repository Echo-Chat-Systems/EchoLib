using System.Security.Cryptography;
using EchoLib.Auth;
using EchoLib.Auth.Encryption;
using EchoLib.Auth.Signing;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math.EC.Rfc8032;
using Org.BouncyCastle.Security;

namespace EchoLib.Helpers;


/// <summary>
/// Key Derivation Helper.
/// </summary>
public static class KdvHelper
{
	public static KeySet Generate()
	{
		// Generate encryption keys
		using RSA encryption = RSA.Create(2048);
		byte[] pubEk = encryption.ExportRSAPublicKey();
		byte[] prvEk = encryption.ExportRSAPrivateKey();

		// Generate signing keys (Ed25519)
		Ed25519KeyPairGenerator signing = new();
		signing.Init(new Ed25519KeyGenerationParameters(new SecureRandom()));

		AsymmetricCipherKeyPair keyPair = signing.GenerateKeyPair();
		byte[] pubSk = ((Ed25519PublicKeyParameters)keyPair.Public).GetEncoded();
		byte[] prvSk = ((Ed25519PrivateKeyParameters)keyPair.Private).GetEncoded();

		return new KeySet
		{
			PubSk = new PublicSigningKey(pubSk),
			PrvSk = new PrivateSigningKey(prvSk),
			PubEk = new PublicEncryptionKey(pubEk),
			PrvEk = new PrivateEncryptionKey(prvEk)
		};
	}
}