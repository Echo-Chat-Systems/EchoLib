using Core.Auth.Signing;
using Core.Models.Net.Users.Broadcasts.User;
using Core.Models.Net.Users.Reputation;

namespace Core.Models.Net.Users.Broadcasts.Server;

public class ServerUserPackage
{
	public required UserCertificate Package { get; set; }
	public required Rep Reputation { get; set; }
	public required DateTime Expiry { get; set; }
	public required Signature Signature { get; set; }
	public required string Origin { get; set; }
}