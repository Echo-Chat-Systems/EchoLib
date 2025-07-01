using EchoLib.Auth;

namespace EchoLib.Models.Net.Users.Broadcasts.User;

public class UserPackage
{
	public required string Origin { get; set; }
	public required BaseProfile BaseProfile { get; set; }
	public required PublicKeyPair Keys { get; set; }
}