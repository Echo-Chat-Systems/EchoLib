using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Models.Crypto.Signing;

namespace Models.Channel;

public class MemberModel : BaseEntityModel
{
	[JsonIgnore]
	public new Snowflake Id { get; set; }

	[JsonPropertyName("id")]
	[Required]
	public UserId UserId { get; set; }

	public CustomisationModel? CustomisationOverride { get; set; }
}