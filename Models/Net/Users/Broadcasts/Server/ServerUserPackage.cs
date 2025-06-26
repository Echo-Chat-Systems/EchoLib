using EchoLib.Auth.Signing;
using EchoLib.Models.Users.Broadcasts.User;
using EchoLib.Models.Users.Reputation;

namespace EchoLib.Models.Users.Broadcasts.Server;

public class ServerUserPackage
{
	public required UserCertificate Package { get; set; }
	public required Rep Reputation { get; set; }
	public required DateTime Expiry { get; set; }
	public required Signature Signature { get; set; }
	public required string Origin { get; set; }
}