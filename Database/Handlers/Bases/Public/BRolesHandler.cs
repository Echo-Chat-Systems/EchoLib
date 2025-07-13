using System.Data.Common;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BRolesHandler : BaseHandler
{
	public abstract Task<MRole> Create(Guid guildId, string name, string customisation, long permissions);
	public abstract Task<MRole?> Get(Guid roleId);
	public abstract Task<MRole> Update(MRole role);
	public abstract Task Delete(Guid roleId);
	public abstract Task<bool> Exists(Guid roleId);
}