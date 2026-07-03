using Models.Comms.Users.Broadcasts.User;
using Models.Comms.Users.Reputation;
using Models.Crypto.Signing;

namespace Models.Comms.Users.Broadcasts.Server;

public class ServerUserPackage
{
	public required UserCertificate Package { get; set; }
	public required Rep Reputation { get; set; }
	public required DateTime Expiry { get; set; }
	public required Signature Signature { get; set; }
	public required string Origin { get; set; }
}