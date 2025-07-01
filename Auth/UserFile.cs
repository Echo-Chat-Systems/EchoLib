using System.Text.Json.Serialization;
using EchoLib.Auth.Encryption;
using EchoLib.Auth.Signing;
using EchoLib.Models.Other;

namespace EchoLib.Auth;


/// <summary>
/// Represents a decrypted locally stored user file.
///
/// All user files are encrypted with AES 256 using a user-provided password.
/// </summary>
public class UserFile : KeySet
{
	public ServerInfo Server { get; set; }
}