using EchoLib.Database.Handlers.Interfaces.Public;

namespace EchoLib.Database.Handlers.Interfaces;

public class PublicHandlers
{
	public required IChannelCategoriesHandler ChannelCategories { get; init; }
	public required IChannelMembersHandler ChannelMembers { get; init; }
	public required IChannelsHandler Channels { get; init; }
	public required IFilesHandler Files { get; init; }
	public required IGuildMembersHandler GuildMembers { get; init; }
	public required IGuildsHandler Guilds { get; init; }
	public required IInvitesHandler Invites { get; init; }
	public required IMessageAttachmentsHandler MessageAttachments { get; init; }
	public required IMessagesHandler Messages { get; init; }
	public required IUserRolesHandler UserRoles { get; init; }
	public required IUsersHandler Users { get; init; }
}