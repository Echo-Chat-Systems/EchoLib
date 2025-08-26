namespace Models.Channel;

public class ChannelDto
{
	/// <summary>
	/// Channel ID.
	/// </summary>
	public required ulong Id { get; init; }

	/// <summary>
	/// Channel name.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Parent channel ID.
	/// </summary>
	public ulong? Parent { get; set; }

	/// <summary>
	/// Channel member list.
	/// </summary>
	public IEnumerable<ChannelMemberDto>? Members { get; set; }

	/// <summary>
	/// Channel list index.
	/// </summary>
	public int? Index { get; set; }

	/// <summary>
	/// Channel configuration <see cref="ChannelConfigJm"/>
	/// </summary>
	public ChannelConfigJm? Config { get; set; }

	/// <summary>
	/// Channel customisation <see cref="ChannelCustomisationJm"/>
	/// </summary>
	public ChannelCustomisationJm? Customisation { get; set; }
}