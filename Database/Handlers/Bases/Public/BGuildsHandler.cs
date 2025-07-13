using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BGuildsHandler : BaseHandler
{
	public abstract Task<MGuild> Create(PublicSigningKey owner, string name, string? customisation = null);
	public abstract Task<MGuild?> Get(Guid id);
	public abstract Task<MGuild> Update(MGuild guild);
	public abstract Task Delete(Guid id);
	public abstract Task<bool> Exists(Guid id);
}