using EchoLib.Database.Handlers.Bases.Public;

namespace EchoLib.Database.Handlers.HandlerGroups;

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicHandlers
{
	public required BChannelCategoriesHandler ChannelCategories { get; init; }
	public required BChannelMembersHandler ChannelMembers { get; init; }
	public required BChannelsHandler Channels { get; init; }
	public required BFilesHandler Files { get; init; }
	public required BGuildMembersHandler GuildMembers { get; init; }
	public required BGuildsHandler Guilds { get; init; }
	public required BInvitesHandler Invites { get; init; }
	public required BMessageAttachmentsHandler MessageAttachments { get; init; }
	public required BMessagesHandler Messages { get; init; }
	public required BRolesHandler Roles { get; init; }
	public required BUserRolesHandler UserRoles { get; init; }
	public required BUsersHandler Users { get; init; }

	public void Populate(HandlersGroup handlers)
	{
		ChannelCategories.Populate(handlers);
		ChannelMembers.Populate(handlers);
		Channels.Populate(handlers);
		Files.Populate(handlers);
		GuildMembers.Populate(handlers);
		Guilds.Populate(handlers);
		Invites.Populate(handlers);
		MessageAttachments.Populate(handlers);
		Messages.Populate(handlers);
		Roles.Populate(handlers);
		UserRoles.Populate(handlers);
		Users.Populate(handlers);
	}
}