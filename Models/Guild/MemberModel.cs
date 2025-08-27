using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Models.Crypto.Signing;
using Models.DatabaseModels;
using Models.Generic;
using Models.User;

namespace Models.Guild;

public class MemberModel : BaseDbm
{
	[Required] [JsonPropertyName("user-id")] public required UserId UserId { get; set; }
	[Required] [JsonPropertyName("roles")] public required IEnumerable<Snowflake> Roles { get; set; }
	public string? Nickname { get; set; }
	[JsonPropertyName("user-override")] public ProfileModel? UserOverride { get; set; }
	[JsonPropertyName("guild-override")] public ProfileModel? GuildOverride { get; set; }
}