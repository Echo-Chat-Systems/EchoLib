using EchoLib.Database.Handlers.Chat;

namespace EchoLib.Database.Handlers.HandlerGroups;

public class ChatHandlers
{
	public required ChannelCategoriesHandler ChannelCategories { get; init; }
	public required ChannelMembersHandler ChannelMembers { get; init; }
	public required ChannelsHandler Channels { get; init; }
	public required GuildMembersHandler GuildMembers { get; init; }
	public required GuildsHandler Guilds { get; init; }
}