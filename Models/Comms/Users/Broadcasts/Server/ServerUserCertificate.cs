using Models.Crypto.Signing;

namespace Models.Comms.Users.Broadcasts.Server;

public class ServerUserCertificate
{
	public ServerUserPackage? Package { get; set; }
	public Signature? Signature { get; set; }
}