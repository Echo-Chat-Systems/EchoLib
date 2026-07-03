using Models.Crypto.Signing;

namespace Models.Comms.Users.Broadcasts.User;

public class UserCertificate
{
	public required UserPackage Package { get; set; }
	public required Signature Signature { get; set; }
}