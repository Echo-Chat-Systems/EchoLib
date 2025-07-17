using Core.Auth.Signing;

namespace Core.Models.Net.Users.Broadcasts.User;

public class UserCertificate
{
	public required UserPackage Package { get; set; }
	public required Signature Signature { get; set; }
}