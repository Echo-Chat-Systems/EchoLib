using System.Security.Cryptography;
using System.Text.Json;
using Models.Crypto;
using Models.Json.Crypto;

namespace Core.Helpers;

/// <summary>
///		Helper class for working with stored user files.
/// </summary>
public static class UserFileHelper
{
	private const int KeySize = 32;  // 256 Bit
	private const int SaltSize = 16;
	private const int NonceSize = 12;
	private const int TagSize = 16;
	private const int Iterations = 100_000;

	/// <summary>
	/// Encrypts a user file to a specified location using the specified passphrase.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="outputFile"></param>
	/// <param name="passphrase"></param>
	public static void Encrypt(UserFileJm data, FileInfo outputFile, string passphrase)
	{
		// Serialise data
		byte[] plaintext = JsonSerializer.SerializeToUtf8Bytes(data);

		// Generate required cryptographic values
		byte[] salt = RandomBytes(SaltSize);
		byte[] nonce = RandomBytes(NonceSize);
		byte[] key = DeriveKey(passphrase, salt);

		byte[] ciphertext = new byte[plaintext.Length];
		byte[] tag = new byte[TagSize];

		// Encrypt the data into ciphertext
		using AesGcm aes = new(key, TagSize);
		aes.Encrypt(nonce, plaintext, ciphertext, tag);

		// Write data to filesystem using known sizes
		using FileStream fs = new(outputFile.Name, FileMode.Create, FileAccess.Write);
		fs.Write(salt);
		fs.Write(nonce);
		fs.Write(tag);
		fs.Write(ciphertext);  // Ciphertext is written last as it is an unknown size
	}

	/// <summary>
	/// Attempts to decrypt a user file using the given passphrase.
	/// </summary>
	/// <param name="file"></param>
	/// <param name="password"></param>
	/// <param name="userFile"></param>
	/// <returns></returns>
	public static bool Decrypt(FileInfo file, string password, out UserFileJm? userFile)
	{
		// Set userFile by default to null
		userFile = null;

		// Read in the file as a byte array
		byte[] data = File.ReadAllBytes(file.Name);

		// Split up file in reverse order of what it was written
		byte[] salt = data[..SaltSize];
		byte[] nonce = data[SaltSize..(SaltSize + NonceSize)];
		byte[] tag = data[(SaltSize + NonceSize)..(SaltSize + NonceSize + TagSize)];
		byte[] ciphertext = data[(SaltSize + NonceSize + TagSize)..];

		// Plaintext byte array
		byte[] plaintext = new byte[ciphertext.Length];

		using AesGcm aes = new AesGcm(DeriveKey(password, salt), TagSize);
		try
		{
			aes.Decrypt(nonce, ciphertext, tag, plaintext);
		}
		catch (CryptographicException) { return false; }

		// Deserialize plaintext into content
		userFile = JsonSerializer.Deserialize<UserFileJm>(plaintext);
		return true;
	}

	private static byte[] DeriveKey(string passphrase, byte[] salt)
	{
		using Rfc2898DeriveBytes kdf = new(passphrase, salt, Iterations, HashAlgorithmName.SHA3_256);
		return kdf.GetBytes(KeySize);
	}

	private static byte[] RandomBytes(int size)
	{
		byte[] bytes = new byte[size];
		RandomNumberGenerator.Fill(bytes);
		return bytes;
	}
}