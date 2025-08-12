using System.Text.Json.Serialization;
using Core.Auth.Signing;
using Core.Helpers.Snowflake;

namespace Core.Models.User.Reputation;

public class RemarkModel
{
	[JsonIgnore] public required Snowflake Id { get; init; }

	public required Signature Sig { get; init; }
	public required RemarkDirection Direction { get; init; }
	[JsonPropertyName("user")] public required UserId TargetUser { get; init; }
	public required string Comment { get; init; }
}

public enum RemarkDirection : short
{
	Up = 1,
	Neutral = 0,
	Down = -1
}