using System.Text.Json.Serialization;
using Core.Models.Other;

namespace Core.Auth;


/// <summary>
/// Represents a decrypted locally stored user file.
///
/// All user files are encrypted with AES 256 using a user-provided password.
/// </summary>
public class UserFile
{
	[JsonPropertyName("keys")]
	public required KeySet Keys { get; set; } 
	
	[JsonPropertyName("server")]
	public required ServerInfo Server { get; set; }
}