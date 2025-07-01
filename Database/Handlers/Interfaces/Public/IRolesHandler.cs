using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IRolesHandler
{
	RoleRow Create(Guid guildId, string name, string customisation, long permissions);
	RoleRow? Get(Guid roleId);
	RoleRow Update(RoleRow role);
	void Delete(Guid roleId);
	bool Exists(Guid roleId);
}