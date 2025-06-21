using EchoLib.Auth;
using EchoLib.Auth.Signing;

namespace EchoLib.Models.Users.Broadcasts.Server;

public class ServerUserCertificate
{
	public ServerUserPackage? Package { get; set; }
	public Signature? Signature { get; set; }
}