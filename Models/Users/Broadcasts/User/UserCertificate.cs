using EchoLib.Auth.Signing;

namespace EchoLib.Models.Users.Broadcasts.User;

public class UserCertificate
{
	public required UserPackage Package { get; set; }
	public required Signature Signature { get; set; }
}