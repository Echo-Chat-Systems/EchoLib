using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BUserRolesHandler : BaseHandler
{
	public abstract Task<MUserRole> Create(PublicSigningKey user, Guid role);
	public abstract Task<MUserRole> Create(MUser user, MRole mRole);
	public abstract Task<MUserRole?> Get(Guid userRoleId);
	public abstract Task<MUserRole?> Get(PublicSigningKey user, Guid role);
	public abstract Task<MUserRole?> Get(MUser user, MRole mRole);
	public abstract Task<MUserRole> Update(MUserRole userRole);
	public abstract Task Delete(Guid userRoleId);
	public abstract Task Delete(PublicSigningKey user, Guid role);
	public abstract Task Delete(MUser user, MRole mRole);
	public abstract Task<bool> Exists(Guid userRoleId);
	public abstract Task<bool> Exists(PublicSigningKey user, Guid role);
	public abstract Task<bool> Exists(MUser user, MRole mRole);
}