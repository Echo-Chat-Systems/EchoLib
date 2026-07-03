using Models.Crypto;
using Models.Generic;
using Models.Json.Crypto;
using Models.JsonModels;
using Models.User.Reputation;

namespace Models.User;

public class PublicUserModel
{
	public required OriginJm Origin { get; init; }
	public required PublicKeyPair Keys { get; init; }
	public required ProfileModel Profile { get; init; }
	public required ReputationModel Rep { get; init; }
}