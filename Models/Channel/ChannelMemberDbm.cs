using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Models.Crypto.Signing;
using Models.Generic;

namespace Models.Channel;

public class ChannelMemberDbm : BaseDbm
{
	public new Snowflake Id { get; set; }

	[JsonPropertyName("id")]
	[Required]
	public UserId UserId { get; set; }

	public ChannelCustomisationJm? CustomisationOverride { get; set; }
}