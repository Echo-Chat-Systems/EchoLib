using Models.Crypto.Signing;
using Models.Functional;
using Models.Functional.Crypto.Signing;
using Models.Generic;
using Models.Json;

namespace Models.Database.Channel;

public class ChannelMemberDbm : BaseDbm
{
	public required Snowflake ChannelId { get; set; }
	public required UserId UserId { get; set; }

	public ChannelCustomisationJm? CustomisationOverride { get; set; }
}