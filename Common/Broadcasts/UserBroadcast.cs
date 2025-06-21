using Models.Common.Auth;

namespace Models.Common.Broadcasts;

public class UserBroadcast
{
	public required string Origin { get; set; }
	public required ProfileBroadcast Profile { get; set; }
	public required KeyPair Keys { get; set; }
}