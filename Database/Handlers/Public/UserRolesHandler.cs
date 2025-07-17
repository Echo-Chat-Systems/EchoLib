using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Defaults.Public;

public abstract class UserRolesHandler : BaseHandler
{
	public async Task<MUserRole> Create(PublicSigningKey user, Guid role)
	{
	}

	public async Task<MUserRole> Create(MUser user, MRole mRole)
	{
	}

	public async Task<MUserRole?> Get(Guid userRoleId)
	{
	}

	public async Task<MUserRole?> Get(PublicSigningKey user, Guid role)
	{
	}

	public async Task<MUserRole?> Get(MUser user, MRole mRole)
	{
	}

	public async Task<MUserRole> Update(MUserRole userRole)
	{
	}

	public async Task Delete(Guid userRoleId)
	{
	}

	public async Task Delete(PublicSigningKey user, Guid role)
	{
	}

	public async Task Delete(MUser user, MRole mRole)
	{
	}

	public async Task<bool> Exists(Guid userRoleId)
	{
	}

	public async Task<bool> Exists(PublicSigningKey user, Guid role)
	{
	}

	public async Task<bool> Exists(MUser user, MRole mRole)
	{
	}
}