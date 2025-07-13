using System.Data.Common;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BChannelsHandler : BaseHandler
{
	public abstract Task<MChannel> Create(Guid guildId, string name, short? type = null, string? customisation = null, string? config = null);
	public abstract Task<MChannel?> Get(Guid id);
	public abstract Task<MChannel> Update(MChannel mChannel);
	public abstract Task Delete(Guid id);
	public abstract Task<bool> Exists(Guid id);
}