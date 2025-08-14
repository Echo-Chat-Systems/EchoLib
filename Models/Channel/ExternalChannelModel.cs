namespace Models.Channel;

/// <summary>
/// Used for serialisation of channels to a protocol compliant format.
/// </summary>
public class ExternalChannelModel(ChannelModel model, IEnumerable<MemberModel> members)
	: ChannelModel(model.Id, model.GuildId, model.Name, model.Config, model.Customisation)
{
	/// <summary>
	/// Channel members.
	/// </summary>
	public required IEnumerable<MemberModel> Members { get; init; } = members;
}