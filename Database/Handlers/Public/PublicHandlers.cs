using EchoLib.Database.Handlers.Chat;
using EchoLib.Database.Handlers.Public;

namespace EchoLib.Database.Handlers.HandlerGroups;

// ReSharper disable once ClassNeverInstantiated.Global
public class PublicHandlers
{
	public required InvitesHandler Invites { get; init; }
	public required RolesHandler Roles { get; init; }
	public required UserRolesHandler UserRoles { get; init; }
	public required UsersHandler Users { get; init; }

	public void Populate(HandlersGroup handlers)
	{
		Invites.Populate(handlers);
		Roles.Populate(handlers);
		UserRoles.Populate(handlers);
		Users.Populate(handlers);
	}
}