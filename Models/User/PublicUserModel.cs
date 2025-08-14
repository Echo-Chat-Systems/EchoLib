using Models.Comms;
using Models.Crypto;
using Models.User.Reputation;

namespace Models.User;

public class PublicUserModel
{
	public required OriginModel Origin { get; init; }
	public required PublicKeyPair Keys { get; init; }
	public required ProfileModel Profile { get; init; }
	public required ReputationModel Rep { get; init; }
}