using Core.Auth;
using Core.Models.User.Reputation;
using Database.Models.User;

namespace Core.Models.User;

public class PublicUserModel
{
	public required OriginModel Origin { get; init; }
	public required PublicKeyPair Keys { get; init; }
	public required ProfileModel Profile { get; init; }
	public required ReputationModel Rep { get; init; }
}