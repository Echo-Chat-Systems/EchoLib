using Models.Crypto.Signing;
using Models.Functional;
using Models.Functional.Crypto.Signing;
using Models.Generic;
using Models.Json;
using Models.Json.Channel;

namespace Models.DataTransfer.Channel;

public class ChannelMemberDto
{
	public required UserId UserId { get; init; }
	public required Snowflake ChannelId { get; init; }
	public required ChannelMemberConfigJm Config { get; set; }
	public ChannelCustomisationJm? CustomisationOverride { get; set; }
}