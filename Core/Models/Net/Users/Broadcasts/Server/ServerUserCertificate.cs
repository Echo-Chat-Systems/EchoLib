using Core.Auth.Signing;

namespace Core.Models.Net.Users.Broadcasts.Server;

public class ServerUserCertificate
{
	public ServerUserPackage? Package { get; set; }
	public Signature? Signature { get; set; }
}