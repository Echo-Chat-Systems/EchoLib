using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IUserRolesHandler
{
	UserRoleRow Create(PublicSigningKey user, Guid role);
	UserRoleRow Create(UserRow user, RoleRow role);
	UserRoleRow? Get(Guid userRoleId);
	UserRoleRow? Get(PublicSigningKey user, Guid role);
	UserRoleRow? Get(UserRow user, RoleRow role);
	UserRoleRow Update(UserRoleRow userRole);
	void Delete(Guid userRoleId);
	void Delete(PublicSigningKey user, Guid role);
	void Delete(UserRow user, RoleRow role);
	bool Exists(Guid userRoleId);
	bool Exists(PublicSigningKey user, Guid role);
	bool Exists(UserRow user, RoleRow role);
}