using Models.Crypto;
using Models.Json.Crypto;
using Models.User;

namespace Models.Comms.Users.Broadcasts.User;

public class UserPackage
{
	public required string Origin { get; set; }
	public required ProfileModel Profile { get; set; }
	public required PublicKeyPair Keys { get; set; }
}