using EchoLib.Database.Handlers.Interfaces.Public;

namespace EchoLib.Database.Handlers.PgSql.Public;

public class PublicHandlers
{
	public required IChannelCategoriesHandler ChannelCategories;
	public required IChannelMembersHandler ChannelMembers;
	public required IChannelsHandler Channels;
	public required IFilesHandler Files;
	public required IGuildMembersHandler GuildMembers;
	public required IGuildsHandler Guilds;
	public required IInvitesHandler Invites;
	public required IMessageAttachmentsHandler MessageAttachments;
	public required IMessagesHandler Messages;
	public required IUserRolesHandler UserRoles;
	public required IUsersHandler Users;
}