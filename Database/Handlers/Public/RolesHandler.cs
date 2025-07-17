using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Defaults.Public;

public abstract class RolesHandler : BaseHandler
{
	public async Task<MRole> Create(Guid guildId, string name, string customisation, long permissions)
	{
	}

	public async Task<MRole?> Get(Guid roleId)
	{
	}

	public async Task<MRole> Update(MRole role)
	{
	}

	public async Task Delete(Guid roleId)
	{
	}

	public async Task<bool> Exists(Guid roleId)
	{
	}
}