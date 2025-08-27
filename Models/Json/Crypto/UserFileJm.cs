using System.Text.Json.Serialization;
using Models.Crypto;
using Models.Generic;

namespace Models.Json.Crypto;


/// <summary>
/// Represents a decrypted locally stored user file.
///
/// All user files are encrypted with AES 256 using a user-provided password.
/// </summary>
public class UserFileJm
{
	[JsonPropertyName("keys")]
	public required KeySetJm Keys { get; set; } 
	
	[JsonPropertyName("server")]
	public required ServerInfoJm Server { get; set; }
}